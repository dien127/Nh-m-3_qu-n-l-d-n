using FINAL_TEST.Data;
using FINAL_TEST.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace FINAL_TEST.ViewComponents
{
    public class SummonneyViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public SummonneyViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            // Kiểm tra xem claim có null hay không
            if (claim != null)
            {
                GioHangViewModel giohang = new GioHangViewModel()
                {
                    DsGioHang = _db.GioHang.Include("SanPham").Where(gh => gh.ApplicationUserId == claim.Value).ToList()
                };

                foreach (var item in giohang.DsGioHang)
                {
                    item.ProductPrice = item.Quantity * item.SanPham.Price;
                    // Cộng thêm tổng thuế và phí ship cho TotalPrice
                    giohang.TotalPrice += item.ProductPrice;
                }




                return View(giohang);
            }

            // Xử lý khi người dùng chưa đăng nhập
            // Ví dụ: Hiển thị giỏ hàng rỗng hoặc chuyển hướng đến trang đăng nhập
            GioHangViewModel emptyCart = new GioHangViewModel();
            return View(emptyCart);
        }
    }
}
