using System.ComponentModel.DataAnnotations;

namespace LHU_MaSV_NguyenVanTi.Api.Dto
{
    public class DangKyHocPhanAnToanDto
    {
        [Required(ErrorMessage = "Vui lòng chọn lớp học phần cần đăng ký.")]
        public int LopHocPhanId { get; set; }
    }
}
