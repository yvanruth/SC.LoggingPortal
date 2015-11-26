using SC.LoggingPortal.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.LoggingPortal.Solr.Models
{
    public interface ISolrManager
    {
        /// <summary>
        /// Retrieve log results
        /// </summary>
        /// <returns></returns>
        SolrSearchResults GetResults();

        /// <summary>
        /// Retrieve log results based on the given options
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        SolrSearchResults GetResults(SolrRequestOptions options);

        /// <summary>
        /// Reindex Solr Index completely
        /// </summary>
        Task IndexAll();

        /// <summary>
        /// Adds a single SolrLogMessage instance to the solr index
        /// </summary>
        /// <param name="message"></param>
        void IndexSingle(LogMessage message);
    }
}
