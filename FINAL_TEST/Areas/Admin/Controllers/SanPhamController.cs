using FINAL_TEST.Data;
using FINAL_TEST.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINAL_TEST.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SanPhamController : Controller
    {
		private readonly ApplicationDbContext _db;
		private readonly IWebHostEnvironment _appEnvironment;

		public SanPhamController(ApplicationDbContext db, IWebHostEnvironment appEnvironment)
		{
			_db = db;
			_appEnvironment = appEnvironment;
		}
		public IActionResult Index()
		{
			var productsWithNhaCungCap = _db.SanPham.Include(sp => sp.NhaCungCap).ToList();
			return View(productsWithNhaCungCap);
		}
		[HttpGet]
		public IActionResult Upsert(int id)
		{
			ViewBag.id = id;
			ViewBag.theloai = _db.TheLoai.ToList();
			ViewBag.nhaCungCaps = _db.NhaCungCap.ToList();

			if (id == 0)
			{

				return View();
			}
			else
			{
				var a = _db.SanPham.Find(id);
				return View(a);
			}
		}
		[HttpPost]
		public IActionResult Upsert(SanPham sanpham, IFormFile ImageUrl)
		{
			ViewBag.theloai = _db.TheLoai.ToList();
			ViewBag.nhaCungCaps = _db.NhaCungCap.ToList();

			if (sanpham.Id == 0)
			{
				//Tên tệp tin gốc của tệp ảnh vừa tải lên
				var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageUrl.FileName);

				//_appEnvironment.WebRootPath cho biết thư mục gốc của ứng dụng web, và "images" là thư mục để lưu ảnh
				var imagePath = Path.Combine(_appEnvironment.WebRootPath, "images", uniqueFileName);

				//FileStream  sao chép nội dung của tệp ảnh từ ImageUrl vào lưu trữ tệp ảnh trên máy chủ.
				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					ImageUrl.CopyTo(stream);
				}
				sanpham.ImageUrl = uniqueFileName;
				_db.SanPham.Add(sanpham);

			}
			else
			{
				//Tên tệp tin gốc của tệp ảnh vừa tải lên
				var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageUrl.FileName);

				//_appEnvironment.WebRootPath cho biết thư mục gốc của ứng dụng web, và "images" là thư mục để lưu ảnh
				var imagePath = Path.Combine(_appEnvironment.WebRootPath, "images", uniqueFileName);

				//FileStream  sao chép nội dung của tệp ảnh từ ImageUrl vào lưu trữ tệp ảnh trên máy chủ.
				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					ImageUrl.CopyTo(stream);
				}
				sanpham.ImageUrl = uniqueFileName;
				_db.SanPham.Update(sanpham);
			}

			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{

			var sanpham = _db.SanPham.FirstOrDefault(sp => sp.Id == id);
			if (sanpham == null)
			{
				return NotFound();
			}
			_db.SanPham.Remove(sanpham);
			_db.SaveChanges();
			return Json(new { success = true });
		}

		[HttpGet]
		public IActionResult Detail(int id)
		{
			if (id == 0)
			{
				return NotFound();
			}
			var sanpham = _db.SanPham.Find(id);
			if (sanpham == null)
			{
				return NotFound();
			}
			return View(sanpham);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var product = _db.SanPham.Find(id);

			if (product == null)
			{
				return NotFound();
			}

			ViewBag.theloai = _db.TheLoai.ToList();
			ViewBag.nhaCungCaps = _db.NhaCungCap.ToList();

			return View(product);
		}

		[HttpPost]
		public IActionResult Edit(SanPham product)
		{
			if (ModelState.IsValid)
			{
				_db.SanPham.Update(product);
				_db.SaveChanges();

				return RedirectToAction("Index");
			}

			// If model state is not valid, return to the form for corrections
			ViewBag.theloai = _db.TheLoai.ToList();
			ViewBag.nhaCungCaps = _db.NhaCungCap.ToList();

			return View(product);
		}
	}
}
