using Facturapi.Wrappers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Facturapi
{
    public class FacturapiClient : IFacturapiClient
    {
        public CustomerWrapper Customer { get; private set; }
        public ProductWrapper Product { get; private set; }
        public InvoiceWrapper Invoice { get; private set; }
        public OrganizationWrapper Organization { get; private set; }
        public ReceiptWrapper Receipt { get; private set; }
        public RetentionWrapper Retention { get; private set; }
        public CatalogWrapper Catalog { get; private set; }
        public CatalogWrapper CartaporteCatalog { get; private set; }
        public ToolWrapper Tool { get; private set; }
        public WebhookWrapper Webhook { get; private set; }
        private readonly HttpClient httpClient;

        public FacturapiClient(string apiKey, string apiVersion = "v2")
        {
            var apiKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey + ":"));
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri($"https://www.facturapi.io/{apiVersion}/")
            };
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", apiKeyBase64);

            this.Customer = new CustomerWrapper(apiKey, apiVersion, this.httpClient);
            this.Product = new ProductWrapper(apiKey, apiVersion, this.httpClient);
            this.Invoice = new InvoiceWrapper(apiKey, apiVersion, this.httpClient);
            this.Organization = new OrganizationWrapper(apiKey, apiVersion, this.httpClient);
            this.Receipt = new ReceiptWrapper(apiKey, apiVersion, this.httpClient);
            this.Retention = new RetentionWrapper(apiKey, apiVersion, this.httpClient);
            this.Catalog = new CatalogWrapper(apiKey, apiVersion, this.httpClient);
            this.CartaporteCatalog = new CatalogWrapper(apiKey, apiVersion, this.httpClient);
            this.Tool = new ToolWrapper(apiKey, apiVersion, this.httpClient);
            this.Webhook = new WebhookWrapper(apiKey, apiVersion, this.httpClient);
        }

        public void Dispose()
        {
            this.httpClient?.Dispose();
        }
    }
}
