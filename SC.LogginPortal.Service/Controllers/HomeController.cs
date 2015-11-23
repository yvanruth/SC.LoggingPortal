using SC.LoggingPortal.Solr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SC.LogginPortal.Service.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var result = new SolrProvider().GetResults();

            return View(new Models.OverviewModel()
            {
                Results = result.Results,
                TotalCount = result.TotalResults,
                Facets = result.Facets
            });
        }
    }
}