using CarRent.Data.Entites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.ViewModels
{
    public class CarRentViewModel
    {
     
        public int CarId { get; set; }
        public string PlateNumber { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }



        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Identity { get; set; }
        public string PhoneNumber { get; set; }
   


        public List <Car> Cars{ get; set; }
        public IFormFile File { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
