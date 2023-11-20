using Microsoft.AspNetCore.Mvc;

namespace FINAL_TEST.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BlogDetail()
        {
            return View();
        }
    }
}
