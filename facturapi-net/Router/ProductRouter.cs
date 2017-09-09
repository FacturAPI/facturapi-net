using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    internal static partial class Router
    {
        public static string ListProducts(Dictionary<string, object> query = null)
        {
            return UriWithQuery("products", query);
        }

        public static string RetrieveProduct(string id)
        {
            return $"products/{id}";
        }

        public static string CreateProduct()
        {
            return "products";
        }

        public static string DeleteProduct(string id)
        {
            return RetrieveProduct(id);
        }

        public static string UpdateProduct(string id)
        {
            return RetrieveProduct(id);
        }
    }
}
