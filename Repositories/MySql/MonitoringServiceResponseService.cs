using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.MySql
{
    public class MonitoringServiceResponseService : MonitoringService<monitoring_services_responses>
    {
        public void CreateNewResponseRecord(monitoring_services_responses newRecord)
        {
            if (newRecord.id == null) newRecord.id = Guid.NewGuid().ToString();
            CreateRecord(newRecord);
        }
    }
}
