using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using IPRestriction.Data;
using IpRestriction.Logger.Domain.Custom;

namespace IPRestriction.Logger.Data.Custom
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public EFRepository(Db context)
        {
            _dbSet = context.Set<T>();
        }

        #region IRepository<T> Members

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public T GetOne(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }

        public void Remove(Expression<Func<T, bool>> predicate)
        {
            T[] itemsToDelete = _dbSet.Where(predicate).ToArray();
            foreach (T item in itemsToDelete)
            {
                _dbSet.Remove(item);
            }
        }

        #endregion
    }
}