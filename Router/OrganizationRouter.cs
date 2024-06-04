using System;
using System.Collections.Generic;

namespace Facturapi
{
    internal static partial class Router
    {
        public static string ListOrganizations(Dictionary<string, object> query = null)
        {
            return UriWithQuery("organizations", query);
        }

        public static string RetrieveOrganization(string id)
        {
            return $"organizations/{id}";
        }

        public static string CreateOrganization()
        {
            return "organizations";
        }

        public static string DeleteOrganization(string id)
        {
            return RetrieveOrganization(id);
        }

        public static string UpdateLegal(string id)
        {
            return $"{RetrieveOrganization(id)}/legal";
        }
        
        public static string UpdateCustomization(string id)
        {
            return $"{RetrieveOrganization(id)}/customization";
        }

        public static string UploadLogo(string id)
        {
            return $"{RetrieveOrganization(id)}/logo";
        }
        
        public static string UploadCertificate(string id)
        {
            return $"{RetrieveOrganization(id)}/certificate";
        }

        public static string DeleteCertificate(string id)
        {
            return $"{RetrieveOrganization(id)}/certificate";
        }

        public static string GetTestApiKey(string id)
        {
            return $"{RetrieveOrganization(id)}/apikeys/test";
        }

        public static string RenewTestApiKey(string id)
        {
            return $"{RetrieveOrganization(id)}/apikeys/test";
        }

        public static string RenewLiveApiKey(string id)
        {
            return $"{RetrieveOrganization(id)}/apikeys/live";
        }

        public static string ListSeriesGroup (string id)
        {
            return $"{RetrieveOrganization(id)}/series-group";
        }
        
        public static string CreateSeriesGroup (string id)
        {
            return $"{RetrieveOrganization(id)}/series-group";
        }

        public static string UpdateSeriesGroup (string id, string series_name)
        {
            return $"{RetrieveOrganization(id)}/series-group/{series_name}";
        }
        public static string DeleteSeriesGroup (string id, string series_name)
        {
            return $"{RetrieveOrganization(id)}/series-group/{series_name}";
        }
        
    }
}
