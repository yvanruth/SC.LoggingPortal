using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.LoggingPortal.Data.Entity
{
    public class LogMessage : EntityBase
    {
        public string Message { get; set; }

        public DateTime Time { get; set; }
    }
}
