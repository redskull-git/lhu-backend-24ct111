using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LHU_MaSV_NguyenVanTi.Api.Migrations
{
    /// <inheritdoc />
    public partial class LHP_NgayBatDau_KetThuc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayBatDau",
                table: "LopHocPhan",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetThuc",
                table: "LopHocPhan",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 1,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 2,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 3,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 4,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 5,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 6,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 7,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 8,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 9,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 10,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 11,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 12,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 13,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 14,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "LopHocPhan",
                keyColumn: "LopHocPhanId",
                keyValue: 15,
                columns: new[] { "NgayBatDau", "NgayKetThuc" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayBatDau",
                table: "LopHocPhan");

            migrationBuilder.DropColumn(
                name: "NgayKetThuc",
                table: "LopHocPhan");
        }
    }
}
