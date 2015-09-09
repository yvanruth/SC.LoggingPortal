using SC.LoggingPortal.Data.Entity;
using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.LoggingPortal.Solr
{
    public class SolrLogMessage
    {
        [SolrUniqueKey("id")]
        public string Id { get; set; }

        [SolrField("message_id")]
        public Guid MessageId { get; set; }

        [SolrField("machine_name")]
        public string MachineName { get; set; }

        [SolrField("application_name")]
        public string ApplicationName { get; set; }

        [SolrField("ip_address")]
        public string IPAddress { get; set; }

        [SolrField("net_version")]
        public string NetVersion { get; set; }

        [SolrField("is_64_bit_process")]
        public bool Is64BitProcess { get; set; }

        [SolrField("logger_name")]
        public string LoggerName { get; set; }

        [SolrField("log_user_identity")]
        public string LogUserIdentity { get; set; }

        [SolrField("log_level")]
        public string LogLevel { get; set; }

        [SolrField("timestamp")]
        public DateTime TimeStamp { get; set; }

        [SolrField("logger_message")]
        public string LoggerMessage { get; set; }
    }
}
