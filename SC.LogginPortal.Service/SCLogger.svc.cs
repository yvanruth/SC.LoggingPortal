﻿using SC.LoggingPortal.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SC.LoggingPortal.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SCLogger" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SCLogger.svc or SCLogger.svc.cs at the Solution Explorer and start debugging.
    public class SCLogger : ISCLogger
    {
        private readonly ILoggingService _loggingService;

        public SCLogger(ILoggingService loggingService)
        {
            this._loggingService = loggingService;
        }

        public void LogMessage(string message)
        {
            this._loggingService.LogMessage("testtttt");
        }
    }
}
