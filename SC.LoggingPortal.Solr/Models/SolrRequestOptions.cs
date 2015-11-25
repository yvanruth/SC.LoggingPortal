using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.LoggingPortal.Solr.Models
{
    public class SolrRequestOptions
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public Dictionary<String, String[]> Facets { get; set; }
    }
}
