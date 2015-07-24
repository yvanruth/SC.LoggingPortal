namespace SC.LogginPortal.Service
{
    using Owin;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}