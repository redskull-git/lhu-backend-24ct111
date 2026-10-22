using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LHU_MaSV_NguyenVanTi.Api.Database.Entities;
using LHU_MaSV_NguyenVanTi.Api.IServices;
using Microsoft.IdentityModel.Tokens;

namespace LHU_MaSV_NguyenVanTi.Api.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(NguoiDungEntity user, string? hoTen = null)
        {
            var secretKey = _config["JwtSettings:SecretKey"]
                ?? throw new InvalidOperationException("JwtSettings:SecretKey chưa được cấu hình.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Họ tên hiển thị: Lấy từ tham số truyền vào, hoặc từ hồ sơ SinhVien nếu có, hoặc dùng Tên đăng nhập
            var displayName = !string.IsNullOrWhiteSpace(hoTen)
                ? hoTen
                : (user.SinhVien?.HoTen ?? user.TenDangNhap);

            // Đóng gói các Claims mô tả người dùng vào Payload
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.TenDangNhap),
                new("HoTen", displayName),
                new(ClaimTypes.Role, user.VaiTro),
                new("SinhVienId", user.SinhVienId?.ToString() ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // Hạn dùng 2 giờ
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
