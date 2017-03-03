using System;
using System.Collections.Generic;
using System.Diagnostics;
using Hangfire;
using Models.ServicesModels;
using Repositories.MySql;

namespace Services
{
    public class Hangfire
    {
        public void LoadAllHangFireJobs()
        {
            try
            {
                using (var service = new MonitoringServiceJobService())
                {
                    var jobs = service.GetListOfActiveMonitoringHangFireJobs();
                    for (var i = 0; i < jobs.Count; i++)
                    {
                        RecurringJob.AddOrUpdate(string.Format("{0} - {1} -> {2}", i, jobs[i].monitoring_services.class_name, jobs[i].host), () => new Hangfire().ExcecuteJob(jobs[i], jobs[i].monitoring_services.class_name), jobs[i].cron, TimeZoneInfo.Local);
                    }
                }
            }
            catch (Exception ex)
            {
                Mail.SendMailNotification(ex);
            }
        }

        /// <summary>
        /// This is the hangfire function that executes all jobs via reflection.
        /// </summary>
        /// <param name="job"></param>
        /// <param name="className"></param>
        public void ExcecuteJob(monitoring_services_jobs job, string className)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(className)) throw new Exception("The class name provided is empty!");
                var clss = className.Trim();

                var Class = Type.GetType("Services.MonitoringServices." + clss);
                if (Class == null) throw new ArgumentException("Unable to locate the required class");

                var constructor = Class.GetConstructor(Type.EmptyTypes);
                if (constructor == null) throw new Exception("Reflection - constructor is null.");

                var magicClassObject = constructor.Invoke(new object[] { });
                var magicMethod = Class.GetMethod("Execute");
                if (magicMethod == null) throw new ArgumentException("Unable to locate the required method");

                var result = (monitoring_services_responses)magicMethod.Invoke(magicClassObject, new object[] { job });
                new MonitoringServiceResponseService().CreateNewResponseRecord(result);

                if (result.is_successful == false && job.notify_on_failure == true)
                    SendFailureNotification(job, result);
            }
            catch (Exception ex)
            {
                Mail.SendMailNotification(ex);
            }
        }


        private static void SendFailureNotification(monitoring_services_jobs job, monitoring_services_responses response)
        {
            // var body = string.Format("Please note that {0} failed to execute.</br>IP address: {1}</br>Please see additional info below</br></br>{2}</br></br>Running time: {3}ms", job.DisplayName,job.Host, response.Message, response.MiliSecondsResponse);
            //var body = string.Format(Mail.GetEmailBodyTemplate(1), "");
            //Mail.SendMailNotification(job. + " Failed", body, "");
        }

    }
}
