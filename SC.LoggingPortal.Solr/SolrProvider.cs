using SC.LoggingPortal.CastleWindsor;
using SolrNet;
using SolrNet.Commands.Parameters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.LoggingPortal.Solr
{
    public class SolrProvider
    {
        ISolrOperations<SolrLogMessage> solr;
        SolrQueryResults<SolrLogMessage> result = new SolrQueryResults<SolrLogMessage>();

        public SolrSearchResults GetResults()
        {
            return GetResults(new SolrRequestOptions()
            {
                Page = 1,
                PageSize = 100,
                Facets = null
            });
        }

        public SolrSearchResults GetResults(SolrRequestOptions ops)
        {
            string[] facets = ConfigurationManager.AppSettings["SolrFacets"].Split('|');
            solr = Windsor.Container.Resolve<ISolrOperations<SolrLogMessage>>();

            var queryOptions = new QueryOptions
            {
                Facet = new FacetParameters
                {
                    Queries = BuildFacetQueryList(facets),
                    Limit = -1, //10,
                    Sort = true,
                    MinCount = 1
                },
                Fields = new List<string>() { },
                OrderBy = new[] { new SortOrder("timestamp") },
                Start = ops.Page > 1 ? (ops.Page - 1) * ops.PageSize : 0,
                Rows = ops.PageSize
            };
            if (ops.Facets != null)
            {
                queryOptions.FilterQueries = BuildFilterQuery(ops.Facets);
            }

            result = solr.Query(SolrQuery.All, queryOptions);

            return new SolrSearchResults()
            {
                TotalResults = result.NumFound,
                Results = result.ToList(),
                Facets = result.FacetFields.Select(f => new SolrFacet()
                {
                    FieldName = f.Key,
                    Filters = f.Value.Select(fi => new SolrFilter()
                    {
                        Value = fi.Key,
                        Count = fi.Value
                    }).ToList()
                }).ToList()
            };
        }

        private ICollection<ISolrQuery> BuildFilterQuery(Dictionary<String, String[]> facets)
        {
            List<SolrNet.ISolrQuery> query = new List<ISolrQuery>();
            foreach (var kvp in facets.Where(f => !string.IsNullOrWhiteSpace(f.Key) && f.Value.Any()))
            {
                var subquery = new List<ISolrQuery>();
                subquery.AddRange(kvp.Value.Select(f => new SolrQueryByField(kvp.Key, f)));
                query.Add(new SolrMultipleCriteriaQuery(subquery, SolrNet.SolrMultipleCriteriaQuery.Operator.OR));
            }
            return query;
        }

        private ICollection<ISolrFacetQuery> BuildFacetQueryList(string[] facets)
        {
            List<SolrNet.ISolrFacetQuery> facetQueryList = new List<SolrNet.ISolrFacetQuery>();
            facetQueryList.AddRange(facets.Select(f => new SolrFacetFieldQuery(new LocalParams { { "ex", f } } + f)));
            return facetQueryList;
        }
    }
}
