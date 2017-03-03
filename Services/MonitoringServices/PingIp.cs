using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Interfaces.ServicesInterfaces;
using Models.ServicesModels;
using Repositories.MySql;

namespace Services.MonitoringServices
{
    public sealed class PingIp : IBasicService
    {
        /// <summary>
        /// Used to ping a hostname or IP address.
        /// </summary>
        /// <param name="jobDetails"></param>
        /// <returns></returns>
        public MonitoringServiceResponse Execute(monitoring_services_responses jobDetails)
        {
            var response = new MonitoringServiceResponse();
            response.Id = Guid.NewGuid().ToString();
            response.Host = jobDetails.host;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                var pingReply = new Ping().Send(jobDetails.host);
                response.IsSuccessful = pingReply != null && pingReply.Status == IPStatus.Success;
                if (pingReply != null) response.Message = pingReply.Status.ToString();
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.GetBaseException().Message;
            }
            stopwatch.Stop();
            response.MiliSecondsResponse = stopwatch.ElapsedMilliseconds;
            return response;
        }
    }
}
