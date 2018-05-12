using Facturapi.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    public class Customer
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Livemode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public string LegalName { get; set; }
        public string TaxId { get; set; }
        
        [Obsolete("Methods requiring you to set ApiKeys globally are deprecated and will be removed in the next major release. Use methods from the Facturapi.Wrapper instance instead.")]
        public static Task<SearchResult<Customer>> ListAsync (Dictionary<string, object> query = null)
        {
            return new CustomerWrapper().ListAsync(query);
        }

        [Obsolete("Methods requiring you to set ApiKeys globally are deprecated and will be removed in the next major release. Use methods from the Facturapi.Wrapper instance instead.")]
        public static Task<Customer> CreateAsync (Dictionary<string, object> data)
        {
            return new CustomerWrapper().CreateAsync(data);
        }

        [Obsolete("Methods requiring you to set ApiKeys globally are deprecated and will be removed in the next major release. Use methods from the Facturapi.Wrapper instance instead.")]
        public static Task<Customer> RetrieveAsync (string id)
        {
            return new CustomerWrapper().RetrieveAsync(id);
        }

        [Obsolete("Methods requiring you to set ApiKeys globally are deprecated and will be removed in the next major release. Use methods from the Facturapi.Wrapper instance instead.")]
        public static Task<Customer> DeleteAsync (string id)
        {
            return new CustomerWrapper().DeleteAsync(id);
        }

        [Obsolete("Methods requiring you to set ApiKeys globally are deprecated and will be removed in the next major release. Use methods from the Facturapi.Wrapper instance instead.")]
        public static Task<Customer> UpdateAsync (string id, Dictionary<string, object> data)
        {
            return new CustomerWrapper().UpdateAsync(id, data);
        }
    }
}
