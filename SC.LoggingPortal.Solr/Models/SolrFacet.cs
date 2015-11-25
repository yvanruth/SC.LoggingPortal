using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.LoggingPortal.Solr.Models
{

    public class SolrFacet
    {
        public String FieldName { get; set; }
        public List<SolrFilter> Filters { get; set; }
    }
}
