using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.LoggingPortal.Solr.Models
{
    public class SolrFilter
    {
        public string Value { get; set; }
        public int Count { get; set; }
        public bool Checked { get; set; }
    }
}
