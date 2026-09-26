using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHU_MaSV_NguyenVanTi.Api.Database.Entities
{
    [Table("DangKy")]
    public class DangKyEntity
    {
        [Key]
        public int DangKyId { get; set; }

        public int SinhVienId { get; set; }

        public int LopHocPhanId { get; set; }

        public DateTime NgayDangKy { get; set; }

        public string TrangThai { get; set; } = "";

        [ForeignKey(nameof(SinhVienId))]
        public SinhVienEntity SinhVien { get; set; } = null!;

        [ForeignKey(nameof(LopHocPhanId))]
        public LopHocPhanEntity LopHocPhan { get; set; } = null!;
    }
}
