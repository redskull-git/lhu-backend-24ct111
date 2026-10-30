using LHU_MaSV_NguyenVanTi.Api.Dto;
using LHU_MaSV_NguyenVanTi.Api.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LHU_MaSV_NguyenVanTi.Api.Controllers
{
    [Route("api/sinh-vien")]
    [ApiController]
    public class SinhVienController : ControllerBase
    {
        private readonly ISinhVienService _sinhVienService;
        private readonly ILogger<SinhVienController> _logger;

        public SinhVienController(ISinhVienService sinhVienService, ILogger<SinhVienController> logger)
        {
            _sinhVienService = sinhVienService;
            _logger = logger;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách sinh viên.
        /// Cho phép cả SinhVien, GiangVien và DaoTao truy cập.
        /// </summary>
        [Authorize(Roles = "SinhVien,GiangVien,DaoTao")]
        [HttpGet("get-all")]
        public async Task<ActionResult<List<SinhVienDto>>> GetAll()
        {
            _logger.LogInformation("Người dùng truy vấn danh sách toàn bộ sinh viên.");
            var danhSach = await _sinhVienService.GetAllAsync();
            return Ok(danhSach);
        }

        /// <summary>
        /// Lấy thông tin chi tiết một sinh viên theo ID.
        /// Cho phép cả SinhVien, GiangVien và DaoTao truy cập.
        /// </summary>
        [Authorize(Roles = "SinhVien,GiangVien,DaoTao")]
        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<SinhVienDto>> GetById(int id)
        {
            var sinhVien = await _sinhVienService.GetByIdAsync(id);
            if (sinhVien == null)
            {
                return NotFound(new { message = $"Không tìm thấy sinh viên có ID = {id}." });
            }

            return Ok(sinhVien);
        }

        /// <summary>
        /// Thêm mới hồ sơ sinh viên vào hệ thống.
        /// Chỉ có vai trò DaoTao mới có thẩm quyền thực hiện.
        /// </summary>
        [Authorize(Roles = "DaoTao")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] SinhVienAddDto req)
        {
            _logger.LogInformation("Phòng Đào tạo yêu cầu thêm mới sinh viên với MaSV: {MaSV}", req.MaSV);
            var (success, message, newId) = await _sinhVienService.AddAsync(req);

            if (!success)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message, sinhVienId = newId });
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên theo SinhVienId.
        /// Chỉ có vai trò DaoTao mới có thẩm quyền thực hiện.
        /// </summary>
        [Authorize(Roles = "DaoTao")]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] SinhVienEditDto req)
        {
            _logger.LogInformation("Phòng Đào tạo cập nhật thông tin sinh viên ID: {SinhVienId}", req.SinhVienId);
            var (success, message) = await _sinhVienService.EditAsync(req);

            if (!success)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }

        /// <summary>
        /// Xóa sinh viên khỏi hệ thống (có kiểm tra ràng buộc đăng ký môn học và tài khoản người dùng).
        /// Chỉ có vai trò DaoTao mới có thẩm quyền thực hiện.
        /// </summary>
        [Authorize(Roles = "DaoTao")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int sinhVienId)
        {
            _logger.LogInformation("Phòng Đào tạo yêu cầu xóa sinh viên ID: {SinhVienId}", sinhVienId);
            var (success, message) = await _sinhVienService.DeleteAsync(sinhVienId);

            if (!success)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }
    }
}
