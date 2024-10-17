using System;
using System.Collections.Generic;

namespace Facturapi
{
    internal static partial class Router
    {
        public static string ListWebhooks(Dictionary<string, object> query = null)
        {
            return UriWithQuery("webhooks", query);
        }

        public static string RetrieveWebhook(string id)
        {
            return $"webhooks/{id}";
        }

        public static string CreateWebhook()
        {
            return "webhooks";
        }

        public static string UpdateWebhook(string id)
        {
            return RetrieveWebhook(id);
        }

        public static string DeleteWebhook(string id)
        {
            return RetrieveWebhook(id);
        }

        public static string ValidateSignature()
        {
            return "webhooks/validate-signature";
        }

   
    }
}
