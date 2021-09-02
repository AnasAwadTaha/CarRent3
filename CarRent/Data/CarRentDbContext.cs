using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarRent.Data.Entites;

namespace CarRent.Data
{
    public class CarRentDbContext : IdentityDbContext
    {
        public CarRentDbContext(DbContextOptions<CarRentDbContext> options)
            : base(options)
        {

        }
        public DbSet<CarRent.Data.Entites.Car> Car { get; set; }
        public DbSet<CarRent.Data.Entites.Rent> Rent { get; set; }
        public DbSet<CarRent.Data.Entites.Customer> Customer { get; set; }
    }
}
