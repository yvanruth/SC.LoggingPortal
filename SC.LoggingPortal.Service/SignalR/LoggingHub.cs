using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SC.LoggingPortal.Service.SignalR
{
    [HubName("loggingHub")]
    public class LoggingHub : Hub
    {
        public static void PushLogMessage(SC.LoggingPortal.Data.Entity.LogMessage message)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<LoggingHub>();
            hub.Clients.All.LogMessage(message);
        }
    }
}