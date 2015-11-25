using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.LoggingPortal.Solr.Models
{
    public class SolrSearchResults
    {
        public int TotalResults { get; set; }
        public List<SolrFacet> Facets { get; set; }
        public List<SolrLogMessage> Results { get; set; }
    }
}
