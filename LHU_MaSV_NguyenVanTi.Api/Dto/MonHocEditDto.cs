namespace LHU_MaSV_NguyenVanTi.Api.Dto
{
    public class MonHocEditDto
    {
        public int MonHocId { get; set; }

        public string MaMon { get; set; } = "";

        public string TenMon { get; set; } = "";

        public int SoTinChi { get; set; }

        public int SoTietLyThuyet { get; set; }
    }
}
