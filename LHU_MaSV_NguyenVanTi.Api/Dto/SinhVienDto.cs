namespace LHU_MaSV_NguyenVanTi.Api.Dto
{
    public class SinhVienDto
    {
        public int SinhVienId { get; set; }

        public string MaSV { get; set; } = "";

        public string HoTen { get; set; } = "";

        public string? Email { get; set; }

        public DateTime? NgaySinh { get; set; }

        public string? LopSinhHoat { get; set; }
    }
}
