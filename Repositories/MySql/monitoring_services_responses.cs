//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repositories.MySql
{
    using System;
    using System.Collections.Generic;
    
    public partial class monitoring_services_responses
    {
        public string id { get; set; }
        public string host { get; set; }
        public Nullable<bool> is_successful { get; set; }
        public string passwrd { get; set; }
        public string port { get; set; }
        public string miliseconds_response { get; set; }
        public string message { get; set; }
        public string monitoring_services_jobs_id { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime updated_on { get; set; }
    
        public virtual monitoring_services_jobs monitoring_services_jobs { get; set; }
    }
}