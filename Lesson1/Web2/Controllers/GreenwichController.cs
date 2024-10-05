using Microsoft.AspNetCore.Mvc;

namespace Web2.Controllers
{
    public class GreenwichController : Controller
    {
        //default URL
        //localhost:PORT/Greenwich/Index
        //localhost:PORT/Greenwich

        //custom URL
        //localhost:PORT/GreenwichVietnam

        [Route("/GreenwichVietnam")]
        public IActionResult Index()
        {
            //Views/Greenwich/Index.cshtml
            return View();
        }

        //localhost:PORT/Greenwich/Hanoi
        public IActionResult HaNoi()
        {
            //Views/Greenwich/HaNoi.cshtml
            return View();
        }

        public IActionResult HCMCity()
        {
            return View();
        }

        public IActionResult DaNang()
        {
            return View();
        }

        public IActionResult CanTho()
        {
            return View();
        }
    }
}
