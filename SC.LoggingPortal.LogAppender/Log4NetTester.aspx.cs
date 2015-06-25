using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SC.LoggingPortal.LogAppender
{
    public partial class Log4NetTester : System.Web.UI.Page
    {
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);  

        protected void Page_Load(object sender, EventArgs e)
        {
            log.Warn("tessssssst");
        }
    }
}