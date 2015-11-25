namespace SC.LoggingPortal.Service
{
    using Castle.Facilities.WcfIntegration;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.Windsor.Installer;
    using System;
    using System.Web.Routing;
    using System.Web.Mvc;
    using SC.LoggingPortal.Service.App_Start;
    using CastleWindsor;
    using Solr.Configuration;

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Windsor.Container = new WindsorContainer().Install(FromAssembly.Named("SC.LoggingPortal.CastleWindsor"));
            Windsor.Container.AddFacility<WcfFacility>()
                .Register
                (
                    Component.For<ISCLogger>()
                            .ImplementedBy<SCLogger>()
                            .Named("SCLogger")
                );

            Windsor.Container.AddFacility(new SolrNetFacility());
        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (Windsor.Container != null)
                Windsor.Container.Dispose();
        }
    }
}