using LHU_MaSV_NguyenVanTi.Api.Dto;

namespace LHU_MaSV_NguyenVanTi.Api.IServices
{
    public interface ILopHocPhan
    {
        Task<List<LopHocPhanDto>> GetAllAsync();

        Task<bool> AddAsync(LopHocPhanAddDto req);

        Task<bool> EditAsync(LopHocPhanEditDto req);

        Task<bool> DeleteAsync(int lopHocPhanId);
    }
}
