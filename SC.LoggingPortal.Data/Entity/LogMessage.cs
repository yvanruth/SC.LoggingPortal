using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.LoggingPortal.Data.Entity
{
    public class LogMessage : EntityBase
    {
        public string MachineName { get; set; }

        public string ApplicationName { get; set; }

        public string IPAddress { get; set; }

        public string NetVersion { get; set; }

        public bool Is64BitProcess { get; set; }

        public string LoggerName { get; set; }

        public string LogUserIdentity { get; set; }

        public string LogLevel { get; set; }

        public DateTime TimeStamp { get; set; }

        public string LoggerMessage { get; set; }
    }
}
