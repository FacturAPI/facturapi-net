using Facturapi;
using Facturapi.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FacturapiTest
{
    public class WrapperBehaviorTests
    {
        [Fact]
        public async Task InvoiceCreateAsync_UsesPostAndQueryString()
        {
            var handler = new RecordingHandler(async (request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Post, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/invoices?foo=bar", request.RequestUri.PathAndQuery);
                Assert.NotNull(request.Content);
                var body = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                Assert.Contains("\"field\":\"value\"", body);

                return JsonResponse("{\"id\":\"inv_001\"}");
            });

            var wrapper = new InvoiceWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.CreateAsync(
                new Dictionary<string, object> { ["field"] = "value" },
                new Dictionary<string, object> { ["foo"] = "bar" });

            Assert.Equal("inv_001", result.Id);
        }

        [Fact]
        public async Task ReceiptCancelAsync_UsesReceiptDeleteRoute()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Delete, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/receipts/rcp_123", request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("{\"id\":\"rcp_123\"}"));
            });

            var wrapper = new ReceiptWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.CancelAsync("rcp_123");

            Assert.Equal("rcp_123", result.Id);
        }

        [Fact]
        public async Task OrganizationDeleteSeriesAsync_UsesDeleteSeriesRoute()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Delete, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/organizations/org_1/series-group/A", request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("{\"name\":\"A\"}"));
            });

            var wrapper = new OrganizationWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.DeleteSeriesAsync("org_1", "A");

            Assert.Equal("A", result.Name);
        }

        [Fact]
        public async Task RetentionListAsync_UsesRetentionsRoute()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/retentions?page=2", request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("{\"data\":[]}"));
            });

            var wrapper = new RetentionWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.ListAsync(new Dictionary<string, object> { ["page"] = 2 });

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task ErrorMapping_UsesStatusFromString()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("{\"message\":\"bad request\",\"status\":\"400\"}", Encoding.UTF8, "application/json")
                });
            });

            var wrapper = new CustomerWrapper("test_key", "v2", CreateHttpClient(handler));
            var exception = await Assert.ThrowsAsync<FacturapiException>(() => wrapper.ListAsync());

            Assert.Equal(400, exception.Status);
            Assert.Equal("bad request", exception.Message);
        }

        [Fact]
        public async Task ErrorMapping_UsesStatusFromFloat()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("{\"message\":\"unauthorized\",\"status\":401.0}", Encoding.UTF8, "application/json")
                });
            });

            var wrapper = new CustomerWrapper("test_key", "v2", CreateHttpClient(handler));
            var exception = await Assert.ThrowsAsync<FacturapiException>(() => wrapper.ListAsync());

            Assert.Equal(401, exception.Status);
            Assert.Equal("unauthorized", exception.Message);
        }

        [Fact]
        public async Task ErrorMapping_NonJsonFallbacksToHttpStatus()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("server exploded", Encoding.UTF8, "text/plain")
                });
            });

            var wrapper = new CustomerWrapper("test_key", "v2", CreateHttpClient(handler));
            var exception = await Assert.ThrowsAsync<FacturapiException>(() => wrapper.ListAsync());

            Assert.Equal(500, exception.Status);
            Assert.Equal("An error occurred", exception.Message);
        }

        [Fact]
        public async Task CustomerListAsync_RespectsCancellationToken()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                return Task.FromResult(JsonResponse("{\"data\":[]}"));
            });

            var wrapper = new CustomerWrapper("test_key", "v2", CreateHttpClient(handler));
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => wrapper.ListAsync(cancellationToken: cts.Token));
        }

        [Fact]
        public async Task InvoiceDownloadPdfAsync_ReturnsSeekableStreamAtPositionZero()
        {
            var payload = Encoding.UTF8.GetBytes("pdf-bytes");
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/invoices/inv_1/pdf", request.RequestUri.PathAndQuery);
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(payload)
                });
            });

            var wrapper = new InvoiceWrapper("test_key", "v2", CreateHttpClient(handler));
            using var stream = await wrapper.DownloadPdfAsync("inv_1");

            Assert.Equal(0, stream.Position);
            using var reader = new StreamReader(stream, Encoding.UTF8, false, 1024, leaveOpen: true);
            var text = await reader.ReadToEndAsync();
            Assert.Equal("pdf-bytes", text);
        }

        [Fact]
        public async Task RetentionDownloadZipAsync_ReturnsSeekableStreamAtPositionZero()
        {
            var payload = Encoding.UTF8.GetBytes("zip-content");
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/retentions/ret_1/zip", request.RequestUri.PathAndQuery);
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(payload)
                });
            });

            var wrapper = new RetentionWrapper("test_key", "v2", CreateHttpClient(handler));
            using var stream = await wrapper.DownloadZipAsync("ret_1");

            Assert.Equal(0, stream.Position);
            using var reader = new StreamReader(stream, Encoding.UTF8, false, 1024, leaveOpen: true);
            var text = await reader.ReadToEndAsync();
            Assert.Equal("zip-content", text);
        }

        private static HttpClient CreateHttpClient(HttpMessageHandler handler)
        {
            return new HttpClient(handler)
            {
                BaseAddress = new Uri("https://www.facturapi.io/v2/")
            };
        }

        private static HttpResponseMessage JsonResponse(string json)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }

        private sealed class RecordingHandler : HttpMessageHandler
        {
            private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> responder;

            public RecordingHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> responder)
            {
                this.responder = responder;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return this.responder(request, cancellationToken);
            }
        }
    }
}
