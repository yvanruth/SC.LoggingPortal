using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SC.LogginPortal.Service.SignalR
{
    public class LoggingHub : Hub
    {
        public void PushLogMessage(string message)
        {
            Clients.All.pull(message);
        }
    }
}