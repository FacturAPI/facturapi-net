using Facturapi.Wrappers;
using System;

namespace Facturapi
{
    public interface IFacturapiClient : IDisposable
    {
        CustomerWrapper Customer { get; }
        ProductWrapper Product { get; }
        InvoiceWrapper Invoice { get; }
        OrganizationWrapper Organization { get; }
        ReceiptWrapper Receipt { get; }
        RetentionWrapper Retention { get; }
        CatalogWrapper Catalog { get; }
        CatalogWrapper CartaporteCatalog { get; }
        ToolWrapper Tool { get; }
    }
}
