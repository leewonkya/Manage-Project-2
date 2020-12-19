using Hangfire;
using Microsoft.Owin;
using Owin;
using Project2.Core;
using Project2.Core.loc;
using Project2.Core.Data.Context;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Project2.Core.Migrations;
using GlobalConfiguration = Hangfire.GlobalConfiguration;

[assembly: OwinStartup(typeof(Project2.Web.App_Start.Startup))]
namespace Project2.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Start unity
            UnityHelper.InitialiseUnityContainer();

            // Make DB update to latest migration
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Core.Migrations.Configuration>());

            // Set the rest of the Ioc
            UnityHelper.BuildUnityContainer();

            // Grab the container as we will need to use it
            var unityContainer = UnityHelper.Container;

            // Set Hangfire to use SQL Server and the connection string
            //GlobalConfiguration.Configuration.UseSqlServerStorage(@"Server=.\SQLExpress;database=OriginV;Trusted_Connection=True;");
            GlobalConfiguration.Configuration.UseSqlServerStorage(Project2Configuration.Instance.OriginContext);

            // Make hangfire use unity container
            GlobalConfiguration.Configuration.UseUnityActivator(unityContainer);

            // Add Hangfire
            // TODO - Do I need this dashboard?
            //app.UseHangfireDashboard();
            app.UseHangfireServer();

            // Get services needed
            var DataContext = unityContainer.Resolve<DataContext>();

            // Routes
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}