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

        public static Task<SearchResult<Customer>> ListAsync (Dictionary<string, object> query = null)
        {
            return new Wrapper().ListCustomers(query);
        }

        public static Task<Customer> CreateAsync (Dictionary<string, object> data)
        {
            return new Wrapper().CreateCustomer(data);
        }

        public static Task<Customer> RetrieveAsync (string id)
        {
            return new Wrapper().RetrieveCustomer(id);
        }

        public static Task<Customer> DeleteAsync (string id)
        {
            return new Wrapper().DeleteCustomer(id);
        }
    }
}
