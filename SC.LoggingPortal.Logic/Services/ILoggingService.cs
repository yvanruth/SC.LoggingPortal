﻿namespace SC.LoggingPortal.Logic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public interface ILoggingService
    {
        void LogMessage(SC.LoggingPortal.Data.Entity.LogMessage message);
    }
}