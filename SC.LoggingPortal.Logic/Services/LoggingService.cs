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

        public void LogMessage(string message)
        {
            this._repository.Insert(new Data.Entity.LogMessage { Id = new Guid(), Message = message, Time = DateTime.Now });
        }
    }
}