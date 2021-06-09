using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SingletonTest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SingletonTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Test t;
        public HomeController(Test t)
        {
            this.t = t;
        }

        public IActionResult Index()
        {
            t.MyProperty = 3;
            return View();
        }

        public IActionResult Privacy()
        {
            t.MyProperty = 5;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
