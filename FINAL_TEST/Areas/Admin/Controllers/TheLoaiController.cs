using FINAL_TEST.Data;
using FINAL_TEST.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FINAL_TEST.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TheLoaiController : Controller
    {
        public readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;
        public TheLoaiController(ApplicationDbContext db, IWebHostEnvironment appEnvironment)
        {
            _db = db;
            _appEnvironment = appEnvironment;
        }
        public IActionResult Index()
        {
            {
                var theloai = _db.TheLoai.ToList();
                //var theloai = _db.TheLoai.Where(t => t.Id > 3).ToList();  LINQ lấy ra danh sách có id > 3
                ViewBag.TheLoai = theloai;
            };
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TheLoai theloai, IFormFile ImageUrl)
        {


            _db.TheLoai.Add(theloai);
            // Lưu lại
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
            var theloai = _db.TheLoai.Find(id);
            return View(theloai);
        }
        [HttpPost]
        public IActionResult Edit(TheLoai theloai, IFormFile ImageUrl)
        {

            _db.TheLoai.Update(theloai);
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
            var theloai = _db.TheLoai.Find(id);
            return View(theloai);
        }
        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {

            var theloai = _db.TheLoai.Find(id);
            if (theloai == null)
            {
                return NotFound();
            }

            // Thêm thông tin vào bảng TheLoai
            _db.TheLoai.Remove(theloai);
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
            var theloai = _db.TheLoai.Find(id);
            if (theloai == null)
            {
                return NotFound();
            }
            return View(theloai);
        }
    }
}
