using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHU_MaSV_NguyenVanTi.Api.Database.Entities
{
    [Table("SinhVien")]
    public class SinhVienEntity
    {
        [Key]
        public int SinhVienId { get; set; }

        [Required]
        [StringLength(20)]
        public string MaSV { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = null!;

        [StringLength(100)]
        public string Email { get; set; } = "";

        public DateTime? NgaySinh { get; set; } = null;

        public string LopSinhHoat { get; set; } = "";

        public ICollection<DangKyEntity> DangKies { get; set; } = new List<DangKyEntity>();
    }
}
