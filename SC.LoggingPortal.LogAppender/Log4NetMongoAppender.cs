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

        protected override void Append(LoggingEvent loggingEvent)
        {
            this._logger.LogMessage(new LogMessage { ApplicationName = System.Web.Hosting.HostingEnvironment.SiteName, IPAddress = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"], Is64BitProcess = Environment.Is64BitProcess, LoggerMessage = loggingEvent.RenderedMessage, LoggerName = loggingEvent.LoggerName, LogLevel = loggingEvent.Level.Name, LogUserIdentity = loggingEvent.Identity, MachineName = Environment.MachineName, NetVersion = Environment.Version.Build.ToString(), TimeStamp = loggingEvent.TimeStamp });            
        }
    }
}