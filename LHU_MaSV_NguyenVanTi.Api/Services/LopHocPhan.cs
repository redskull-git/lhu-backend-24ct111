using LHU_MaSV_NguyenVanTi.Api.Database;
using LHU_MaSV_NguyenVanTi.Api.Database.Entities;
using LHU_MaSV_NguyenVanTi.Api.Dto;
using LHU_MaSV_NguyenVanTi.Api.IServices;
using Microsoft.EntityFrameworkCore;

namespace LHU_MaSV_NguyenVanTi.Api.Services
{
    public class LopHocPhan : ILopHocPhan
    {
        DatabaseContext db;

        public LopHocPhan(DatabaseContext context)
        {
            db = context;
        }

        public async Task<List<LopHocPhanDto>> GetAllAsync()
        {
            var danhSachLopHocPhan = await db.LopHocPhan
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

            return danhSachLopHocPhan;
        }

        public async Task<bool> AddAsync(LopHocPhanAddDto req)
        {
            bool tonTaiMaLopHP = await db.LopHocPhan
                .Where(l => l.MaLopHP == req.MaLopHP)
                .AnyAsync();

            if (tonTaiMaLopHP)
            {
                return false;
            }

            bool tonTaiMonHoc = await db.MonHoc
                .Where(m => m.MonHocId == req.MonHocId)
                .AnyAsync();

            if (!tonTaiMonHoc)
            {
                return false;
            }

            LopHocPhanEntity newLopHocPhan = new LopHocPhanEntity()
            {
                MonHocId = req.MonHocId,
                MaLopHP = req.MaLopHP,
                HocKy = req.HocKy,
                NamHoc = req.NamHoc,
                SiSoToiDa = req.SiSoToiDa
            };

            await db.AddAsync(newLopHocPhan);

            int affected = await db.SaveChangesAsync();

            return affected > 0;
        }

        public async Task<bool> EditAsync(LopHocPhanEditDto req)
        {
            var lopHocPhan = await db.LopHocPhan
                .Where(l => l.LopHocPhanId == req.LopHocPhanId)
                .FirstOrDefaultAsync();

            if (lopHocPhan == null)
            {
                return false;
            }

            bool tonTaiMaLopHP = await db.LopHocPhan
                .Where(l => l.MaLopHP == req.MaLopHP && l.LopHocPhanId != req.LopHocPhanId)
                .AnyAsync();

            if (tonTaiMaLopHP)
            {
                return false;
            }

            bool tonTaiMonHoc = await db.MonHoc
                .Where(m => m.MonHocId == req.MonHocId)
                .AnyAsync();

            if (!tonTaiMonHoc)
            {
                return false;
            }

            lopHocPhan.MonHocId = req.MonHocId;
            lopHocPhan.MaLopHP = req.MaLopHP;
            lopHocPhan.HocKy = req.HocKy;
            lopHocPhan.NamHoc = req.NamHoc;
            lopHocPhan.SiSoToiDa = req.SiSoToiDa;

            await db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int lopHocPhanId)
        {
            bool tonTaiDangKy = await db.DangKies
                .Where(d => d.LopHocPhanId == lopHocPhanId)
                .AnyAsync();

            if (tonTaiDangKy)
            {
                return false;
            }

            var lopHocPhan = await db.LopHocPhan
                .Where(l => l.LopHocPhanId == lopHocPhanId)
                .FirstOrDefaultAsync();

            if (lopHocPhan == null)
            {
                return false;
            }

            db.LopHocPhan.Remove(lopHocPhan);

            int affected = await db.SaveChangesAsync();

            return affected > 0;
        }
    }
}
