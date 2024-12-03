using MenOFVarna.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MenOFVarna.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger)
        {
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Men Of Varna";
            ViewData["Message"] = "Welcome to our community!";
            return View();
        }

    }
}
