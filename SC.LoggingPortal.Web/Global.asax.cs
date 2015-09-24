namespace SC.LoggingPortal.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.Web.SessionState;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.Windsor.Installer;
    using SC.LoggingPortal.CastleWindsor;
    using SC.LoggingPortal.Solr.Configuration;

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Windsor.Container = new WindsorContainer().Install(FromAssembly.Named("SC.LoggingPortal.CastleWindsor"));
            Windsor.Container.AddFacility(new SolrNetFacility());
        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (Windsor.Container != null)
                Windsor.Container.Dispose();
        }
    }
}