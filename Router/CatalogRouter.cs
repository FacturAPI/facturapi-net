using System.Collections.Generic;

namespace Facturapi
{
    internal static partial class Router
    {
        public static string SearchProductKeys(Dictionary<string, object> query = null)
        {
            return UriWithQuery("catalogs/products", query);
        }

        public static string SearchUnitKeys(Dictionary<string, object> query = null)
        {
            return UriWithQuery("catalogs/units", query);
        }
    }
}
