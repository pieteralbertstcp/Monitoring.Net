using System.Collections.Generic;

namespace Interfaces.email
{
    public interface IExternalEmailProviders
    {
        void SendEmail(List<string> emailAddressList, string subject, string body);
        bool IsProviderActive();
    }
}