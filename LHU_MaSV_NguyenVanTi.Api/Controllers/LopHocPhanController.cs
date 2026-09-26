using LHU_MaSV_NguyenVanTi.Api.Database;
using LHU_MaSV_NguyenVanTi.Api.Database.Entities;
using LHU_MaSV_NguyenVanTi.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LHU_MaSV_NguyenVanTi.Api.Controllers
{
    [Route("api/lop-hoc-phan")]
    [ApiController]
    public class LopHocPhanController : ControllerBase
    {
        DatabaseContext db;

        public LopHocPhanController(DatabaseContext context)
        {
            db = context;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<LopHocPhanDto>>> GetAll()
        {
            List<LopHocPhanDto> danhSachLopHocPhan = await db.LopHocPhan
                .Select(l => new LopHocPhanDto()
                {
                    LopHocPhanId = l.LopHocPhanId,
                    MonHocId = l.MonHocId,
                    MaLopHP = l.MaLopHP,
                    HocKy = l.HocKy,
                    NamHoc = l.NamHoc,
                    SiSoToiDa = l.SiSoToiDa
                })
                .ToListAsync();

            return Ok(danhSachLopHocPhan);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(LopHocPhanAddDto req)
        {
            LopHocPhanEntity newLopHocPhan = new LopHocPhanEntity()
            {
                MonHocId = req.MonHocId,
                MaLopHP = req.MaLopHP,
                HocKy = req.HocKy,
                NamHoc = req.NamHoc,
                SiSoToiDa = req.SiSoToiDa
            };

            await db.AddAsync(newLopHocPhan);

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(LopHocPhanEditDto req)
        {
            var lopHocPhan = await db.LopHocPhan
                .Where(l => l.LopHocPhanId == req.LopHocPhanId)
                .FirstOrDefaultAsync();

            if (lopHocPhan == null)
            {
                return NotFound();
            }

            lopHocPhan.MonHocId = req.MonHocId;
            lopHocPhan.MaLopHP = req.MaLopHP;
            lopHocPhan.HocKy = req.HocKy;
            lopHocPhan.NamHoc = req.NamHoc;
            lopHocPhan.SiSoToiDa = req.SiSoToiDa;

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int lopHocPhanId)
        {
            var lopHocPhan = await db.LopHocPhan
                .Where(l => l.LopHocPhanId == lopHocPhanId)
                .FirstOrDefaultAsync();

            if (lopHocPhan == null)
            {
                return NotFound();
            }

            db.LopHocPhan.Remove(lopHocPhan);

            await db.SaveChangesAsync();

            return Ok();
        }
    }
}
