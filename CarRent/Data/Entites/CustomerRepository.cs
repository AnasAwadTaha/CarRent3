using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarRent.Data.Entites
{
    public class CustomerRepository : IGenericRepository<Customer>
    {
        private readonly CarRentDbContext _DbContext;
        public CustomerRepository(CarRentDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public void Delete(int Id)
        {
            var Entity = _DbContext.Set<Customer>().Find(Id);
            _DbContext.Remove(Entity);
        }

        public Customer Get(int id)
        {
            var Entity = _DbContext.Set<Customer>().Find(id);
            return Entity;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _DbContext.Set<Customer>().ToList();
        }

        public void Insert(Customer entity)
        {
            _DbContext.Set<Customer>().Add(entity);
            Save();
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }

        public void Update(int Id, Customer entity)
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

        public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> expression)
        {
            return _DbContext.Set<Customer>().Where(expression);
        }
    }
}

