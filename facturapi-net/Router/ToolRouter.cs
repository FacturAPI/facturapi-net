using System.Collections.Generic;

namespace Facturapi
{
    internal static partial class Router
    {
        public static string ValidateTaxId(Dictionary<string, object> query = null)
        {
            return UriWithQuery("tools/tax_id_validation", query);
        }
    }
}
