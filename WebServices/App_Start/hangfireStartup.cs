using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Hangfire;
using Hangfire.MySql;
using Owin;

namespace WebServices.App_Start
{
    public class HangfireStartup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseMySqlStorage(ConfigurationManager.AppSettings["HangfireMySql"]);
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            new Services.Hangfire.Hangfire().LoadAllHangFireJobs();
        }
    }
}