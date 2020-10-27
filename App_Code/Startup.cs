using Hangfire;

using Microsoft.Owin;

using Owin;

using System;
using System.Configuration;


[assembly: Microsoft.Owin.OwinStartupAttribute(typeof(HangFire.Startup))]

namespace HangFire

{

    public partial class Startup

    {

        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
            .UseSqlServerStorage(ConfigurationManager.AppSettings["hangfireConnect"].ToString());

            //BackgroundJob.Enqueue(() => Console.WriteLine("Getting Started with HangFire!"));

            app.UseHangfireDashboard();

            app.UseHangfireServer();

        }

    }

}