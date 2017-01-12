using System;
using System.Text.RegularExpressions;
using Hangfire;

namespace Services
{
    public class Mail
    {

        #region  public methods

        public static void SendMailNotification(string subject, string body, string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(body) ||
                string.IsNullOrWhiteSpace(emailAddress))
                throw new ArgumentNullException("Unable to send mail as the params are not valid/null");
            BackgroundJob.Enqueue(() => SendEmail(subject, body, emailAddress));
        }

        public static void SendMailNotification(Exception exception)
        {
            var emailAddress = "";
            BackgroundJob.Enqueue(
                () =>
                    SendEmail("Exception occured - " + exception.GetType(),
                        "Please note that the following exception occured: " +
                        exception.GetBaseException().InnerException, emailAddress));
        }

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



        private static void SendEmail(string subject, string body, string emailAddress)
        {
            try
            {
                if (IsEmailAddressValid(emailAddress) == false) throw new ArgumentException("Please check the email provided, as it seems to be invalid.");
                var defaultEmailProvider = ObtainDefaultEmailProvider();
                if (defaultEmailProvider == null) throw new Exception("No Default email provider has been specified.");



            }
            catch (Exception ex)
            {
                //log 
                throw;
            }
        }



        private static dynamic ObtainDefaultEmailProvider()
        {
            return null;
        }
        #endregion

    }


}
