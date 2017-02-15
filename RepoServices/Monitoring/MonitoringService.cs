using System;
using System.Linq;
using System.Linq.Expressions;
using Repositories;

namespace RepoServices.Monitoring
{
    public class MonitoringService<T> : monitoringEntities where T : class
    {
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("Predicate value must be passed to FindBy<T,TKey>.");
            return _dbContext.Set<T>().Where(predicate).AsQueryable<T>(); ;
        }
    }
}
