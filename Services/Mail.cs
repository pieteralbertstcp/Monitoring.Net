using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Hangfire;
using Models.emaill;
using Models.ServicesModels;
using Services.MonitoringServices;

namespace Services
{
    /// <summary>
    /// Used to send out email notifications.
    /// </summary>
    public class Mail
    {
        #region  public methods
        /// <summary>
        /// Send out custom email notifications
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="emailAddress"></param>
        public static void SendMailNotification(string subject, string body, string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(body) ||
                string.IsNullOrWhiteSpace(emailAddress))
                throw new ArgumentNullException("Unable to send mail as the params are not valid/null");
            SendEmail(subject, body, emailAddress.Split(',').ToList());
        }


        /// <summary>
        /// Send out exception email notifications to the sys adim specified in the system config
        /// </summary>
        /// <param name="exception"></param>
        public static void SendMailNotification(Exception exception)
        {
            //TODO: Get the email from the generic cnfig
            var emailAddress = "";
            SendEmail("Exception occured - " + exception.GetType(), "Please note that the following exception occured: " + exception.GetBaseException().InnerException, emailAddress.Split(',').ToList());
        }

        /// <summary>
        /// Regex function to validate the provided email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmailAddressValid(string email)
        {
            const string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                return Regex.Replace(email, expresion, string.Empty).Length == 0;
            }
            return false;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// This private function enqeues a hangfire job to send out the email notifications
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="emailAddress"></param>
        private static void SendEmail(string subject, string body, List<string> emailAddress)
        {
            BackgroundJob.Enqueue(() => SendMailViaProvider(ObtainDefaultEmailProvider(), subject, body, emailAddress));
        }

        /// <summary>
        /// Used to obtain the configured email provider as well as its authentication details
        /// </summary>
        /// <returns></returns>
        private static EmailProvider ObtainDefaultEmailProvider()
        {
            return new EmailProvider();
        }

        /// <summary>
        /// This is the generic email job that actually sends out the email notifications via the passed provider.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="emailAddress"></param>
        private static void SendMailViaProvider(EmailProvider provider, string subject, string body, List<string> emailAddress)
        {
            try
            {
                if (IsProviderActive(provider.Host) == false) throw new Exception("Unable to access / ping the following email provider: " + provider.Host);
                if (emailAddress == null || emailAddress.Count == 0) throw new ArgumentException("Please check the email provided, as it seems to be invalid.");
                var msg = new MailMessage { From = new MailAddress(provider.EmailAddress) };
                msg.To.Add(string.Join(";", emailAddress.ToArray()));
                msg.Subject = subject;
                msg.Body = body;
                var client = new SmtpClient
                {
                    UseDefaultCredentials = true,
                    Host = provider.Host,
                    Port = provider.Port,
                    EnableSsl = provider.UseSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(provider.EmailAddress, provider.Password),
                    Timeout = 20000
                };
                client.Send(msg);
            }
            catch (Exception ex)
            {
                throw ex.InnerException != null ? ex.InnerException.GetBaseException() : ex;
            }
        }

        /// <summary>
        /// pings the Provided email host/smtpand returns bool
        /// </summary>
        /// <param name="emailHostName"></param>
        /// <returns></returns>
        private static bool IsProviderActive(string emailHostName)
        {
            var result = new PingIp().Execute(new MonitoringServiceJob { Host = emailHostName });
            return result.IsSuccessful;
        }

        #endregion
    }
}
