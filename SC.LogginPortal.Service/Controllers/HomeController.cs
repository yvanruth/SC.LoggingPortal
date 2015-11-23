using SC.LoggingPortal.Solr;
using System;
using System.Collections.Generic;
using System.IO;
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
                Facets = result.Facets,
                Page = 1
            });
        }

        public ActionResult JSON(int page)
        {
            var result = new SolrProvider().GetResults(new SolrRequestOptions()
            {
                Page = page,
                PageSize = 100,
                Facets = null
            });

            return Json(new
            {
                HTML = RenderRazorViewToString("Index", new Models.OverviewModel()
                    {
                        Results = result.Results,
                        TotalCount = result.TotalResults,
                        Facets = result.Facets,
                        Page = page
                    })
            }, JsonRequestBehavior.AllowGet);
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}