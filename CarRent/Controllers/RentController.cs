using CarRent.Data;
using CarRent.Data.Entites;
using CarRent.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarRent.Controllers
{
    public class RentController : Controller
    {
        private readonly IGenericRepository<Rent> _Rentrepository;
        private readonly IGenericRepository<Car> _Carrepository;
        private readonly IGenericRepository<Customer> _CustomerRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public RentController(IGenericRepository<Rent> repository, IGenericRepository<Car> Carrepository, IGenericRepository<Customer> CustomerRepository, UserManager<IdentityUser> userManager)
        {
            _Rentrepository = repository;
            _Carrepository = Carrepository;
            _CustomerRepository = CustomerRepository;
            _userManager = userManager;
        }
        public IActionResult Index()
        { 
            return View();
        }
        [HttpGet]
        public IActionResult Create(int Id)
        {
            var Car = _Carrepository.Get(Id);
            var viewModel = new CarRentViewModel();
            viewModel.CarId = Id;
            viewModel.PlateNumber = Car.PlateNumber;
            viewModel.CustomerName = HttpContext.User.Identity.Name;
            viewModel.StartDate = DateTime.Now;
            viewModel.EndDate = DateTime.Now;
            viewModel.ImageUrl = Car.ImageUrl;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CarRentViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name);// will give the user's userName
            var customerId = _CustomerRepository.Find(a => a.CustomerName == userName).First().Id;
            if (ModelState.IsValid)
            {
                Rent rent = new Rent()
                {
                    CarId = viewModel.CarId,
                    StartDate = viewModel.StartDate,
                    EndDate = viewModel.EndDate,
                    CustomerId = customerId,
                };
                _Rentrepository.Insert(rent);
               var Car = _Carrepository.Get(viewModel.CarId);
                Car carEntity = new Car()
                {
                    //Id = Car.Id,
                    Color = Car.Color,
                    ImageUrl = Car.ImageUrl,
                    PlateNumber = Car.PlateNumber,
                    Status = true,
                };
                _Carrepository.Update(viewModel.CarId, carEntity);
            }
            return RedirectToAction("Index","Home");
        }
    }
}
