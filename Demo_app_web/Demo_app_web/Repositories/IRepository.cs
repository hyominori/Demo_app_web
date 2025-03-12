using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Linq.Expressions;

namespace Demo_app_web.Repositories
{
    public interface IRepository<T> where T : class
    {
        /*
         * Get all records of entity
         * @return list of entity
         */
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>>predicate);
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();

    }
}
