using FINAL_TEST.Data;
using FINAL_TEST.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FINAL_TEST.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NhaCungCapController : Controller
    {
		public readonly ApplicationDbContext _db;
		public NhaCungCapController(ApplicationDbContext db)
		{
			_db = db;

		}
		public IActionResult Index()
		{
			{
				var nhacungcap = _db.NhaCungCap.ToList();
				ViewBag.NhaCungCap = nhacungcap;
			}
			return View();
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(NhaCungCap nhacungcap)
		{

			_db.NhaCungCap.Add(nhacungcap);
			//Lưu lại
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{
			if (id == 0)
			{
				return NotFound();
			}
			var NhaCungCap = _db.NhaCungCap.Find(id);
			return View(NhaCungCap);
		}
		[HttpPost]
		public IActionResult Edit(NhaCungCap NhaCungCap)
		{

			// Thêm thông tin vào bảng NhaCungCap
			_db.NhaCungCap.Update(NhaCungCap);
			// Lưu lại
			_db.SaveChanges();


			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			if (id == 0)
			{
				return NotFound();
			}
			var NhaCungCap = _db.NhaCungCap.Find(id);
			return View(NhaCungCap);
		}
		[HttpPost]
		public IActionResult DeleteConfirm(int id)
		{

			var NhaCungCap = _db.NhaCungCap.Find(id);
			if (NhaCungCap == null)
			{
				return NotFound();
			}

			// Thêm thông tin vào bảng NhaCungCap
			_db.NhaCungCap.Remove(NhaCungCap);
			// Lưu lại
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Detail(int id)
		{
			if (id == 0)
			{
				return NotFound();
			}
			var NhaCungCap = _db.NhaCungCap.Find(id);
			if (NhaCungCap == null)
			{
				return NotFound();
			}
			return View(NhaCungCap);
		}
	}
}
