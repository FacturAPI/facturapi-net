using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    internal static partial class Router
    {
        public static string ListRetentionss(Dictionary<string, object> query = null)
        {
            return UriWithQuery("retentions", query);
        }

        public static string RetrieveRetention(string id)
        {
            return $"retentions/{id}";
        }

        public static string CreateRetention()
        {
            return "retentions";
        }

        public static string CancelRetention(string id, Dictionary<string, object> query = null)
        {
            return UriWithQuery(RetrieveRetention(id), query);
        }

        public static string DownloadRetention(string id, string format)
        {
            return $"retentions/{id}/{format}";
        }

        public static string SendRetentionByEmail(string id)
        {
            return $"retentions/{id}/email";
        }
    }
}
