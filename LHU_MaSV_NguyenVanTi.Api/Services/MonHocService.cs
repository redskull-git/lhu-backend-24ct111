using LHU_MaSV_NguyenVanTi.Api.Database;
using LHU_MaSV_NguyenVanTi.Api.Database.Entities;
using LHU_MaSV_NguyenVanTi.Api.Dto;
using LHU_MaSV_NguyenVanTi.Api.IServices;
using Microsoft.EntityFrameworkCore;

namespace LHU_MaSV_NguyenVanTi.Api.Services
{
    public class MonHocService : IMonHocService
    {
        DatabaseContext db;

        public MonHocService(DatabaseContext context)
        {
            db = context;
        }

        public async Task<bool> AddAsync(MonHocAddDto req)
        {
            bool tonTaiMonHoc = await db.MonHoc
                .Where(m => m.MaMon == req.MaMon)
                .AnyAsync();

            if (tonTaiMonHoc == true)
            {
                return false;
            }

            MonHocEntity newMonHoc = new MonHocEntity()
            {
                MaMon = req.MaMon,
                TenMon = req.TenMon,
                SoTinChi = req.SoTinChi,
                SoTietLyThuyet = req.SoTietLyThuyet
            };

            await db.AddAsync(newMonHoc);

            int affected = await db.SaveChangesAsync();

            if (affected > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int monHocId)
        {
            bool tonTaiLopHocPhan = await db.LopHocPhan
                .Where(l => l.MonHocId == monHocId)
                .AnyAsync();

            if (tonTaiLopHocPhan == true)
            {
                return false;
            }

            var monHoc = await db.MonHoc
                .Where(m => m.MonHocId == monHocId)
                .FirstOrDefaultAsync();

            if (monHoc == null)
            {
                return false;
            }

            db.MonHoc.Remove(monHoc);

            int affected = await db.SaveChangesAsync();

            return affected > 0;
        }

        public async Task<bool> EditAsync(MonHocEditDto req)
        {
            var monHoc = await db.MonHoc
                .Where(m => m.MonHocId == req.MonHocId)
                .FirstOrDefaultAsync();

            if (monHoc == null)
            {
                return false;
            }

            bool tonTaiMonHoc = await db.MonHoc
                .Where(m => m.MaMon == req.MaMon && m.MonHocId != req.MonHocId)
                .AnyAsync();

            if (tonTaiMonHoc == true)
            {
                return false;
            }

            monHoc.MaMon = req.MaMon;
            monHoc.TenMon = req.TenMon;
            monHoc.SoTinChi = req.SoTinChi;
            monHoc.SoTietLyThuyet = req.SoTietLyThuyet;

            await db.SaveChangesAsync();

            return true;
        }

        public async Task<List<MonHocDto>> GetAllAsync()
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

            return danhSachMonHoc;
        }
    }
}
