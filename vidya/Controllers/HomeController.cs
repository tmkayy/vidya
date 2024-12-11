using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace vidya.Controllers
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                case 404:
                    return View("Error404");
                case 401:
                    return View("Error401");
                default:
                    return View("Error500");
            }
        }
    }
}