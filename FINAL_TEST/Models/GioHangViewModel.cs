namespace FINAL_TEST.Models
{
    public class GioHangViewModel
    {
        public IEnumerable<GioHang> DsGioHang { get; set; }

        public HoaDon HoaDon { get; set; }
        public double TotalPrice { get; set; }
    }
}
