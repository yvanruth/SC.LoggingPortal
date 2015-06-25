namespace SC.LoggingPortal.LogAppender
{
    using log4net.Appender;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class Log4NetMongoAppender : AppenderSkeleton
    {
        private readonly SC.LoggingPortal.LogAppender.Service.SCLogger _logger = new SC.LoggingPortal.LogAppender.Service.SCLogger();               
        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            this._logger.LogMessage(string.Format("hahahaa {0} - {1}", loggingEvent.RenderedMessage.ToString(), DateTime.Now.ToLongDateString()));            
        }
    }
}