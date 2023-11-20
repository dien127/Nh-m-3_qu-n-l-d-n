using FINAL_TEST.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FINAL_TEST.ViewComponents
{
    public class QuantityViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public QuantityViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
                var totalQuantity = _db.GioHang.Where(gh => gh.ApplicationUserId == claim.Value).Sum(gh => gh.Quantity);

                return View(totalQuantity);
            }

            // Xử lý khi người dùng chưa đăng nhập
            // Hiển thị số lượng giỏ hàng là 0
            return View(0);
        }
    }
}
