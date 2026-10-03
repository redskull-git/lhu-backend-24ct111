using LHU_MaSV_NguyenVanTi.Api.Dto;

namespace LHU_MaSV_NguyenVanTi.Api.IServices
{
    public interface IMonHocService
    {
        Task<List<MonHocDto>> GetAllAsync();

        Task<bool> AddAsync(MonHocAddDto req);

        Task<bool> EditAsync(MonHocEditDto req);

        Task<bool> DeleteAsync(int monHocId);
    }
}
