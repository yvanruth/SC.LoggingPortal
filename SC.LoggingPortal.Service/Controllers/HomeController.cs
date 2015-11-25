using SC.LoggingPortal.Solr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SC.LoggingPortal.Service.Controllers
{
    public class HomeController : Controller
    {
        public const int PageSize = 100;

        public ActionResult Index()
        {
            var result = new SolrProvider().GetResults();

            return View(new Models.OverviewModel()
            {
                Results = result.Results,
                TotalCount = result.TotalResults,
                Facets = result.Facets,
                Page = 1,
                TotalPages = ((result.TotalResults + PageSize - 1) / PageSize)
            });
        }

        public ActionResult JSON(int page, string f = null)
        {
            var facets = HttpUtility.ParseQueryString(f);

            Dictionary<string, string[]> parsedFacets = null;
            if (facets.HasKeys())
            {
                parsedFacets = new Dictionary<string, string[]>();
                foreach (string key in facets.Keys)
                {
                    string value = facets[key];
                    if (!string.IsNullOrEmpty(facets[key]))
                    {
                        parsedFacets.Add(key, HttpUtility.UrlDecode(facets[key]).Split(','));
                    }
                }
            }

            var result = new SolrProvider().GetResults(new SolrRequestOptions()
            {
                Page = page,
                PageSize = PageSize,
                Facets = parsedFacets
            });

            string timestamp = parsedFacets.ContainsKey("timestamp") ? parsedFacets["timestamp"][0] : string.Empty;

            return Json(new
            {
                HTML = RenderRazorViewToString("Index", new Models.OverviewModel()
                    {
                        Results = result.Results,
                        TotalCount = result.TotalResults,
                        Facets = result.Facets,
                        Timestamp = timestamp,
                        Page = page,
                        TotalPages = ((result.TotalResults + PageSize - 1) / PageSize)
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