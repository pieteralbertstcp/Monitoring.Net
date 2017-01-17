using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.Dashboard;
using System.Configuration;
using Hangfire.MySql;


[assembly: OwinStartup(typeof(WebServices.App_Start.Startup))]

namespace WebServices.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 5 });
            //TODO: Swop out the connection string for the 1 in the generic config
            JobStorage.Current = new MySqlStorage("server=localhost;uid=root;pwd=creative;database=hangfire;Allow User Variables=True");
            app.UseHangfireDashboard("/hangfire");
            new Services.Hangfire().LoadAllHangFireJobs();
            app.UseHangfireServer();
        }
    }
}
