﻿
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: OwinStartup(typeof(SC.LoggingPortal.Service.Startup))]
namespace SC.LoggingPortal.Service
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var hubConfiguration = new HubConfiguration();

            hubConfiguration.EnableDetailedErrors = true;
            hubConfiguration.EnableJavaScriptProxies = true;

            app.MapSignalR();
        }
    }
}