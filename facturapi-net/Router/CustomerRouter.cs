using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static string CreateCustomer()
        {
            return "customers";
        }

        public static string DeleteCustomer(string id)
        {
            return RetrieveCustomer(id);
        }
    }
}
