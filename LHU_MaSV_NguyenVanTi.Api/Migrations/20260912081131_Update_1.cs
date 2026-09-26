using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LHU_MaSV_NguyenVanTi.Api.Migrations
{
    /// <inheritdoc />
    public partial class Update_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhan_MonHocId",
                table: "LopHocPhan",
                column: "MonHocId");

            migrationBuilder.AddForeignKey(
                name: "FK_LopHocPhan_MonHoc_MonHocId",
                table: "LopHocPhan",
                column: "MonHocId",
                principalTable: "MonHoc",
                principalColumn: "MonHocId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LopHocPhan_MonHoc_MonHocId",
                table: "LopHocPhan");

            migrationBuilder.DropIndex(
                name: "IX_LopHocPhan_MonHocId",
                table: "LopHocPhan");
        }
    }
}
