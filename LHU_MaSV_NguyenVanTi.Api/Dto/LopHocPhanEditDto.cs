using System.ComponentModel.DataAnnotations;

namespace LHU_MaSV_NguyenVanTi.Api.Dto
{
    public class LopHocPhanEditDto : IValidatableObject
    {
        public int LopHocPhanId { get; set; }

        public int MonHocId { get; set; }

        public string MaLopHP { get; set; } = "";

        public int HocKy { get; set; }

        public int NamHoc { get; set; }

        public int SiSoToiDa { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NgayBatDau != null && NgayKetThuc != null)
            {
                if (NgayKetThuc <= NgayBatDau)
                {
                    yield return new ValidationResult("Ngày kết thúc phải lớn hơn ngày bắt đầu");
                }
            }
        }
    }
}
