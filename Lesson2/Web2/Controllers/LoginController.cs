using Microsoft.AspNetCore.Mvc;
using Web2.Models;

namespace Web2.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            //render form login for user to input
            return View("~/Views/Home/Login.cshtml");
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            //check login
            if (username == "admin" && password == "123456")
            {
                TempData["Success"] = "Login succeed !";
                //login succeed : redirect to homepage
                return RedirectToAction("Index", "Home");
            } else
            {
                TempData["Fail"] = "Login failed !";
                //login failed : redirect to login page
                return RedirectToAction("Index");
            }
        }
    }
}
