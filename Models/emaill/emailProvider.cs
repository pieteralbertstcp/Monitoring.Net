using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.emaill
{
    public struct EmailProvider
    {
        public string Id { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool UseSsl { get; set; }
    }
}
