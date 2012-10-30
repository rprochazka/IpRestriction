using System;
using System.Linq;
using System.Linq.Expressions;

namespace IpRestriction.Logger.Domain.Custom
{
    public interface IRepository<T> where T : class
    {
        void Add(T item);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T GetOne(Expression<Func<T, bool>> predicate);
        void Remove(T item);
        void Remove(Expression<Func<T, bool>> predicate);
    }
}
