//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repositories
{
    using System;
    using System.Collections.Generic;
    
    public partial class monitoring_services_jobs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public monitoring_services_jobs()
        {
            this.monitoring_services_responses = new HashSet<monitoring_services_responses>();
        }
    
        public string id { get; set; }
        public string host { get; set; }
        public string username { get; set; }
        public string passwrd { get; set; }
        public string port { get; set; }
        public string display_name { get; set; }
        public string description { get; set; }
        public string cron { get; set; }
        public Nullable<bool> execute_job { get; set; }
        public string monitoring_services_id { get; set; }
        public Nullable<bool> notify_on_failure { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime updated_on { get; set; }
    
        public virtual monitoring_services monitoring_services { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<monitoring_services_responses> monitoring_services_responses { get; set; }
    }
}