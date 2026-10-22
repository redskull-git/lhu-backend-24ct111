using LHU_MaSV_NguyenVanTi.Api.Database.Entities;

namespace LHU_MaSV_NguyenVanTi.Api.IServices
{
    public interface IJwtTokenService
    {
        string GenerateToken(NguoiDungEntity user, string? hoTen = null);
    }
}
