using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CmsSystem.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(int id);

        void Delete(T entity);

        T GetSingleById(int id);

        T GetSingleByCondition(Expression<Func<T, bool>> expression);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate);
    }
}
