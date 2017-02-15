using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Interfaces.email;
using Interfaces.Services;
using Services.MonitoringServices;

namespace Services.ExternalEmailProviders
{
    /// <summary>
    /// Class used to send emails as well as test Gmail status
    /// </summary>
    public class gmail : IExternalEmailProviders
    {
        public string Host;
        private string Username;
        private string Password;

        public gmail()
        {
            //Get auth detials from database.
            Password = "";
            Username = "";
            Host = "";
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Password)) throw new NullReferenceException("No Gmail auth detials are available.");
        }

        public void SendEmail(List<string> emailAddressList, string subject, string body)
        {
            var emailAddresses = "";
            foreach (var em in emailAddressList)
            {
                emailAddresses = emailAddresses + em.Trim() + ";";
            }
            if(string.IsNullOrWhiteSpace(emailAddresses)) throw new ArgumentNullException("emailAddressList");
            var msg = new MailMessage {From = new MailAddress(Username)};
            msg.To.Add(emailAddresses);
            msg.Subject = subject;
            msg.Body = body;
            var client = new SmtpClient
            {
                UseDefaultCredentials = true,
                Host = Host,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(Username, Password),
                Timeout = 20000
            };
            try
            {
                client.Send(msg);
            }
            finally
            {
                msg.Dispose();
            }
        }

        public bool IsProviderActive()
        {
            var result = new PingIp().Execute(new MonitoringServiceJob { Host = Host });
            return result.IsSuccessful;
        }
    }
}
