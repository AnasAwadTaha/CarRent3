using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRent.Data;
using CarRent.Data.Entites;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using CarRent.ViewModels;

namespace CarRent.Controllers
{
    public class CarController : Controller
    {
        private readonly IGenericRepository<Car> _repository;
        private readonly IHostingEnvironment hosting;

        [Obsolete]
        public CarController(IGenericRepository<Car> repository, IHostingEnvironment hosting)
        {
            _repository = repository;
            this.hosting = hosting;
        }

        // GET: Car
        public IActionResult Index()
        {
            var Cars = _repository.GetAll();
            return View(Cars);
        }

        // GET: Car/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = _repository.Get(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            var Model = new CarRentViewModel
            {

            };
            return View(Model);
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public IActionResult Create(CarRentViewModel viewModel)
        {
            try
            {
                string fileName = string.Empty;
                if (ModelState.IsValid)
                {
                    string FileName = UploadFile(viewModel.File) ?? string.Empty;
                    //string uplaods = Path.Combine(hosting.WebRootPath, "Upload");
                    //fileName = viewModel.File.Name;
                    //string fullpath = Path.Combine(uplaods, fileName);
                    //viewModel.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    Car Car = new Car
                    {
                        Id = viewModel.CarId,
                        Color = viewModel.Color,
                        ImageUrl = FileName,
                        PlateNumber = viewModel.PlateNumber,
                        Status = viewModel.Status,
                    };


                _repository.Insert(Car);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ypur Enterd Invalid Data");
            }

            return View(viewModel);
        }

        // GET: Car/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var car = _repository.Get(id);
            var viewModel = new CarRentViewModel
            {
                CarId = car.Id,
                Color = car.Color,
                ImageUrl = car.ImageUrl,
                PlateNumber = car.PlateNumber,
                Status = car.Status,
            };
            return View(viewModel);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarRentViewModel viewModel)
        {
            if (viewModel.CarId == 0)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                string FileName = UploadFile(viewModel.File) == null ? string.Empty : UploadFile(viewModel.File);
                //string Upload = Path.Combine(hosting.WebRootPath, "Upload");
                var car = _repository.Get(viewModel.CarId);
                try
                {
                    Car Car = new Car
                    {
                        Id = viewModel.CarId,
                        Color = viewModel.Color,
                        //ImageUrl = FileName,
                        PlateNumber = viewModel.PlateNumber,
                        Status = viewModel.Status,
                    };
                    _repository.Update(viewModel.CarId, Car);
                    return RedirectToAction(nameof(Index));
                }
                catch 
                {
                    return View();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Car = _repository.Get(id);
            return View(Car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _repository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
           
        }

        public bool CarStatus(int id)
        {
            var car = _repository.Get(id);

            if (car.Status == false)
            {
                return true;
            }
            return false;
        }
        [Obsolete]
        string UploadFile(IFormFile formFile)
        {
            string FileName = formFile.FileName;
            if (FileName != null)
            {
                string Uploads = Path.Combine(hosting.WebRootPath, "Uploads");

                string FullBath = Path.Combine(Uploads, FileName);
                formFile.CopyTo(new FileStream(FullBath, FileMode.Create));
                return formFile.FileName;
            }
            return null;
        }
        [Obsolete]
        string UploadFile(IFormFile file, string ImageUrl)
        {
            if (file != null)
            {
                string Uploads = Path.Combine(hosting.WebRootPath, "Uploads");

                string NewPath = Path.Combine(Uploads, file.FileName);
                //Delete Old File
                string OLdPath = Path.Combine(Uploads, ImageUrl);
                if (NewPath != OLdPath)
                {
                    System.IO.File.Delete(OLdPath);
                    //Save the new File
                    file.CopyTo(new FileStream(NewPath, FileMode.Create));
                }
                return file.FileName;
            }
            return ImageUrl;
        }
    }
}
