using Models.ServicesModels;
using Repositories.MySql;

namespace Interfaces.ServicesInterfaces

{
    public interface IBasicService
    {
        MonitoringServiceResponse Execute(monitoring_services_responses jobDetails);
    }
}
