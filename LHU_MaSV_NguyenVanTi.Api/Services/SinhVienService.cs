using LHU_MaSV_NguyenVanTi.Api.Database;
using LHU_MaSV_NguyenVanTi.Api.Database.Entities;
using LHU_MaSV_NguyenVanTi.Api.Dto;
using LHU_MaSV_NguyenVanTi.Api.IServices;
using Microsoft.EntityFrameworkCore;

namespace LHU_MaSV_NguyenVanTi.Api.Services
{
    public class SinhVienService : ISinhVienService
    {
        private readonly DatabaseContext db;

        public SinhVienService(DatabaseContext context)
        {
            db = context;
        }

        public async Task<List<SinhVienDto>> GetAllAsync()
        {
            return await db.SinhViens
                .AsNoTracking()
                .Select(s => new SinhVienDto
                {
                    SinhVienId = s.SinhVienId,
                    MaSV = s.MaSV,
                    HoTen = s.HoTen,
                    Email = s.Email,
                    NgaySinh = s.NgaySinh,
                    LopSinhHoat = s.LopSinhHoat
                })
                .ToListAsync();
        }

        public async Task<SinhVienDto?> GetByIdAsync(int sinhVienId)
        {
            return await db.SinhViens
                .AsNoTracking()
                .Where(s => s.SinhVienId == sinhVienId)
                .Select(s => new SinhVienDto
                {
                    SinhVienId = s.SinhVienId,
                    MaSV = s.MaSV,
                    HoTen = s.HoTen,
                    Email = s.Email,
                    NgaySinh = s.NgaySinh,
                    LopSinhHoat = s.LopSinhHoat
                })
                .FirstOrDefaultAsync();
        }

        public async Task<(bool Success, string Message, int? NewId)> AddAsync(SinhVienAddDto req)
        {
            bool tonTaiMaSV = await db.SinhViens
                .AnyAsync(s => s.MaSV == req.MaSV);

            if (tonTaiMaSV)
            {
                return (false, $"Mã sinh viên '{req.MaSV}' đã tồn tại trong hệ thống.", null);
            }

            var newSinhVien = new SinhVienEntity
            {
                MaSV = req.MaSV.Trim(),
                HoTen = req.HoTen.Trim(),
                Email = req.Email?.Trim() ?? "",
                NgaySinh = req.NgaySinh,
                LopSinhHoat = req.LopSinhHoat?.Trim() ?? ""
            };

            await db.SinhViens.AddAsync(newSinhVien);
            int affected = await db.SaveChangesAsync();

            if (affected > 0)
            {
                return (true, "Thêm mới sinh viên thành công.", newSinhVien.SinhVienId);
            }

            return (false, "Thêm mới sinh viên thất bại.", null);
        }

        public async Task<(bool Success, string Message)> EditAsync(SinhVienEditDto req)
        {
            var sinhVien = await db.SinhViens
                .FirstOrDefaultAsync(s => s.SinhVienId == req.SinhVienId);

            if (sinhVien == null)
            {
                return (false, $"Không tìm thấy sinh viên có ID = {req.SinhVienId}.");
            }

            bool tonTaiMaSV = await db.SinhViens
                .AnyAsync(s => s.MaSV == req.MaSV && s.SinhVienId != req.SinhVienId);

            if (tonTaiMaSV)
            {
                return (false, $"Mã sinh viên '{req.MaSV}' đã trùng với sinh viên khác.");
            }

            sinhVien.MaSV = req.MaSV.Trim();
            sinhVien.HoTen = req.HoTen.Trim();
            sinhVien.Email = req.Email?.Trim() ?? "";
            sinhVien.NgaySinh = req.NgaySinh;
            sinhVien.LopSinhHoat = req.LopSinhHoat?.Trim() ?? "";

            await db.SaveChangesAsync();
            return (true, "Cập nhật thông tin sinh viên thành công.");
        }

        public async Task<(bool Success, string Message)> DeleteAsync(int sinhVienId)
        {
            var sinhVien = await db.SinhViens
                .FirstOrDefaultAsync(s => s.SinhVienId == sinhVienId);

            if (sinhVien == null)
            {
                return (false, $"Không tìm thấy sinh viên có ID = {sinhVienId}.");
            }

            bool coDangKy = await db.DangKies
                .AnyAsync(d => d.SinhVienId == sinhVienId);

            if (coDangKy)
            {
                return (false, "Không thể xóa: Sinh viên đã có dữ liệu đăng ký học phần trong hệ thống.");
            }

            bool coTaiKhoan = await db.NguoiDungs
                .AnyAsync(u => u.SinhVienId == sinhVienId);

            if (coTaiKhoan)
            {
                return (false, "Không thể xóa: Sinh viên đang được liên kết với một tài khoản người dùng.");
            }

            db.SinhViens.Remove(sinhVien);
            int affected = await db.SaveChangesAsync();

            return affected > 0
                ? (true, "Xóa sinh viên thành công.")
                : (false, "Xóa sinh viên thất bại.");
        }
    }
}
