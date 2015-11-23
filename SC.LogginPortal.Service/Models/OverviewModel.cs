using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC.LogginPortal.Service.Models
{
    public class OverviewModel
    {
        public List<SC.LoggingPortal.Solr.SolrLogMessage> Results { get; set; }

        public int TotalCount { get; set; }

        public List<SC.LoggingPortal.Solr.SolrFacet> Facets { get; set; }
    }
}