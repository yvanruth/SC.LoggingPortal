using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.LoggingPortal.Solr
{
    public class SolrSearchResults
    {
        public int TotalResults { get; set; }
        public List<SolrFacet> Facets { get; set; }
        public List<SolrLogMessage> Results { get; set; }
    }

    public class SolrFacet
    {
        public String FieldName { get; set; }
        public SolrFacetType FacetType { get; set; }
        public List<SolrFilter> Filters { get; set; }
    }

    public class SolrFilter
    {
        public object Value { get; set; }
        public int Count { get; set; }
        public bool Checked { get; set; }
    }

    public enum SolrFacetType
    {
        FIELD,
        DATE,
        PIVOT,
        QUERY
    }

    public class SolrRequestOptions
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public Dictionary<String, String[]> Facets { get; set; }
    }
}
