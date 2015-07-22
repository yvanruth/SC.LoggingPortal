namespace SC.LoggingPortal.LogAppender
{
    using log4net.Appender;
    using log4net.spi;
    using SC.LoggingPortal.LogAppender.Service;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class Log4NetMongoAppender : FileAppender
    {
        private readonly SCLogger _logger = new SCLogger();

        public Log4NetMongoAppender()
        {
            var x = true;
        }
        
        
        protected override void Append(LoggingEvent loggingEvent)
        {
            this._logger.LogMessage(string.Format("{0} - {1}", loggingEvent.RenderedMessage.ToString(), DateTime.Now.ToLongDateString()));            
        }

        
    }
}