using Models.ServicesModels;

namespace Interfaces.Services

{
    public interface IBasicService
    {
        MonitoringServiceResponse Execute(MonitoringServiceJob jobDetails);
    }
}
