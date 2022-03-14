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
        public Wrappers.ReceiptWrapper Receipt { get; private set; }
        public Wrappers.RetentionWrapper Retention { get; private set; }
        public Wrappers.CatalogWrapper Catalog { get; private set; }
        public Wrappers.ToolWrapper Tool { get; private set; }

        public FacturapiClient(string apiKey, string apiVersion = "v2")
        {
            this.Customer = new Wrappers.CustomerWrapper(apiKey, apiVersion);
            this.Product = new Wrappers.ProductWrapper(apiKey, apiVersion);
            this.Invoice = new Wrappers.InvoiceWrapper(apiKey, apiVersion);
            this.Organization = new Wrappers.OrganizationWrapper(apiKey, apiVersion);
            this.Receipt = new Wrappers.ReceiptWrapper(apiKey, apiVersion);
            this.Retention = new Wrappers.RetentionWrapper(apiKey, apiVersion);
            this.Catalog = new Wrappers.CatalogWrapper(apiKey, apiVersion);
            this.Tool = new Wrappers.ToolWrapper(apiKey, apiVersion);
        }
    }
}
