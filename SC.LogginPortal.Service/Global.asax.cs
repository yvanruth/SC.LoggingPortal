namespace SC.LoggingPortal.Service
{
    using System;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.Threading;
    using Castle.Facilities.WcfIntegration;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.Windsor.Installer;
    using SC.LoggingPortal.CastleWindsor;
    using SC.LoggingPortal.Logic.Services;

    public class Global : System.Web.HttpApplication
    {
        IWindsorContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
            _container = new WindsorContainer().Install(FromAssembly.InThisApplication());
            _container.AddFacility<WcfFacility>()
                .Register
                (              
                    Component.For<ILoggingService>().ImplementedBy<LoggingService>(),
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