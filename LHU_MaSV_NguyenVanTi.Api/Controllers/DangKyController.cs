using Dapper;
using LHU_MaSV_NguyenVanTi.Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LHU_MaSV_NguyenVanTi.Api.Controllers
{
    [Route("api/dang-ky")]
    [ApiController]
    public class DangKyController : ControllerBase
    {
        private readonly SqlConnection cn;

        public DangKyController(SqlConnection cn)
        {
            this.cn = cn;
        }

        /// <summary>
        /// API Đăng ký học phần an toàn (Phòng chống lỗ hổng IDOR)
        /// Sinh viên bắt buộc phải có vai trò SinhVien ([Authorize(Roles = "SinhVien")])
        /// SinhVienId được trích xuất trực tiếp từ Claims của Token, loại bỏ nguy cơ sửa ID người khác.
        /// </summary>
        [Authorize(Roles = "SinhVien")]
        [HttpPost("them-moi")]
        public async Task<IActionResult> ThemMoi([FromBody] DangKyHocPhanAnToanDto req)
        {
            // 1. Trích xuất SinhVienId an toàn từ Claims trong Token đã được ký số
            var sinhVienIdClaim = User.FindFirst("SinhVienId")?.Value;
            if (string.IsNullOrEmpty(sinhVienIdClaim))
            {
                return Unauthorized("Token không hợp lệ hoặc thiếu thông tin định danh sinh viên.");
            }

            int currentSinhVienId = int.Parse(sinhVienIdClaim);

            // 2. Thực hiện đăng ký thông qua Stored Procedure với đúng SinhVienId của người đang đăng nhập
            var param = new DynamicParameters();
            param.Add("@SinhVienId", currentSinhVienId, dbType: DbType.Int32);
            param.Add("@LopHocPhanId", req.LopHocPhanId, dbType: DbType.Int32);
            param.Add("@KetQua", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@ThongBao", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);

            await cn.ExecuteAsync("sp_DangKyHocPhan", param, commandType: CommandType.StoredProcedure);

            int ketQua = param.Get<int>("@KetQua");
            string thongBao = param.Get<string>("@ThongBao");

            if (ketQua == 1) return Ok(new { message = "Đăng ký học phần thành công!", lopHocPhanId = req.LopHocPhanId, sinhVienId = currentSinhVienId });

            return BadRequest(new { message = thongBao });
        }
    }
}
