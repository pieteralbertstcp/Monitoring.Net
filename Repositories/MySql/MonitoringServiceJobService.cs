using System.Collections.Generic;
using System.Linq;

namespace Repositories.MySql
{
    public class MonitoringServiceJobService : MonitoringService<monitoring_services_jobs>
    {
        public List<monitoring_services_jobs> GetListOfActiveMonitoringHangFireJobs()
        {
            return Where(x=>x.execute_job == true).ToList();
        }
    }
}
