namespace SC.LoggingPortal.Logic.Services
{
    using log4net.Appender;
    using SC.LoggingPortal.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class LoggingService : ILoggingService
    {
        private readonly IRepository<LoggingPortal.Data.Entity.LogMessage> _repository;

        public LoggingService(IRepository<LoggingPortal.Data.Entity.LogMessage> repository)
        {
            this._repository = repository;
        }

        public void LogMessage(Data.Entity.LogMessage message)
        {
            this._repository.InsertAsync(new Data.Entity.LogMessage 
            { 
                Id = new Guid(), 
                ApplicationName = message.ApplicationName, 
                IPAddress = message.IPAddress, 
                Is64BitProcess = message.Is64BitProcess, 
                MachineName = message.MachineName, 
                LoggerMessage = message.LoggerMessage, 
                LoggerName = message.LoggerName, 
                LogLevel = message.LogLevel, 
                LogUserIdentity = message.LogUserIdentity, 
                NetVersion = message.NetVersion, 
                TimeStamp = message.TimeStamp 
            });
        }
    }
}