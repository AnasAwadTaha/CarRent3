using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarRent.Data.Entites
{
    public class CarRepository : IGenericRepository<Car>
    {
        private readonly CarRentDbContext _DbContext;
        public CarRepository(CarRentDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public void Delete(int Id)
        {
            var Entity = _DbContext.Set<Car>().Find(Id);
            _DbContext.Remove(Entity);
        }

        public Car Get(int id)
        {
            var Entity = _DbContext.Set<Car>().Find(id);
            return Entity;
        }

        public IEnumerable<Car> GetAll()
        {
            return _DbContext.Set<Car>().ToList();
        }

        public void Insert(Car entity)
        {
            _DbContext.Set<Car>().Add(entity);
            Save();
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }

        public void Update(int Id, Car entity)
        {
            
            if (Id != 0)
            {
                var Old = Get(Id);
                if (Old.Id != Id)
                    return;
                if (Old != null)
                {
                    Old.Color = entity.Color;
                    Old.ImageUrl = entity.ImageUrl;
                    Old.Status = entity.Status;
                    Old.PlateNumber = entity.PlateNumber;
                    Save();
                }
            }
        }

        public IEnumerable<Car> Find(Expression<Func<Car, bool>> expression)
        {
            return _DbContext.Set<Car>().Where(expression);
        }
    }
}
