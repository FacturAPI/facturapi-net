using System;
using System.Collections.Generic;

namespace Facturapi
{
    internal static partial class Router
    {
        public static string ListOrganizations(Dictionary<string, object> query = null)
        {
            return UriWithQuery("organization", query);
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

        public static string GetApiKeys(string id)
        {
            return $"{RetrieveOrganization(id)}/apikeys";
        }
    }
}
