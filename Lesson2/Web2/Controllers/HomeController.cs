using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web2.Models;

namespace Web2.Controllers
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
            //pass data to view
            //1. ViewBag
            List<string> students = ["Minh", "Tuan", "Hung", "Lan"];
            ViewBag.Students = students;

            //2. ViewData
            List<int> numbers = [10, 40, 50, 83, 96];
            ViewData["Numbers"] = numbers;
            return View();
        }

        public IActionResult Add()
        {
            //use TempData for redirect
            TempData["Message"] = "Add succeed !";
            //redirect to homepage after successful add
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
