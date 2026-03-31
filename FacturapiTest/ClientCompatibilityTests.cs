using Facturapi;
using Facturapi.Wrappers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FacturapiTest
{
    public class ClientCompatibilityTests
    {
        [Fact]
        public void Router_ListCustomers_AllowsNullQueryValues()
        {
            var query = new Dictionary<string, object>
            {
                ["foo"] = null!,
                [""] = "ignored"
            };

            var url = Router.ListCustomers(query);

            Assert.Equal("customers?foo=", url);
        }

        [Fact]
        public async Task RetentionCancelAsync_ThrowsFacturapiExceptionWithStatus()
        {
            var handler = new StubHttpMessageHandler((request, cancellationToken) =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("{\"message\":\"bad retention\",\"status\":400}", Encoding.UTF8, "application/json")
                };
                return Task.FromResult(response);
            });

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://www.facturapi.io/v2/")
            };
            var wrapper = new RetentionWrapper("test_key", "v2", httpClient);

            var exception = await Assert.ThrowsAsync<FacturapiException>(() =>
                wrapper.CancelAsync("ret_123", cancellationToken: CancellationToken.None));

            Assert.Equal(400, exception.Status);
            Assert.Equal("bad retention", exception.Message);
        }

        [Fact]
        public async Task InvoiceStampDraftAsync_AndLegacyMethod_BothWork()
        {
            var handler = new StubHttpMessageHandler((request, cancellationToken) =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"id\":\"inv_123\"}", Encoding.UTF8, "application/json")
                };
                return Task.FromResult(response);
            });

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://www.facturapi.io/v2/")
            };
            var wrapper = new InvoiceWrapper("test_key", "v2", httpClient);

            var fromAsync = await wrapper.StampDraftAsync("inv_123");
#pragma warning disable CS0618
            var fromLegacy = await wrapper.StampDraft("inv_123");
#pragma warning restore CS0618

            Assert.Equal("inv_123", fromAsync.Id);
            Assert.Equal("inv_123", fromLegacy.Id);
        }

        [Fact]
        public async Task CreateWithCustomHttpClient_DoesNotDisposeInjectedClient()
        {
            var handler = new StubHttpMessageHandler((request, cancellationToken) =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"ok\":true}", Encoding.UTF8, "application/json")
                };
                return Task.FromResult(response);
            });

            var injectedHttpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://www.facturapi.io/v2/")
            };

            var client = FacturapiClient.CreateWithCustomHttpClient("test_key", injectedHttpClient, "v2");

            var healthy = await client.Tool.HealthCheckAsync();
            Assert.True(healthy);

            client.Dispose();

            var response = await injectedHttpClient.GetAsync("check");
            Assert.True(response.IsSuccessStatusCode);
        }

        private sealed class StubHttpMessageHandler : HttpMessageHandler
        {
            private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handler;

            public StubHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handler)
            {
                this.handler = handler;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return this.handler(request, cancellationToken);
            }
        }
    }
}
