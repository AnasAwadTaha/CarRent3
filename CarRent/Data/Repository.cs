using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarRent.Data
{
    public class Repository<T> : IGenericRepository<T> where T  : class
    {
        private readonly CarRentDbContext _DbContext;
        public Repository(CarRentDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public void Delete(int Id)
        {
            var Entity = _DbContext.Set<T>().Find(Id);
            _DbContext.Remove(Entity);
        }

        public T Get(int id)
        {
            var Entity = _DbContext.Set<T>().Find(id);
            return Entity;
        }

        public IEnumerable<T> GetAll()
        {
           return  _DbContext.Set<T>().ToList();
        }

        public void Insert(T entity)
        {
            _DbContext.Set<T>().Add(entity);
            Save();
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }

        public void Update(int Id, T entity)
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

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _DbContext.Set<T>().Where(expression);
        }
    }
}
