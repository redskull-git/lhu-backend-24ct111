using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LHU_MaSV_NguyenVanTi.Api.Migrations
{
    /// <inheritdoc />
    public partial class ThemMoiBangNguoiDung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhauHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SinhVienId = table.Column<int>(type: "int", nullable: true),
                    GiangVienId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NguoiDung_SinhVien_SinhVienId",
                        column: x => x.SinhVienId,
                        principalTable: "SinhVien",
                        principalColumn: "SinhVienId");
                });

            migrationBuilder.InsertData(
                table: "NguoiDung",
                columns: new[] { "Id", "GiangVienId", "MatKhauHash", "SinhVienId", "TenDangNhap", "VaiTro" },
                values: new object[,]
                {
                    { 1, null, "$2a$11$ToDL5Apm.cdz0RUOZggDkenlORElj.PA6qx1gZ7G5Yj5XQdhNml46", null, "daotao", "DaoTao" },
                    { 2, 1, "$2a$11$ToDL5Apm.cdz0RUOZggDkenlORElj.PA6qx1gZ7G5Yj5XQdhNml46", null, "giangvien", "GiangVien" },
                    { 3, null, "$2a$11$ToDL5Apm.cdz0RUOZggDkenlORElj.PA6qx1gZ7G5Yj5XQdhNml46", 1, "sinhvien", "SinhVien" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_SinhVienId",
                table: "NguoiDung",
                column: "SinhVienId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_TenDangNhap",
                table: "NguoiDung",
                column: "TenDangNhap",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NguoiDung");
        }
    }
}
