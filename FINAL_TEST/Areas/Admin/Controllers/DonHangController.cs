using FINAL_TEST.Data;
using FINAL_TEST.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINAL_TEST.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DonHangController : Controller
	{
		private readonly ApplicationDbContext _db;
		public DonHangController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			var hoadon = _db.HoaDon.Include(hd => hd.ApplicationUser).ToList();

			return View(hoadon);
		}
	}
}
