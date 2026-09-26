using LHU_MaSV_NguyenVanTi.Api.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LHU_MaSV_NguyenVanTi.Api.Database.Seeds
{
    public class MonHocSeed : IEntityTypeConfiguration<MonHocEntity>
    {
        public void Configure(EntityTypeBuilder<MonHocEntity> builder)
        {
            builder.HasData(Data);
        }

        public static readonly MonHocEntity[] Data = new[]
        {
            new MonHocEntity
            {
                MonHocId = 1,
                MaMon = "IT101",
                TenMon = "Nhập môn lập trình",
                SoTinChi = 3,
                SoTietLyThuyet = 30
            },
            new MonHocEntity
            {
                MonHocId = 2,
                MaMon = "IT102",
                TenMon = "Kỹ thuật lập trình",
                SoTinChi = 3,
                SoTietLyThuyet = 30
            },
            new MonHocEntity
            {
                MonHocId = 3,
                MaMon = "IT103",
                TenMon = "Cấu trúc dữ liệu và giải thuật",
                SoTinChi = 4,
                SoTietLyThuyet = 45
            },
            new MonHocEntity
            {
                MonHocId = 4,
                MaMon = "IT104",
                TenMon = "Cơ sở dữ liệu",
                SoTinChi = 3,
                SoTietLyThuyet = 30
            },
            new MonHocEntity
            {
                MonHocId = 5,
                MaMon = "IT105",
                TenMon = "Hệ quản trị cơ sở dữ liệu",
                SoTinChi = 3,
                SoTietLyThuyet = 30
            },
            new MonHocEntity
            {
                MonHocId = 6,
                MaMon = "IT106",
                TenMon = "Mạng máy tính",
                SoTinChi = 3,
                SoTietLyThuyet = 30
            },
            new MonHocEntity
            {
                MonHocId = 7,
                MaMon = "IT107",
                TenMon = "Lập trình hướng đối tượng",
                SoTinChi = 3,
                SoTietLyThuyet = 30
            },
            new MonHocEntity
            {
                MonHocId = 8,
                MaMon = "IT108",
                TenMon = "Phát triển ứng dụng Web",
                SoTinChi = 3,
                SoTietLyThuyet = 30
            },
            new MonHocEntity
            {
                MonHocId = 9,
                MaMon = "IT109",
                TenMon = "Phát triển ứng dụng di động",
                SoTinChi = 3,
                SoTietLyThuyet = 30
            },
            new MonHocEntity
            {
                MonHocId = 10,
                MaMon = "IT110",
                TenMon = "Kiểm thử phần mềm",
                SoTinChi = 3,
                SoTietLyThuyet = 30
            }
        };
    }
}
