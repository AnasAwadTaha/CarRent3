using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarRent.Data.Entites
{
    public class RentRepository : IGenericRepository<Rent>
    {
        private readonly CarRentDbContext _DbContext;
        public RentRepository(CarRentDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public void Delete(int Id)
        {
            var Entity = _DbContext.Set<Rent>().Find(Id);
            _DbContext.Remove(Entity);
        }

        public Rent Get(int id)
        {
            var Entity = _DbContext.Set<Rent>().Find(id);
            return Entity;
        }

        public IEnumerable<Rent> GetAll()
        {
            return _DbContext.Set<Rent>().ToList();
        }

        public void Insert(Rent entity)
        {
            _DbContext.Set<Rent>().Add(entity);
            Save();
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }

        public void Update(int Id, Rent entity)
        {
            var OldEntity = Get(Id);
            if (OldEntity != null)
            {
                Delete(Id);
                Save();
                Insert(entity);
                Save();
            }
            else if (OldEntity == null)
            {
            }
        }

        public IEnumerable<Rent> Find(Expression<Func<Rent, bool>> expression)
        {
            return _DbContext.Set<Rent>().Where(expression);
        }
    }
}
