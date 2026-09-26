using LHU_MaSV_NguyenVanTi.Api.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LHU_MaSV_NguyenVanTi.Api.Database.Seeds
{
    public class LopHocPhanSeed : IEntityTypeConfiguration<LopHocPhanEntity>
    {
        public void Configure(EntityTypeBuilder<LopHocPhanEntity> builder)
        {
            builder.HasData(Data);
        }

        public static readonly LopHocPhanEntity[] Data = new[]
        {
            new LopHocPhanEntity
            {
                LopHocPhanId = 1,
                MonHocId = 1,
                MaLopHP = "24DTH01_IT101",
                HocKy = 1,
                NamHoc = 2024,
                SiSoToiDa = 45
            },
            new LopHocPhanEntity
            {
                LopHocPhanId = 2,
                MonHocId = 1,
                MaLopHP = "24DTH02_IT101",
                HocKy = 1,
                NamHoc = 2024,
                SiSoToiDa = 45
            },
            new LopHocPhanEntity
            {
                LopHocPhanId = 3,
                MonHocId = 2,
                MaLopHP = "24DTH01_IT102",
                HocKy = 2,
                NamHoc = 2024,
                SiSoToiDa = 40
            },
            new LopHocPhanEntity
            {
                LopHocPhanId = 4,
                MonHocId = 2,
                MaLopHP = "24DTH02_IT102",
                HocKy = 2,
                NamHoc = 2024,
                SiSoToiDa = 40
            },
            new LopHocPhanEntity
            {
                LopHocPhanId = 5,
                MonHocId = 3,
                MaLopHP = "24DTH01_IT103",
                HocKy = 1,
                NamHoc = 2025,
                SiSoToiDa = 35
            },
            new LopHocPhanEntity
            {
                LopHocPhanId = 6,
                MonHocId = 3,
                MaLopHP = "24DTH02_IT103",
                HocKy = 1,
                NamHoc = 2025,
                SiSoToiDa = 35
            },
            new LopHocPhanEntity
            {
                LopHocPhanId = 7,
                MonHocId = 4,
                MaLopHP = "24DTH01_IT104",
                HocKy = 1,
                NamHoc = 2025,
                SiSoToiDa = 50
            },
            new LopHocPhanEntity
            {
                LopHocPhanId = 8,
                MonHocId = 5,
                MaLopHP = "24DTH01_IT105",
                HocKy = 2,
                NamHoc = 2025,
                SiSoToiDa = 40
            },
            new LopHocPhanEntity
            {
                LopHocPhanId = 9,
                MonHocId = 6,
                MaLopHP = "24DTH01_IT106",
                HocKy = 1,
                NamHoc = 2025,
                SiSoToiDa = 45
            },
            new LopHocPhanEntity
            {
                LopHocPhanId = 10,
                MonHocId = 7,
                MaLopHP = "24DTH01_IT107",
                HocKy = 1,
                NamHoc = 2025,
                SiSoToiDa = 50
            },
            new LopHocPhanEntity
            {
                LopHocPhanId = 11,
                MonHocId = 7,
                MaLopHP = "24DTH02_IT107",
                HocKy = 2,
                NamHoc = 2025,
                SiSoToiDa = 50
            },
            new LopHocPhanEntity
            {
                LopHocPhanId = 12,
                MonHocId = 8,
                MaLopHP = "24DTH01_IT108",
                HocKy = 2,
                NamHoc = 2025,
                SiSoToiDa = 40
            },
            new LopHocPhanEntity
            {
                LopHocPhanId = 13,
                MonHocId = 8,
                MaLopHP = "24DTH02_IT108",
                HocKy = 2,
                NamHoc = 2025,
                SiSoToiDa = 40
            },
            new LopHocPhanEntity
            {
                LopHocPhanId = 14,
                MonHocId = 9,
                MaLopHP = "24DTH01_IT109",
                HocKy = 1,
                NamHoc = 2026,
                SiSoToiDa = 45
            },
            new LopHocPhanEntity
            {
                LopHocPhanId = 15,
                MonHocId = 10,
                MaLopHP = "24DTH01_IT110",
                HocKy = 2,
                NamHoc = 2026,
                SiSoToiDa = 40
            }
        };
    }
}
