using System.Collections.Generic;

namespace Facturapi
{
    internal static partial class Router
    {
        public static string ListCustomers(Dictionary<string, object> query = null)
        {
            return UriWithQuery("customers", query);
        }

        public static string RetrieveCustomer(string id, Dictionary<string, object> query = null)
        {
            return $"{UriWithQuery($"customers/{id}", query)}";
        }

        public static string ValidateCustomerTaxInfo(string id)
        {
            return $"customers/{id}/tax-info-validation";
        }

        public static string CreateCustomer(Dictionary<string, object> query = null)
        {
            return UriWithQuery("customers", query);
        }

        public static string DeleteCustomer(string id)
        {
            return RetrieveCustomer(id);
        }

        public static string UpdateCustomer(string id, Dictionary<string, object> query = null) {
            return RetrieveCustomer(id, query);
        }

        public static string SendEditLinkByEmail(string id)
        {
            return $"{UriWithQuery($"customers/{id}/email-edit-link")}";
        }
    }
}
