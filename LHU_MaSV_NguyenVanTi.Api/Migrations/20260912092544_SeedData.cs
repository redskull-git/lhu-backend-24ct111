using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LHU_MaSV_NguyenVanTi.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MonHoc",
                columns: new[] { "MonHocId", "MaMon", "SoTietLyThuyet", "SoTinChi", "TenMon" },
                values: new object[,]
                {
                    { 1, "IT101", 30, 3, "Nhập môn lập trình" },
                    { 2, "IT102", 30, 3, "Kỹ thuật lập trình" },
                    { 3, "IT103", 45, 4, "Cấu trúc dữ liệu và giải thuật" },
                    { 4, "IT104", 30, 3, "Cơ sở dữ liệu" },
                    { 5, "IT105", 30, 3, "Hệ quản trị cơ sở dữ liệu" },
                    { 6, "IT106", 30, 3, "Mạng máy tính" },
                    { 7, "IT107", 30, 3, "Lập trình hướng đối tượng" },
                    { 8, "IT108", 30, 3, "Phát triển ứng dụng Web" },
                    { 9, "IT109", 30, 3, "Phát triển ứng dụng di động" },
                    { 10, "IT110", 30, 3, "Kiểm thử phần mềm" }
                });

            migrationBuilder.InsertData(
                table: "LopHocPhan",
                columns: new[] { "LopHocPhanId", "HocKy", "MaLopHP", "MonHocId", "NamHoc", "SiSoToiDa" },
                values: new object[,]
                {
                    { 1, 1, "24DTH01_IT101", 1, 2024, 45 },
                    { 2, 1, "24DTH02_IT101", 1, 2024, 45 },
                    { 3, 2, "24DTH01_IT102", 2, 2024, 40 },
                    { 4, 2, "24DTH02_IT102", 2, 2024, 40 },
                    { 5, 1, "24DTH01_IT103", 3, 2025, 35 },
                    { 6, 1, "24DTH02_IT103", 3, 2025, 35 },
                    { 7, 1, "24DTH01_IT104", 4, 2025, 50 },
                    { 8, 2, "24DTH01_IT105", 5, 2025, 40 },
                    { 9, 1, "24DTH01_IT106", 6, 2025, 45 },
                    { 10, 1, "24DTH01_IT107", 7, 2025, 50 },
                    { 11, 2, "24DTH02_IT107", 7, 2025, 50 },
                    { 12, 2, "24DTH01_IT108", 8, 2025, 40 },
                    { 13, 2, "24DTH02_IT108", 8, 2025, 40 },
                    { 14, 1, "24DTH01_IT109", 9, 2026, 45 },
                    { 15, 2, "24DTH01_IT110", 10, 2026, 40 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MonHoc",
                keyColumn: "MonHocId",
                keyValue: 10);
        }
    }
}
