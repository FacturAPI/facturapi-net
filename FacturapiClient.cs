using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Facturapi
{
    public class FacturapiClient : IDisposable
    {
        public Wrappers.CustomerWrapper Customer { get; private set; }
        public Wrappers.ProductWrapper Product { get; private set; }
        public Wrappers.InvoiceWrapper Invoice { get; private set; }
        public Wrappers.OrganizationWrapper Organization { get; private set; }
        public Wrappers.ReceiptWrapper Receipt { get; private set; }
        public Wrappers.RetentionWrapper Retention { get; private set; }
        public Wrappers.CatalogWrapper Catalog { get; private set; }
        public Wrappers.CatalogWrapper CartaporteCatalog { get; private set; }
        public Wrappers.ToolWrapper Tool { get; private set; }
        private readonly HttpClient httpClient;

        public FacturapiClient(string apiKey, string apiVersion = "v2")
        {
            var apiKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey + ":"));
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri($"https://www.facturapi.io/{apiVersion}/")
            };
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", apiKeyBase64);

            this.Customer = new Wrappers.CustomerWrapper(apiKey, apiVersion, this.httpClient);
            this.Product = new Wrappers.ProductWrapper(apiKey, apiVersion, this.httpClient);
            this.Invoice = new Wrappers.InvoiceWrapper(apiKey, apiVersion, this.httpClient);
            this.Organization = new Wrappers.OrganizationWrapper(apiKey, apiVersion, this.httpClient);
            this.Receipt = new Wrappers.ReceiptWrapper(apiKey, apiVersion, this.httpClient);
            this.Retention = new Wrappers.RetentionWrapper(apiKey, apiVersion, this.httpClient);
            this.Catalog = new Wrappers.CatalogWrapper(apiKey, apiVersion, this.httpClient);
            this.CartaporteCatalog = new Wrappers.CatalogWrapper(apiKey, apiVersion, this.httpClient);
            this.Tool = new Wrappers.ToolWrapper(apiKey, apiVersion, this.httpClient);
        }

        public void Dispose()
        {
            this.httpClient?.Dispose();
        }
    }
}
