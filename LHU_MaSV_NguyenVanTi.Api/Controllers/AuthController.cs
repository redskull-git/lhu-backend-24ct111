using System.Security.Claims;
using BCrypt.Net;
using LHU_MaSV_NguyenVanTi.Api.Database;
using LHU_MaSV_NguyenVanTi.Api.Database.Entities;
using LHU_MaSV_NguyenVanTi.Api.Dto;
using LHU_MaSV_NguyenVanTi.Api.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LHU_MaSV_NguyenVanTi.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _db;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(DatabaseContext db, IJwtTokenService jwtTokenService)
        {
            _db = db;
            _jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// API Đăng nhập hệ thống - Cấp phát JSON Web Token (JWT)
        /// Không yêu cầu Token trước đó ([AllowAnonymous])
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto req)
        {
            // 1. Tìm tài khoản theo Tên đăng nhập kèm hồ sơ SinhVien (để lấy Họ tên)
            var user = await _db.NguoiDungs
                .Include(u => u.SinhVien)
                .FirstOrDefaultAsync(u => u.TenDangNhap == req.TenDangNhap);
            if (user == null)
            {
                return Unauthorized(new { message = "Tên đăng nhập hoặc mật khẩu không chính xác." });
            }

            // 2. Kiểm tra mật khẩu băm bằng BCrypt.Verify
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(req.MatKhau, user.MatKhauHash);
            if (!isPasswordValid)
            {
                return Unauthorized(new { message = "Tên đăng nhập hoặc mật khẩu không chính xác." });
            }

            // 3. Cấp phát Token có thời hạn 2 giờ
            var hoTen = user.SinhVien?.HoTen ?? user.TenDangNhap;
            var token = _jwtTokenService.GenerateToken(user, hoTen);

            return Ok(new LoginResponseDto
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(2),
                HoTen = hoTen,
                VaiTro = user.VaiTro,
                SinhVienId = user.SinhVienId
            });
        }

        /// <summary>
        /// API Xem thông tin tài khoản đang đăng nhập
        /// Yêu cầu phải gửi kèm JWT Token hợp lệ ([Authorize])
        /// </summary>
        [Authorize]
        [HttpGet("me")]
        public ActionResult<UserInfoDto> GetProfile()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            var hoTen = User.FindFirst("HoTen")?.Value ?? "";
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? "";
            var svIdStr = User.FindFirst("SinhVienId")?.Value;

            int.TryParse(userIdStr, out int userId);
            int? sinhVienId = int.TryParse(svIdStr, out int svId) ? svId : null;

            return Ok(new UserInfoDto
            {
                Id = userId,
                TenDangNhap = username,
                HoTen = hoTen,
                VaiTro = role,
                SinhVienId = sinhVienId
            });
        }
    }
}
