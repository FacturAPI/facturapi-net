using System;
using System.Collections.Generic;
using System.Linq;

namespace Facturapi
{
    internal static partial class Router
    {
        private static string UriWithQuery(string path, Dictionary<string, object> query = null)
        {
            if (query == null || query.Count == 0)
            {
                return path;
            }

            var queryString = DictionaryToQueryString(query);
            if (String.IsNullOrEmpty(queryString))
            {
                return path;
            }

            return $"{path}?{queryString}";
        }

        private static string DictionaryToQueryString(Dictionary<string, object> dict)
        {
            return String.Join(
                "&",
                dict
                    .Where(x => !String.IsNullOrEmpty(x.Key))
                    .Select(x => String.Format(
                        "{0}={1}",
                        Uri.EscapeDataString(x.Key),
                        Uri.EscapeDataString(x.Value?.ToString() ?? String.Empty))));
        }
    }
}
