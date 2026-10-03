using LHU_MaSV_NguyenVanTi.Api.Dto;
using LHU_MaSV_NguyenVanTi.Api.IServices;
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

        [HttpGet("get-all")]
        public async Task<ActionResult<List<LopHocPhanDto>>> GetAll()
        {
            var danhSachLopHocPhan = await lopHocPhanService.GetAllAsync();

            return Ok(danhSachLopHocPhan);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(LopHocPhanAddDto req)
        {
            bool isSuccess = await lopHocPhanService.AddAsync(req);

            if (isSuccess == false) return BadRequest();

            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(LopHocPhanEditDto req)
        {
            bool isSuccess = await lopHocPhanService.EditAsync(req);

            if (isSuccess == false) return BadRequest();

            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int lopHocPhanId)
        {
            bool isSuccess = await lopHocPhanService.DeleteAsync(lopHocPhanId);

            if (!isSuccess) return BadRequest();

            return Ok();
        }
    }
}
