using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories.MySql
{
    public class MonitoringService<T> : MonitoringDbContext where T : class
    {
        #region Public methods

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate, int recordCount)
        {
            if (predicate != null)
            {
                return _dbContext.Set<T>().Where(predicate).AsQueryable<T>().Take(recordCount);
            }

            throw new ArgumentNullException("Predicate value must be passed to FindBy<T,TKey>.");
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public T GetSingleBy(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
            {
                return _dbContext.Set<T>().Where(predicate).SingleOrDefault();
            }

            throw new ArgumentNullException("Predicate value must be passed to FindSingleBy<T>.");
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
            {
                return _dbContext.Set<T>().Where(predicate).AsQueryable<T>(); ;
            }

            throw new ArgumentNullException("Predicate value must be passed to FindBy<T,TKey>.");
        }


        public T CreateRecord(T entity)
        {
            var savedEntity = _dbContext.Set<T>().Add(entity);
            var id = _dbContext.SaveChanges();
            if (id != 1)
                return null;
            return savedEntity;
        }

     /*   public T UpdateUpdate(T entity)
        {
            _dbContext.Entry<T>(entity).State = System.Data.EntityState.Modified;

            var updateReturn = _dbContext.SaveChanges();
            if (updateReturn != 1)
                return null;
            return entity;
        }*/

        #endregion
    }
}
