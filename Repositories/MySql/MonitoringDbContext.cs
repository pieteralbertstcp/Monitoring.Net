using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.MySql
{
    public class MonitoringDbContext : IDisposable
    {
        protected monitoringEntities _dbContext;

        protected MonitoringDbContext()
        {
            _dbContext = new monitoringEntities();
        }

        private bool _disposed;

        #region Public methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion


        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
        }

        #endregion
    }
}

