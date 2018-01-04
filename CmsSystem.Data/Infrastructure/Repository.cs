using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CmsSystem.Data.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Properties

        private UserSystemDbContext _dataContext;
        private readonly IDbSet<T> _dbSet;

        protected IDbFactory DbFactory
        {
            get;
        }

        protected UserSystemDbContext DbContext
        {
            get { return _dataContext ?? (_dataContext = DbFactory.Init()); }
        }

        #endregion Properties

        protected Repository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public T GetSingleById(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}