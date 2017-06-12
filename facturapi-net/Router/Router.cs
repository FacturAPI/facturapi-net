using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Facturapi
{
    internal static partial class Router
    {
        private static string UriWithQuery(string path, Dictionary<string, object> query = null)
        {
            var url = path;
            if (query != null)
            {
                return $"{path}{DictionaryToQueryString(query)}";
            }
            else
            {
                return path;
            }
        }

        private static string DictionaryToQueryString(Dictionary<string, object> dict)
        {
            return String.Join("&", dict.Select(x => String.Format("{0}={1}", Uri.EscapeDataString(x.Key), Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
}
