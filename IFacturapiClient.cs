using Facturapi.Wrappers;
using System;

namespace Facturapi
{
    public interface IFacturapiClient : IDisposable
    {
        ICustomerWrapper Customer { get; }
        IProductWrapper Product { get; }
        IInvoiceWrapper Invoice { get; }
        IOrganizationWrapper Organization { get; }
        IReceiptWrapper Receipt { get; }
        IRetentionWrapper Retention { get; }
        ICatalogWrapper Catalog { get; }
        ICartaporteCatalogWrapper CartaporteCatalog { get; }
        IToolWrapper Tool { get; }
        IWebhookWrapper Webhook { get; }
    }
}
