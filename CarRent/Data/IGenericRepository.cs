using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarRent.Data
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Insert(T entity);
        void Update(int Id, T entity);
        void Delete(int Id);
        void Save();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    }
}
