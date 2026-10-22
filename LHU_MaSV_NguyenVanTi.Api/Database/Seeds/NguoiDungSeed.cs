using LHU_MaSV_NguyenVanTi.Api.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LHU_MaSV_NguyenVanTi.Api.Database.Seeds
{
    public class NguoiDungSeed : IEntityTypeConfiguration<NguoiDungEntity>
    {
        public void Configure(EntityTypeBuilder<NguoiDungEntity> builder)
        {
            builder.HasData(Data);
        }

        public static readonly NguoiDungEntity[] Data = new[]
        {
            new NguoiDungEntity
            {
                Id = 1,
                TenDangNhap = "daotao",
                MatKhauHash = "$2a$11$ToDL5Apm.cdz0RUOZggDkenlORElj.PA6qx1gZ7G5Yj5XQdhNml46", // MatKhau@123
                VaiTro = "DaoTao",
                SinhVienId = null,
                GiangVienId = null
            },
            new NguoiDungEntity
            {
                Id = 2,
                TenDangNhap = "giangvien",
                MatKhauHash = "$2a$11$ToDL5Apm.cdz0RUOZggDkenlORElj.PA6qx1gZ7G5Yj5XQdhNml46", // MatKhau@123
                VaiTro = "GiangVien",
                SinhVienId = null,
                GiangVienId = 1
            },
            new NguoiDungEntity
            {
                Id = 3,
                TenDangNhap = "sinhvien",
                MatKhauHash = "$2a$11$ToDL5Apm.cdz0RUOZggDkenlORElj.PA6qx1gZ7G5Yj5XQdhNml46", // MatKhau@123
                VaiTro = "SinhVien",
                SinhVienId = 1,
                GiangVienId = null
            }
        };
    }
}
