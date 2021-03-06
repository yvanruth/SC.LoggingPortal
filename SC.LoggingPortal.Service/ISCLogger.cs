﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SC.LoggingPortal.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISCLogger" in both code and config file together.
    [ServiceContract]
    public interface ISCLogger
    {
        [OperationContract]
        void LogMessage(SC.LoggingPortal.Data.Entity.LogMessage message);
    }
}
