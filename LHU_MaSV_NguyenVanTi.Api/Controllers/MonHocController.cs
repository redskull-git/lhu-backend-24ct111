using LHU_MaSV_NguyenVanTi.Api.Dto;
using LHU_MaSV_NguyenVanTi.Api.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LHU_MaSV_NguyenVanTi.Api.Controllers
{
    [Route("api/mon-hoc")]
    [ApiController]
    public class MonHocController : ControllerBase
    {
        IMonHocService monHocService;

        public MonHocController(IMonHocService monHocService)
        {
            this.monHocService = monHocService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<MonHocDto>>> GetAll()
        {
            var danhSachMonHoc = await monHocService.GetAllAsync();

            return Ok(danhSachMonHoc);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(MonHocAddDto req)
        {
            bool isSuccess = await monHocService.AddAsync(req);

            if (isSuccess == false) return BadRequest();

            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(MonHocEditDto req)
        {
            bool isSuccess = await monHocService.EditAsync(req);

            if (isSuccess == false) return BadRequest();

            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int monHocId)
        {
            bool isSuccess = await monHocService.DeleteAsync(monHocId);

            if (!isSuccess) return BadRequest();

            return Ok();
        }
    }
}
