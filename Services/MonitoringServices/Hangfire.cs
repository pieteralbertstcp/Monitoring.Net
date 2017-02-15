using System;
using System.Diagnostics;
using Hangfire;
using Interfaces.Services;
using Models.ServicesModels;

namespace Services
{
    public class Hangfire
    {
        public void LoadAllHangFireJobs()
        {
           /* try
            {
                for (int i = 0; i < UPPER; i++)
                {
                    RecurringJob.AddOrUpdate(i + " ClassName" +  " - IP", () => new Hangfire().ExcecuteJob("TEST", "localhost", "Network.IsAddressPingable"), "#1#5 * * * *", TimeZoneInfo.Local);        
                }
            }
            catch (Exception ex)
            {
                Mail.SendMailNotification(ex);
            }*/
        }
        
        /// <summary>
        /// This is the hangfire function that executes all jobs via reflection.
        /// </summary>
        /// <param name="job"></param>
        /// <param name="className"></param>
        public void ExcecuteJob(MonitoringServiceJob job, string className)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(className)) throw new Exception("The class name is empty!");
                var clss = className.Trim();
               
                var Class = Type.GetType("Services.MonitoringServices." + clss);
                if (Class == null) throw new ArgumentException("Unable to locate the required class");

                var constructor = Class.GetConstructor(Type.EmptyTypes);
                if (constructor == null) throw new Exception("Reflection - constructor is null.");

                var magicClassObject = constructor.Invoke(new object[] { });
                var magicMethod = Class.GetMethod("Execute");
                if (magicMethod == null) throw new ArgumentException("Unable to locate the required method");

                var result = (MonitoringServiceResponse) magicMethod.Invoke(magicClassObject, new object[] { job });
               SaveJobResults(result);

                if (result.IsSuccessful == false && job.NotifyOnFailure)
                    SendFailureNotification(job, result);
            }
            catch (Exception ex)
            {
                Mail.SendMailNotification(ex);
            }
        }

        /// <summary>
        /// Saves the job results to DB.
        /// </summary>
        /// <param name="result"></param>
        private static void SaveJobResults(MonitoringServiceResponse result)
        {
            
        }

        private static void SendFailureNotification(MonitoringServiceJob job, MonitoringServiceResponse response)
        {
            var body = string.Format("Please note that {0} failed to execute.</br>IP address: {1}</br>Please see additional info below</br></br>{2}</br></br>Running time: {3}ms", job.DisplayName,job.Host, response.Message, response.MiliSecondsResponse);
            Mail.SendMailNotification(job.DisplayName + " Failed", body, job.NotificationEmail);
        }
    }
}
