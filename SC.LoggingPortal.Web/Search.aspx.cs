using SC.LoggingPortal.CastleWindsor;
using SC.LoggingPortal.Solr;
using SolrNet;
using SolrNet.Commands.Parameters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SC.LoggingPortal.Web
{
    public partial class Search : System.Web.UI.Page
    {
        ISolrOperations<SolrLogMessage> solr;
        SolrQueryResults<SolrLogMessage> result = new SolrQueryResults<SolrLogMessage>();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            string[] facets = ConfigurationManager.AppSettings["solr_facets"].Split('|');
            solr = Windsor.Container.Resolve<ISolrOperations<SolrLogMessage>>();

            result = solr.Query(SolrQuery.All, new QueryOptions
            {
                // FilterQueries = BuildFilterQuery(),
                Facet = new FacetParameters
                {
                    Queries = BuildFacetQueryList(facets),
                    Limit = -1
                },
                Fields = new List<string>() { },
                //  OrderBy = new[] { new SortOrder(SolrHelper.GetSolrSortFieldName(this.solrSortField), this.solrSortOrder) },
                //   ExtraParams = extraParams,
                Start = 0,
                Rows = 100,
                //Grouping = new GroupingParameters
                //{
                //    Fields = new List<string> { "camping" },
                //    Limit = groupResultLimit,
                //    Ngroups = true
                //}
            });

            this.rptLogMessages.DataSource = result.ToList();
            this.rptLogMessages.DataBind();

            this.rptFacets.DataSource = result.FacetFields.Select(x => x.Key);
            this.rptFacets.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
    
        }

        private ICollection<ISolrFacetQuery> BuildFacetQueryList(string[] facets)
        {
            List<SolrNet.ISolrFacetQuery> facetQueryList = new List<SolrNet.ISolrFacetQuery>();

            facets.ToList().ForEach(facet => 
            {
                facetQueryList.Add(new SolrFacetFieldQuery(new LocalParams { { "ex", facet } } + facet));
            }
            );

            return facetQueryList;
        }

        protected void rptFacets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string facetKey = e.Item.DataItem as string;

                Literal litFacetName = e.Item.FindControl("litFacetName") as Literal;
                CheckBoxList cblFacetOptions = e.Item.FindControl("cblFacetOptions") as CheckBoxList;

                if (litFacetName != null)
                {
                    litFacetName.Text = facetKey;
                }

                if (cblFacetOptions != null)
                {
                    cblFacetOptions.DataSource = result.FacetFields.Where(x => x.Key.Equals(facetKey)).First().Value.Select(x => new ListItem { Text = string.Format("{0} ({1})", x.Key, x.Value), Value = string.Format("{0}|{1}", facetKey, x.Key) }).ToList();
                    cblFacetOptions.DataBind();
                }
            }
        }

        protected void rptLogMessages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SolrLogMessage message = e.Item.DataItem as SolrLogMessage;

                Literal litMessage = e.Item.FindControl("litMessage") as Literal;

                if (litMessage != null)
                {
                    litMessage.Text = message.LoggerMessage;
                }
            }
        }
    }
}