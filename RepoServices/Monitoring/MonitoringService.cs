using System;
using System.Linq;
using System.Linq.Expressions;
using Repositories;
using Repositories.MySql;

namespace RepoServices.Monitoring
{
    public class MonitoringService<T> : monitoringEntities where T : class
    {
    }
}
