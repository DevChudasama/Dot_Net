using Folio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Folio.Controllers
{
    public class HomeController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Portfolio()
        {
            return View();
        }

        public IActionResult Journal()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Journal_2()
        {
            return View();
        }

        public IActionResult Portfolio_Details()
        {
            return View();
        }
    }
}