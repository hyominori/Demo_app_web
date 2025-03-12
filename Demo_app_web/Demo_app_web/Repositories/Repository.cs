using Demo_app_web.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demo_app_web.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected SE06304DemoContext _demoProtectContext;

        protected DbSet<T> _dbSet;

        public Repository(SE06304DemoContext demoProtectContext)
        {
            _demoProtectContext = demoProtectContext;
            _dbSet = _demoProtectContext.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList(); //ToList() method is used to convert the IEnumerable to List
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList(); //LinQ getting records which are fit the condition
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id); //Find() method is used to get the record by id
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity); //Add() method is used to add the record
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity); //Update() method is used to update the record
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity); //Remove() method is used to remove the record
        }

        public virtual void SaveChanges()
        {
            _demoProtectContext.SaveChanges(); //SaveChanges() method is used to save the changes
        }
    }
}
