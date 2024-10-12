using Microsoft.AspNetCore.Mvc;

namespace Web1.Controllers
{
    public class GreenwichController : Controller
    {
        public IActionResult Index()
        {
            //pass data from Controller (Backend) to View (Frontend)

            int year = DateTime.Now.Year;
            string university = "Greenwich Vietnam";
            string[] sports = ["Football", "Volleyball", "Badminton", "Pickle ball"];
            int[] numbers = [10, 30, 49, 28, 18, 79];

            //method 1: ViewBag
            ViewBag.Year = year;
            ViewBag.DaiHoc = university;
            ViewBag.ThanhPho = "Hà Nội";
            ViewBag.Sports = sports;
            ViewBag.Numbers = numbers;

            //method 2: ViewData
            ViewData["PiNumber"] = 3.14;
            ViewData["Pass"] = true;
  
            return View();
        }
    }
}
