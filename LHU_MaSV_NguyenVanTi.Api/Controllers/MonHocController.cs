using LHU_MaSV_NguyenVanTi.Api.Database;
using LHU_MaSV_NguyenVanTi.Api.Database.Entities;
using LHU_MaSV_NguyenVanTi.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LHU_MaSV_NguyenVanTi.Api.Controllers
{
    [Route("api/mon-hoc")]
    [ApiController]
    public class MonHocController : ControllerBase
    {
        DatabaseContext db;

        public MonHocController(DatabaseContext context)
        {
            db = context;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<MonHocDto>>> GetAll()
        {
            List<MonHocDto> danhSachMonHoc = await db.MonHoc
                .Select(m => new MonHocDto()
                {
                    MonHocId = m.MonHocId,
                    MaMon = m.MaMon,
                    TenMon = m.TenMon,
                    SoTinChi = m.SoTinChi,
                    SoTietLyThuyet = m.SoTietLyThuyet
                })
                .ToListAsync();

            return Ok(danhSachMonHoc);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(MonHocAddDto req)
        {
            MonHocEntity newMonHoc = new MonHocEntity()
            {
                MaMon = req.MaMon,
                TenMon = req.TenMon,
                SoTinChi = req.SoTinChi,
                SoTietLyThuyet = req.SoTietLyThuyet
            };

            await db.AddAsync(newMonHoc);

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(MonHocEditDto req)
        {
            var monHoc = await db.MonHoc
                .Where(m => m.MonHocId == req.MonHocId)
                .FirstOrDefaultAsync();

            if (monHoc == null)
            {
                return NotFound();
            }

            monHoc.MaMon = req.MaMon;
            monHoc.TenMon = req.TenMon;
            monHoc.SoTinChi = req.SoTinChi;
            monHoc.SoTietLyThuyet = req.SoTietLyThuyet;

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int monHocId)
        {
            var monHoc = await db.MonHoc
                .Where(m => m.MonHocId == monHocId)
                .FirstOrDefaultAsync();

            if (monHoc == null)
            {
                return NotFound();
            }

            db.MonHoc.Remove(monHoc);

            await db.SaveChangesAsync();

            return Ok();
        }
    }
}
