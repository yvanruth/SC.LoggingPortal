using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SC.LoggingPortal.Service.Controllers
{
    public class SolrController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RebuildSolrIndex()
        {
            var s = new System.Diagnostics.Stopwatch();
            s.Start();
            await SolrManager.IndexAll();
            s.Stop();
            Debug.WriteLine("Solr Index reindex in: " + s.ElapsedMilliseconds + " ms");

            dynamic model = new ExpandoObject();
            model.Status = String.Format("Index rebuilded in: {0} ms.", s.ElapsedMilliseconds);

            return View("Index", model);
        }
    }
}