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

    public class Global : System.Web.HttpApplication
    {
        IWindsorContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            _container = new WindsorContainer().Install(FromAssembly.Named("SC.LoggingPortal.CastleWindsor"));
            _container.AddFacility<WcfFacility>()
                .Register
                (                                  
                    Component.For<ISCLogger>()
                            .ImplementedBy<SCLogger>()
                            .Named("SCLogger")
                );

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (_container != null)                
                _container.Dispose();
        }
    }
}