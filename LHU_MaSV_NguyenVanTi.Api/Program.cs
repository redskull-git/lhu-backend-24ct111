using System.Text;
using LHU_MaSV_NguyenVanTi.Api;
using LHU_MaSV_NguyenVanTi.Api.Database;
using LHU_MaSV_NguyenVanTi.Api.IServices;
using LHU_MaSV_NguyenVanTi.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình xử lý ngoại lệ tập trung (Global Exception Handling)
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();

// 2. Cấu hình Swagger UI tích hợp nút Authorize ổ khóa (JWT Bearer Security)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Hệ thống Quản lý Học vụ API - LHU 24CT111",
        Version = "v1",
        Description = "API Quản lý học vụ tích hợp xác thực JWT và phân quyền theo vai trò RBAC"
    });

    // Định nghĩa Scheme xác thực HTTP Bearer Token (chuẩn OpenAPI)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Dán trực tiếp Access Token (chuỗi JWT eyJ...):",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Tự động gắn Header Authorization: Bearer {token} vào tất cả request
    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", document, null),
            new List<string>()
        }
    });
});

// 3. Cấu hình Chuỗi kết nối CSDL SQL Server & Dapper
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped(sp => new SqlConnection(connectionString));

// 4. Đăng ký các dịch vụ nghiệp vụ (Dependency Injection)
builder.Services.AddScoped<IMonHocService, MonHocService>();
builder.Services.AddScoped<ILopHocPhan, LopHocPhan>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// 5. Cấu hình JWT Bearer Authentication Middleware
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            ValidateLifetime = true,          // Tự động từ chối nếu Token đã hết hạn
            ValidateIssuerSigningKey = true,  // Kiểm tra chữ ký số bằng SecretKey gốc
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// 6. Cấu hình HTTP Request Pipeline
app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Học Vụ API v1");
});

// THỨ TỰ SỐNG CÒN TRONG PIPELINE:
app.UseAuthentication(); // 1. Xác thực: Bạn là ai? (401 Unauthorized nếu thất bại)
app.UseAuthorization();  // 2. Phân quyền: Bạn được làm gì? (403 Forbidden nếu không đủ vai trò)

app.MapControllers();

app.Run();
