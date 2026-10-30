using LHU_MaSV_NguyenVanTi.Api.Dto;

namespace LHU_MaSV_NguyenVanTi.Api.IServices
{
    public interface ISinhVienService
    {
        Task<List<SinhVienDto>> GetAllAsync();

        Task<SinhVienDto?> GetByIdAsync(int sinhVienId);

        Task<(bool Success, string Message, int? NewId)> AddAsync(SinhVienAddDto req);

        Task<(bool Success, string Message)> EditAsync(SinhVienEditDto req);

        Task<(bool Success, string Message)> DeleteAsync(int sinhVienId);
    }
}
