using System;
using System.Diagnostics;
using System.Net;
using Interfaces.ServicesInterfaces;
using Models.ServicesModels;
using Repositories.MySql;

namespace Services.MonitoringServices
{

    public sealed class TestFtpConnection : IBasicService
    {
        /// <summary>
        /// Used to check if a FTP connection can be made to the desired host.
        /// </summary>
        /// <param name="jobDetials"></param>
        /// <returns>MonitoringServiceResponse</returns>
        public MonitoringServiceResponse Execute(monitoring_services_responses jobDetials)
        {
            var response = new MonitoringServiceResponse();
            response.Id = Guid.NewGuid().ToString();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var request = (FtpWebRequest)WebRequest.Create("ftp://" + jobDetials.host);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential("USERNAME ", jobDetials.passwrd);
                var ftpResponse = (FtpWebResponse)request.GetResponse();
                response.IsSuccessful = true;
                response.Message = ftpResponse.StatusCode + " " + ftpResponse.StatusDescription;
                ftpResponse.Close();
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
