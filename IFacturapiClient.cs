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
        INominaCatalogWrapper NominaCatalog { get; }
        /// <summary>Busca claves y descripciones de fracciones arancelarias de comercio exterior.</summary>
        IComercioExteriorCatalogWrapper ComercioExteriorCatalog { get; }
        IToolWrapper Tool { get; }
        IWebhookWrapper Webhook { get; }
    }
}
