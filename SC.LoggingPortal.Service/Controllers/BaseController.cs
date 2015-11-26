using SC.LoggingPortal.Solr;
using SC.LoggingPortal.Solr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SC.LoggingPortal.Service.Controllers
{
    public class BaseController : Controller
    {
        private ISolrManager _manager;
        public ISolrManager SolrManager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = new SolrManager();
                }
                return _manager;
            }
        }
    }
}