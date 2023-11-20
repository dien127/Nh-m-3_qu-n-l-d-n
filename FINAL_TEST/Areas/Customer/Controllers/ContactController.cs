using Microsoft.AspNetCore.Mvc;

namespace FINAL_TEST.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
