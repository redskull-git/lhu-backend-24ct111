using System.Text;
using System.Threading.RateLimiting;
using LHU_MaSV_NguyenVanTi.Api;
using LHU_MaSV_NguyenVanTi.Api.Database;
using LHU_MaSV_NguyenVanTi.Api.IServices;
using LHU_MaSV_NguyenVanTi.Api.Middlewares;
using LHU_MaSV_NguyenVanTi.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// 1. CẤU HÌNH STRUCTURED LOGGING VỚI SERILOG (ROLLING FILE & CONSOLE)
// ============================================================================
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{CorrelationId}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/lhu-academic-.log",
        rollingInterval: RollingInterval.Day, // Mỗi ngày tạo một file log độc lập
        retainedFileCountLimit: 30,          // Tự động xóa tệp log quá hạn 30 ngày
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{CorrelationId}] {Message:lj} {Properties:j}{NewLine}{Exception}"
    )
    .CreateLogger();

builder.Host.UseSerilog();

// ============================================================================
// 2. CẤU HÌNH XỬ LÝ NGOẠI LỆ TẬP TRUNG & CONTROLLERS
// ============================================================================
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();

// ============================================================================
// 3. CẤU HÌNH CHÍNH SÁCH CHIA SẺ TÀI NGUYÊN CHÉO NGUỒN GỐC (CORS)
// ============================================================================
var allowedOrigins = builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>()
    ?? new[] { "http://localhost:3000", "http://localhost:5173" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("LhuFrontendPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
              .WithHeaders("Content-Type", "Authorization", "X-Correlation-ID")
              .AllowCredentials(); // Hỗ trợ gửi kèm Cookie / Authorization Credentials
    });
});

// ============================================================================
// 4. CẤU HÌNH ĐIỀU TIẾT LƯU LƯỢNG (RATE LIMITING .NET 8/9/10)
// ============================================================================
builder.Services.AddRateLimiter(options =>
{
    // Cấu hình phản hồi HTTP 429 Too Many Requests chuẩn mực
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.Headers.RetryAfter = "60";
        var responseObj = new
        {
            status = 429,
            title = "Too Many Requests",
            detail = "Bạn đã gửi quá 5 yêu cầu đăng ký trong vòng 1 phút. Vui lòng thử lại sau.",
            retryAfterSeconds = 60
        };
        await context.HttpContext.Response.WriteAsJsonAsync(responseObj, cancellationToken);
    };

    // Chính sách giới hạn cho API Đăng ký học phần (Fixed Window: 5 req/phút)
    options.AddPolicy("DangKyHocPhanPolicy", httpContext =>
    {
        // Ưu tiên phân vùng theo Claim SinhVienId trong Token đã xác thực; nếu chưa có thì dùng IP
        var userIdentifier = httpContext.User.FindFirst("SinhVienId")?.Value 
                             ?? httpContext.Connection.RemoteIpAddress?.ToString() 
                             ?? "anonymous";

        return RateLimitPartition.GetFixedWindowLimiter(userIdentifier, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 5,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0 // Từ chối ngay lập tức khi vượt quá hạn ngạch
        });
    });
});

// ============================================================================
// 5. CẤU HÌNH SWAGGER UI TÍCH HỢP XÁC THỰC BEARER TOKEN
// ============================================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Hệ thống Quản lý Học vụ API - LHU 24CT111",
        Version = "v1",
        Description = "API Quản lý học vụ tích hợp CORS, Rate Limiting, Serilog Logging và JWT RBAC"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Dán trực tiếp Access Token (chuỗi JWT eyJ...):",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", document, null),
            new List<string>()
        }
    });
});

// ============================================================================
// 6. CẤU HÌNH DATABASE CONTEXT (EF CORE) & DAPPER
// ============================================================================
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped(sp => new SqlConnection(connectionString));

// ============================================================================
// 7. ĐĂNG KÝ DEPENDENCY INJECTION CHO SERVICES
// ============================================================================
builder.Services.AddScoped<IMonHocService, MonHocService>();
builder.Services.AddScoped<ILopHocPhan, LopHocPhan>();
builder.Services.AddScoped<ISinhVienService, SinhVienService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// ============================================================================
// 8. CẤU HÌNH JWT BEARER AUTHENTICATION & AUTHORIZATION
// ============================================================================
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// ============================================================================
// 9. THỨ TỰ MIDDLEWARE PIPELINE CHUẨN MỰC
// ============================================================================
// 1. Tự động sinh và đính kèm mã truy vết X-Correlation-ID đầu tiên
app.UseMiddleware<CorrelationIdMiddleware>();

// 2. Ghi nhận HTTP Request với Serilog (chứa CorrelationId)
app.UseSerilogRequestLogging();

// 3. Xử lý ngoại lệ tập trung
app.UseExceptionHandler();

// 4. Tài liệu Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Học Vụ API v1");
});

// 5. Kích hoạt CORS (Phải đứng trước Authentication để xử lý Preflight OPTIONS)
app.UseCors("LhuFrontendPolicy");

// 6. Xác thực JWT (Giải mã token để nạp Claims vào HttpContext.User)
app.UseAuthentication();

// 7. Điều tiết lưu lượng Rate Limiting (Đứng sau Authentication để đọc Claim SinhVienId)
app.UseRateLimiter();

// 8. Phân quyền vai trò (RBAC)
app.UseAuthorization();

// 9. Điều phối đến Controller Endpoint
app.MapControllers();

app.Run();
