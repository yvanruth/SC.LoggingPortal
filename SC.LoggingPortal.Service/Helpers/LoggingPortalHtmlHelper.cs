using SC.LoggingPortal.Solr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SC.LoggingPortal.Service.Helpers
{
    public class LoggingPortalHtmlHelper
    {
        public static String GetPopoverContent(SolrLogMessage model)
        {
            var sb = new StringBuilder(String.Format("<div>{0}</div><br />", model.LoggerMessage));

            string template = "<div><b>{0}: </b>{1}</div>";
            sb.Append(String.Format(template, FriendlyFieldName("log_level"), model.LogLevel));
            sb.Append(String.Format(template, FriendlyFieldName("application_name"), model.ApplicationName));
            sb.Append(String.Format(template, FriendlyFieldName("log_user_identity"), model.LogUserIdentity));
            sb.Append(String.Format(template, FriendlyFieldName("machine_name"), model.MachineName));
            sb.Append(String.Format(template, FriendlyFieldName("ip_address"), model.IPAddress));
            sb.Append(String.Format(template, FriendlyFieldName("is_64_bit_process"), model.Is64BitProcess));
            sb.Append(String.Format(template, FriendlyFieldName("logger_name"), model.LoggerName));
            return sb.ToString();
        }

        public static String FriendlyFieldName(string fieldName)
        {
            string friendly = fieldName;
            switch (fieldName)
            {
                case "application_name":
                    friendly = "Application name";
                    break;
                case "ip_address":
                    friendly = "IP address";
                    break;
                case "is_64_bit_process":
                    friendly = "64 bit process";
                    break;
                case "log_level":
                    friendly = "Log level";
                    break;
                case "log_user_identity":
                    friendly = "Application pool";
                    break;
                case "machine_name":
                    friendly = "Machine name";
                    break;
                case "net_version":
                    friendly = ".NET version";
                    break;
                case "timestamp":
                    friendly = "Log timestamp";
                    break;
                case "logger_name":
                    friendly = "Logger name";
                    break;
            }
            return friendly;
        }
    }
}