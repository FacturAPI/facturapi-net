using System;
using System.Collections;
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
            var parts = new List<string>();
            foreach (var entry in dict.Where(x => !String.IsNullOrEmpty(x.Key)))
            {
                AppendQueryPart(parts, entry.Key, entry.Value);
            }

            return String.Join("&", parts);
        }

        private static void AppendQueryPart(List<string> parts, string key, object value)
        {
            // A null value means "no filter": omit the key instead of sending it
            // empty, which the API can read as an explicit empty value. Pass an
            // empty string to send `key=` on purpose.
            if (value == null)
            {
                return;
            }

            if (value is IDictionary dictionary)
            {
                foreach (DictionaryEntry entry in dictionary)
                {
                    AppendQueryPart(parts, key + "[" + entry.Key + "]", entry.Value);
                }

                return;
            }

            if (!(value is string) && value is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    AppendQueryPart(parts, key, item);
                }

                return;
            }

            parts.Add(Uri.EscapeDataString(key) + "=" + Uri.EscapeDataString(value.ToString() ?? String.Empty));
        }
    }
}
