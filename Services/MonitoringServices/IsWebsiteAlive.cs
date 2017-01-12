using System;
using System.Diagnostics;
using System.Net;
using Interfaces.Services;

namespace Services.MonitoringServices
{
    public class IsWebsiteAlive : IBasicService
    {
        /// <summary>
        /// Used to test if a website is alive. Checks for 200 status code.
        /// </summary>
        /// <param name="jobDetails"></param>
        /// <returns></returns>
        public MonitoringServiceResponse Execute(MonitoringServiceJob jobDetails)
        {
            var result = new MonitoringServiceResponse
            {
                Id = Guid.NewGuid().ToString(),
                Host = jobDetails.Host
            };

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                var req = (HttpWebRequest)WebRequest.Create(jobDetails.Host);
                var response = (HttpWebResponse)req.GetResponse();
                result.IsSuccessful = response.StatusCode == HttpStatusCode.OK;
                result.Message = response.StatusCode.ToString();
                response.Close();
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Message = ex.GetBaseException().Message;
            }
            stopwatch.Stop();
            result.MiliSecondsResponse = stopwatch.ElapsedMilliseconds;
            return result;
        }
    }
}
