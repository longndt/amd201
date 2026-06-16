using Microsoft.AspNetCore.Mvc;

namespace Web1.Controllers
{
    public class MobileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IPhone()
        {
            return View();
        }

        public IActionResult Samsung()
        {
            return View();
        }
    }
}
