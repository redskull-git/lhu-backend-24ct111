using LHU_MaSV_NguyenVanTi.Api.Dto;
using LHU_MaSV_NguyenVanTi.Api.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LHU_MaSV_NguyenVanTi.Api.Controllers
{
    [Route("api/lop-hoc-phan")]
    [ApiController]
    public class LopHocPhanController : ControllerBase
    {
        ILopHocPhan lopHocPhanService;

        public LopHocPhanController(ILopHocPhan lopHocPhanService)
        {
            this.lopHocPhanService = lopHocPhanService;
        }

        // Cho phép cả Sinh viên, Giảng viên và Đào tạo cùng xem danh sách lớp học phần
        [Authorize(Roles = "SinhVien,GiangVien,DaoTao")]
        [HttpGet("get-all")]
        public async Task<ActionResult<List<LopHocPhanDto>>> GetAll()
        {
            var danhSachLopHocPhan = await lopHocPhanService.GetAllAsync();

            return Ok(danhSachLopHocPhan);
        }

        // Chỉ có Phòng Đào tạo mới có quyền mở thêm lớp học phần mới
        [Authorize(Roles = "DaoTao")]
        [HttpPost("add")]
        public async Task<IActionResult> Add(LopHocPhanAddDto req)
        {
            bool isSuccess = await lopHocPhanService.AddAsync(req);

            if (isSuccess == false) return BadRequest();

            return Ok();
        }

        // Chỉ có Phòng Đào tạo mới có quyền chỉnh sửa thông tin lớp học phần
        [Authorize(Roles = "DaoTao")]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit(LopHocPhanEditDto req)
        {
            bool isSuccess = await lopHocPhanService.EditAsync(req);

            if (isSuccess == false) return BadRequest();

            return Ok();
        }

        // Chỉ có Phòng Đào tạo mới có quyền xóa lớp học phần
        [Authorize(Roles = "DaoTao")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int lopHocPhanId)
        {
            bool isSuccess = await lopHocPhanService.DeleteAsync(lopHocPhanId);

            if (!isSuccess) return BadRequest();

            return Ok();
        }
    }
}
