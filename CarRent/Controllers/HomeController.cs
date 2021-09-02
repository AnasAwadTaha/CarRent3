using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarRent.Models;
using Microsoft.AspNetCore.Authorization;
using CarRent.Data;
using CarRent.Data.Entites;

namespace CarRent.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Car> repository;

        public HomeController(ILogger<HomeController> logger, IGenericRepository<Car> repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        public IActionResult Index()
        {
           var cars = repository.GetAll();

            return View(cars);
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
