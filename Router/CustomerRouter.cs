using System.Collections.Generic;

namespace Facturapi
{
    internal static partial class Router
    {
        public static string ListCustomers(Dictionary<string, object> query = null)
        {
            return UriWithQuery("customers", query);
        }

        public static string RetrieveCustomer(string id)
        {
            return $"customers/{id}";
        }

        public static string ValidateCustomerTaxInfo(string id)
        {
            return $"customers/{id}/tax-info-validation";
        }

        public static string CreateCustomer()
        {
            return "customers";
        }

        public static string DeleteCustomer(string id)
        {
            return RetrieveCustomer(id);
        }

        public static string UpdateCustomer(string id) {
            return RetrieveCustomer(id);
        }
    }
}
