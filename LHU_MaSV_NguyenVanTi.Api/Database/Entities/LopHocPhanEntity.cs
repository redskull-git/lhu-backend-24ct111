using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHU_MaSV_NguyenVanTi.Api.Database.Entities
{
    [Table("LopHocPhan")]
    public class LopHocPhanEntity
    {
        [Key]
        public int LopHocPhanId { get; set; }

        public int MonHocId { get; set; }

        [Required]
        [MaxLength(20)]
        public string MaLopHP { get; set; } = null!;

        public int HocKy { get; set; }

        public int NamHoc { get; set; }

        public int SiSoToiDa { get; set; }

        [ForeignKey(nameof(MonHocId))]
        public MonHocEntity MonHoc { get; set; } = null!;

        public ICollection<DangKyEntity> DangKies { get; set; } = new List<DangKyEntity>();
    }
}
