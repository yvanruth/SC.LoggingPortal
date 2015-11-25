using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC.LoggingPortal.Service.Models
{
    public class OverviewModel
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }

        public List<SC.LoggingPortal.Solr.SolrLogMessage> Results { get; set; }

        public int TotalCount { get; set; }

        public List<SC.LoggingPortal.Solr.SolrFacet> Facets { get; set; }
        public string Timestamp { get; set; }
    }
}