using SC.LoggingPortal.CastleWindsor;
using SolrNet;
using SolrNet.Commands.Parameters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SC.LoggingPortal.Solr.Models;
using SC.LoggingPortal.Data.Entity;

namespace SC.LoggingPortal.Solr
{
    public class SolrManager : ISolrManager
    {
        ISolrOperations<SolrLogMessage> solr;
        IndexService indexService;

        public SolrManager()
        {
            solr = Windsor.Container.Resolve<ISolrOperations<SolrLogMessage>>();
            indexService = new IndexService();
        }

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
            Dictionary<string, string[]> selectedFacets = ops.Facets ?? new Dictionary<string, string[]>();

            var queryOptions = new QueryOptions
            {
                Facet = new FacetParameters
                {
                    Queries = BuildFacetQueryList(),
                    Limit = -1, //10,
                    //Sort = true,
                    MinCount = 0
                },
                Fields = new List<string>() { },
                OrderBy = new[] { new SortOrder("timestamp", Order.DESC) },
                Start = ops.Page > 1 ? (ops.Page - 1) * ops.PageSize : 0,
                Rows = ops.PageSize
            };
            if (selectedFacets.Any())
            {
                queryOptions.FilterQueries = BuildFilterQuery(selectedFacets);
            }

            var result = solr.Query(SolrQuery.All, queryOptions);

            // Facet ordering stuff
            var resultFacets = result.FacetFields.Select(f => new SolrFacet()
                {
                    FieldName = f.Key,
                    Filters = f.Value.Select(fi => new SolrFilter()
                    {
                        Value = fi.Key,
                        Count = fi.Value,
                        Checked = (selectedFacets.ContainsKey(f.Key) && selectedFacets[f.Key].Any(v => v.Equals(fi.Key, StringComparison.OrdinalIgnoreCase)))
                    }).ToList()
                }).ToList();

            resultFacets = resultFacets.OrderBy(f => f.FieldName).ToList();
            var loggerNameFacet = resultFacets.FirstOrDefault(c => c.FieldName.Equals("logger_name", StringComparison.OrdinalIgnoreCase));
            resultFacets.Remove(loggerNameFacet);
            resultFacets.Add(loggerNameFacet);

            return new SolrSearchResults()
            {
                TotalResults = result.NumFound,
                Results = result.ToList(),
                Facets = resultFacets
            };
        }

        private ICollection<ISolrQuery> BuildFilterQuery(Dictionary<String, String[]> facets)
        {
            List<SolrNet.ISolrQuery> query = new List<ISolrQuery>();
            foreach (var kvp in facets.Where(f => !string.IsNullOrWhiteSpace(f.Key) && f.Value.Any()))
            {
                if (kvp.Value.Count() == 1 && kvp.Value.FirstOrDefault().Contains('|'))
                {
                    // Date Query
                    string[] datesArray = kvp.Value.FirstOrDefault().Split('|');
                    DateTime min = DateTime.MinValue, max = DateTime.MinValue;

                    if (DateTime.TryParse(datesArray[0], out min) &&
                        DateTime.TryParse(datesArray[1], out max) &&
                        min != DateTime.MinValue &&
                        max != DateTime.MaxValue)
                    {
                        query.Add(new SolrQueryByRange<DateTime>(kvp.Key, min, max));
                    }
                }
                else
                {
                    var subquery = new List<ISolrQuery>();
                    subquery.AddRange(kvp.Value.Select(f => new SolrQueryByField(kvp.Key, f)));
                    query.Add(new LocalParams { { "tag", kvp.Key } } + new SolrMultipleCriteriaQuery(subquery, SolrNet.SolrMultipleCriteriaQuery.Operator.OR));
                }
            }
            return query;
        }

        private ICollection<ISolrFacetQuery> BuildFacetQueryList()
        {
            string[] facets_fields = ConfigurationManager.AppSettings["SolrFacets"].Split('|');

            List<SolrNet.ISolrFacetQuery> facetQueryList = new List<SolrNet.ISolrFacetQuery>();
            facetQueryList.AddRange(facets_fields.Select(f => new SolrFacetFieldQuery(new LocalParams { { "ex", f } } + f)));
            return facetQueryList;
        }

        #region Index
        public async Task IndexAll()
        {
            await indexService.FullIndex();
        }

        public void IndexSingle(LogMessage message)
        {
            indexService.AddSingle(message);
        }
        #endregion
    }
}
