using System.Collections.Generic;

namespace Facturapi
{
    internal static partial class Router
    {
        public static string SearchNominaDeductions(Dictionary<string, object> query = null)
        {
            return UriWithQuery("catalogs/deductions", query);
        }

        public static string SearchNominaPerceptions(Dictionary<string, object> query = null)
        {
            return UriWithQuery("catalogs/perceptions", query);
        }
    }
}
