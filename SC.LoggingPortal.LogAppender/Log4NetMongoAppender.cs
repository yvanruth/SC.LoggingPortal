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

        private bool LoggingDisabled
        {
            get
            {
                HttpContext.Current.Cache.Insert("Absolute Cache expiration 10 mins / 5 mins.")
                if (HttpContext.Current.Cache["sc.loggingportal.isloggingdisabled"] != null)
                {
                    return (bool)HttpContext.Current.Cache["sc.loggingportal.isloggingdisabled"];
                }

                return false;
            }

            set
            {
                HttpContext.Current.Cache["sc.loggingportal.isloggingdisabled"] = value;
            }
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (!LoggingDisabled)
            {
                try
                {
                    this._logger.LogMessage(new LogMessage { ApplicationName = System.Web.Hosting.HostingEnvironment.SiteName, IPAddress = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(), Is64BitProcess = Environment.Is64BitProcess, LoggerMessage = loggingEvent.RenderedMessage, LoggerName = loggingEvent.LoggerName, LogLevel = loggingEvent.Level.Name, LogUserIdentity = loggingEvent.UserName, MachineName = Environment.MachineName, NetVersion = Environment.Version.ToString(), TimeStamp = loggingEvent.TimeStamp });
                }
                catch (Exception ex)
                {
                    LoggingDisabled = true;
                }   
            }                    
        }
    }
}