using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.Data.Entites
{
    public class Car
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
    }
}
