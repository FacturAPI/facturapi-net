using System.Collections.Generic;

namespace Facturapi
{
    internal static partial class Router
    {
        // Carta Porte 3.1 catalogs
        public static string SearchCartaporteAirTransportCodes(Dictionary<string, object> query = null)
        {
            return UriWithQuery("catalogs/cartaporte/3.1/air-transport-codes", query);
        }

        public static string SearchCartaporteTransportConfigs(Dictionary<string, object> query = null)
        {
            return UriWithQuery("catalogs/cartaporte/3.1/transport-configs", query);
        }

        public static string SearchCartaporteRightsOfPassage(Dictionary<string, object> query = null)
        {
            return UriWithQuery("catalogs/cartaporte/3.1/rights-of-passage", query);
        }

        public static string SearchCartaporteCustomsDocuments(Dictionary<string, object> query = null)
        {
            return UriWithQuery("catalogs/cartaporte/3.1/customs-documents", query);
        }

        public static string SearchCartaportePackagingTypes(Dictionary<string, object> query = null)
        {
            return UriWithQuery("catalogs/cartaporte/3.1/packaging-types", query);
        }

        public static string SearchCartaporteTrailerTypes(Dictionary<string, object> query = null)
        {
            return UriWithQuery("catalogs/cartaporte/3.1/trailer-types", query);
        }

        public static string SearchCartaporteHazardousMaterials(Dictionary<string, object> query = null)
        {
            return UriWithQuery("catalogs/cartaporte/3.1/hazardous-materials", query);
        }

        public static string SearchCartaporteNavalAuthorizations(Dictionary<string, object> query = null)
        {
            return UriWithQuery("catalogs/cartaporte/3.1/naval-authorizations", query);
        }

        public static string SearchCartaportePortStations(Dictionary<string, object> query = null)
        {
            return UriWithQuery("catalogs/cartaporte/3.1/port-stations", query);
        }

        public static string SearchCartaporteMarineContainers(Dictionary<string, object> query = null)
        {
            return UriWithQuery("catalogs/cartaporte/3.1/marine-containers", query);
        }
    }
}
