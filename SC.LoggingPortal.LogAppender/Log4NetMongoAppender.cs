namespace SC.LoggingPortal.LogAppender
{
    using log4net.Appender;
    using log4net.spi;
    using SC.LoggingPortal.LogAppender.Service;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;


    public class Log4NetMongoAppender : FileAppender
    {
        private readonly SCLogger _logger = new SCLogger();

        public Log4NetMongoAppender()
        {
            //var x = true;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            this._logger.LogMessage(new LogMessage { ApplicationName = System.Web.Hosting.HostingEnvironment.SiteName, IPAddress = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(), Is64BitProcess = Environment.Is64BitProcess, LoggerMessage = loggingEvent.RenderedMessage, LoggerName = loggingEvent.LoggerName, LogLevel = loggingEvent.Level.Name, LogUserIdentity = loggingEvent.UserName, MachineName = Environment.MachineName, NetVersion = Environment.Version.ToString(), TimeStamp = loggingEvent.TimeStamp });            
        }
    }
}