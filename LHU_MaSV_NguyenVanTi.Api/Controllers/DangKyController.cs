using Dapper;
using LHU_MaSV_NguyenVanTi.Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LHU_MaSV_NguyenVanTi.Api.Controllers
{
    [Route("api/dang-ky")]
    [ApiController]
    public class DangKyController : ControllerBase
    {
        private readonly SqlConnection cn;
        private readonly ILogger<DangKyController> _logger;

        public DangKyController(SqlConnection cn, ILogger<DangKyController> logger)
        {
            this.cn = cn;
            this._logger = logger;
        }

        /// <summary>
        /// API Đăng ký học phần an toàn:
        /// 1. Phòng chống lỗ hổng IDOR (SinhVienId trích xuất từ Token JWT).
        /// 2. Điều tiết lưu lượng chống spam ([EnableRateLimiting("DangKyHocPhanPolicy")] - tối đa 5 req/phút).
        /// 3. Ghi nhật ký có cấu trúc (Structured Logging) kèm CorrelationId xuyên suốt.
        /// </summary>
        [Authorize(Roles = "SinhVien")]
        [EnableRateLimiting("DangKyHocPhanPolicy")]
        [HttpPost("them-moi")]
        public async Task<IActionResult> ThemMoi([FromBody] DangKyHocPhanAnToanDto req)
        {
            // 1. Trích xuất SinhVienId an toàn từ Claims trong Token đã được ký số
            var sinhVienIdClaim = User.FindFirst("SinhVienId")?.Value;
            if (string.IsNullOrEmpty(sinhVienIdClaim))
            {
                _logger.LogWarning("Yêu cầu đăng ký học phần bị từ chối do thiếu Claim SinhVienId trong Token.");
                return Unauthorized("Token không hợp lệ hoặc thiếu thông tin định danh sinh viên.");
            }

            int currentSinhVienId = int.Parse(sinhVienIdClaim);

            _logger.LogInformation("Bắt đầu xử lý đăng ký học phần: SinhVienId = {SinhVienId}, LopHocPhanId = {LopHocPhanId}",
                currentSinhVienId, req.LopHocPhanId);

            // 2. Thực hiện đăng ký thông qua Stored Procedure với đúng SinhVienId của người đang đăng nhập
            var param = new DynamicParameters();
            param.Add("@SinhVienId", currentSinhVienId, dbType: DbType.Int32);
            param.Add("@LopHocPhanId", req.LopHocPhanId, dbType: DbType.Int32);
            param.Add("@KetQua", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@ThongBao", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);

            await cn.ExecuteAsync("sp_DangKyHocPhan", param, commandType: CommandType.StoredProcedure);

            int ketQua = param.Get<int>("@KetQua");
            string thongBao = param.Get<string>("@ThongBao");

            if (ketQua == 1)
            {
                _logger.LogInformation("Sinh viên {SinhVienId} đăng ký thành công lớp {LopHocPhanId}",
                    currentSinhVienId, req.LopHocPhanId);

                return Ok(new
                {
                    message = "Đăng ký học phần thành công!",
                    lopHocPhanId = req.LopHocPhanId,
                    sinhVienId = currentSinhVienId
                });
            }

            _logger.LogWarning("Sinh viên {SinhVienId} đăng ký lớp {LopHocPhanId} thất bại. Lý do nghiệp vụ: {ThongBao}",
                currentSinhVienId, req.LopHocPhanId, thongBao);

            return BadRequest(new { message = thongBao });
        }
    }
}
