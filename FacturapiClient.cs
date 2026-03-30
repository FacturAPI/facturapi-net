using Facturapi.Wrappers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Facturapi
{
    public sealed class FacturapiClient : IFacturapiClient
    {
        public ICustomerWrapper Customer { get; private set; }
        public IProductWrapper Product { get; private set; }
        public IInvoiceWrapper Invoice { get; private set; }
        public IOrganizationWrapper Organization { get; private set; }
        public IReceiptWrapper Receipt { get; private set; }
        public IRetentionWrapper Retention { get; private set; }
        public ICatalogWrapper Catalog { get; private set; }
        public ICartaporteCatalogWrapper CartaporteCatalog { get; private set; }
        public IToolWrapper Tool { get; private set; }
        public IWebhookWrapper Webhook { get; private set; }
        private readonly HttpClient httpClient;
        private readonly bool ownsHttpClient;
        private bool disposed;

        public FacturapiClient(string apiKey, string apiVersion = "v2")
            : this(apiKey, apiVersion, CreateDefaultHttpClient(apiKey, apiVersion), ownsHttpClient: true)
        {
        }

        private FacturapiClient(string apiKey, string apiVersion, HttpClient httpClient, bool ownsHttpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.ownsHttpClient = ownsHttpClient;
            ConfigureHttpClient(this.httpClient, apiKey, apiVersion);

            this.Customer = new CustomerWrapper(apiKey, apiVersion, this.httpClient);
            this.Product = new ProductWrapper(apiKey, apiVersion, this.httpClient);
            this.Invoice = new InvoiceWrapper(apiKey, apiVersion, this.httpClient);
            this.Organization = new OrganizationWrapper(apiKey, apiVersion, this.httpClient);
            this.Receipt = new ReceiptWrapper(apiKey, apiVersion, this.httpClient);
            this.Retention = new RetentionWrapper(apiKey, apiVersion, this.httpClient);
            this.Catalog = new CatalogWrapper(apiKey, apiVersion, this.httpClient);
            this.CartaporteCatalog = new CartaporteCatalogWrapper(apiKey, apiVersion, this.httpClient);
            this.Tool = new ToolWrapper(apiKey, apiVersion, this.httpClient);
            this.Webhook = new WebhookWrapper(apiKey, apiVersion, this.httpClient);
        }

        public static FacturapiClient CreateWithCustomHttpClient(string apiKey, HttpClient httpClient, string apiVersion = "v2")
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            return new FacturapiClient(apiKey, apiVersion, httpClient, ownsHttpClient: false);
        }

        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            if (this.ownsHttpClient)
            {
                this.httpClient.Dispose();
            }
            this.disposed = true;
            GC.SuppressFinalize(this);
        }

        private static HttpClient CreateDefaultHttpClient(string apiKey, string apiVersion)
        {
            var client = new HttpClient();
            ConfigureHttpClient(client, apiKey, apiVersion);
            return client;
        }

        private static void ConfigureHttpClient(HttpClient client, string apiKey, string apiVersion)
        {
            var apiKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey + ":"));
            client.BaseAddress = new Uri($"https://www.facturapi.io/{apiVersion}/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", apiKeyBase64);
        }
    }
}
