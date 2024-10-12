using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web1.Models;

namespace Web1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        //default url: localhost:PORT/Home/About
        //set custom route (url)
        [Route("/aboutus")]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Demo()
        {
            //default view: Views/Home/Demo.cshtml
            //set custom view
            return View("~/Views/Demo/Test.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
