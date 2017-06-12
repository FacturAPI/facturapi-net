using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Facturapi
{
    public class Product
    {
        public string Id { get; set; }
        public bool Livemode { get; set; }
        public string ProductKey { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool TaxIncluded { get; set; }
        public List<Tax> Taxes { get; set; }
        public string UnitKey { get; set; }
        public string UnitName { get; set; }
        public string Sku { get; set; }

        public static Task<SearchResult<Product>> ListAsync(Dictionary<string, object> query = null)
        {
            return new Wrapper().ListProducts(query);
        }

        public static Task<Product> CreateAsync(Dictionary<string, object> data)
        {
            return new Wrapper().CreateProduct(data);
        }

        public static Task<Product> RetrieveAsync(string id)
        {
            return new Wrapper().RetrieveProduct(id);
        }

        public static Task<Product> DeleteAsync(string id)
        {
            return new Wrapper().DeleteProduct(id);
        }
    }
}
