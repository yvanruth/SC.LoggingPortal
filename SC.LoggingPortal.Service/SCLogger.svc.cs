﻿using SC.LoggingPortal.Logic.Services;
using SC.LoggingPortal.Service.SignalR;
using SC.LoggingPortal.Solr;
using SC.LoggingPortal.Solr.Models;
using System;

namespace SC.LoggingPortal.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SCLogger" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SCLogger.svc or SCLogger.svc.cs at the Solution Explorer and start debugging.
    public class SCLogger : ISCLogger
    {
        private readonly ILoggingService _loggingService;
        private ISolrManager _solrManager;

        public SCLogger(ILoggingService loggingService)
        {
            this._loggingService = loggingService;
            this._solrManager = new SolrManager();
        }

        public void LogMessage(SC.LoggingPortal.Data.Entity.LogMessage message)
        {
            message.TimeStamp = DateTime.Now;
            // Mongo
            try
            {
                this._loggingService.LogMessage(message);
            }
            catch { }

            // Solr
            try
            {
                this._solrManager.IndexSingle(message);
            }
            catch { }

            // SignalR (realtime)
            try
            {
                LoggingHub.PushLogMessage(message);
            }
            catch { }
        }
    }
}