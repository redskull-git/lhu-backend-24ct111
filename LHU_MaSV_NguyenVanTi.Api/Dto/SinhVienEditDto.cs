using System.ComponentModel.DataAnnotations;

namespace LHU_MaSV_NguyenVanTi.Api.Dto
{
    public class SinhVienEditDto
    {
        [Required(ErrorMessage = "Vui lòng cung cấp SinhVienId")]
        public int SinhVienId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã sinh viên")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Mã sinh viên từ 3 đến 20 ký tự")]
        public string MaSV { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập họ và tên sinh viên")]
        [MaxLength(100, ErrorMessage = "Họ tên sinh viên tối đa 100 ký tự")]
        public string HoTen { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [MaxLength(100, ErrorMessage = "Email tối đa 100 ký tự")]
        public string? Email { get; set; }

        public DateTime? NgaySinh { get; set; }

        [MaxLength(50, ErrorMessage = "Tên lớp sinh hoạt tối đa 50 ký tự")]
        public string? LopSinhHoat { get; set; }
    }
}
