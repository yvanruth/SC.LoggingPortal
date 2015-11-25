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
        public void PushLogMessage(string message)
        {
            Clients.All.pull(message);
        }
    }
}