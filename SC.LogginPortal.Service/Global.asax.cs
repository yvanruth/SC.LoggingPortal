namespace SC.LogginPortal.Service
{
    using System;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.Threading;
    using Castle.Facilities.WcfIntegration;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using SC.LogginPortal.Service.Logging;
    using SC.LogginPortal.Service.Logging.Service;
    using Castle.Windsor.Installer;

    public class Global : System.Web.HttpApplication
    {
        IWindsorContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
            _container = new WindsorContainer();

            _container.AddFacility<WcfFacility>()
                .Register
                (
                     Component.For<ILogger>().ImplementedBy<Logger>(),
                     Component.For<ILoggingService>().ImplementedBy<LoggingService>(),
                     Component.For<ISCLogger>()
                              .ImplementedBy<SCLogger>()
                              .Named("SCLogger")
                );
            _container.Install(FromAssembly.InThisApplication());
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