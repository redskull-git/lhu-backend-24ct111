using LHU_MaSV_NguyenVanTi.Api;
using LHU_MaSV_NguyenVanTi.Api.Database;
using LHU_MaSV_NguyenVanTi.Api.IServices;
using LHU_MaSV_NguyenVanTi.Api.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

builder.Services.AddDbContext<DatabaseContext>(options =>
    {
        options.UseSqlServer(connectionString);
    }
);

builder.Services.AddScoped(o => new SqlConnection(connectionString));

builder.Services.AddScoped<IMonHocService, MonHocService>();
builder.Services.AddScoped<ILopHocPhan, LopHocPhan>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Swagger"));

app.UseAuthorization();

app.MapControllers();

app.Run();
