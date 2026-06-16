using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiMvcDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGreeter _greeter;
        private readonly ILogger<HomeController> _logger;

        // Constructor injection: the DI container provides these instances
        public HomeController(IGreeter greeter, ILogger<HomeController> logger)
        {
            _greeter = greeter;
            _logger = logger;
        }

        // GET: /
        public IActionResult Index()
        {
            // Resolve and use the injected service
            var message = _greeter.Greet("AMD201");
            _logger.LogInformation("Greeting generated: {Message}", message);

            // Pass data to the view
            ViewBag.Message = message;
            return View();
        }
    }
}
