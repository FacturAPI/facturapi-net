using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    public class FacturapiClient
    {
        public Wrappers.CustomerWrapper Customer { get; private set; }
        public Wrappers.ProductWrapper Product { get; private set; }
        public Wrappers.InvoiceWrapper Invoice { get; private set; }
        public Wrappers.OrganizationWrapper Organization { get; private set; }

        public FacturapiClient(string apiKey)
        {
            this.Customer = new Wrappers.CustomerWrapper(apiKey);
            this.Product = new Wrappers.ProductWrapper(apiKey);
            this.Invoice = new Wrappers.InvoiceWrapper(apiKey);
            this.Organization = new Wrappers.OrganizationWrapper(apiKey);
        }
    }

    [Obsolete("Deprecated. Use FacturapiClient instead.")]
    public class Wrapper : FacturapiClient
    {
        public Wrapper(string apiKey) : base(apiKey) { }
    }
}
