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
        public async Task InvoiceGetPaymentSummaryAsync_UsesPaymentSummaryRoute()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/invoices/inv_123/payment-summary?amount=58", request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("{\"uuid\":\"6CF6CE33-1BD2-4F88-A443-33013C069169\",\"installment\":1,\"last_balance\":100,\"total\":100,\"currency\":\"MXN\",\"amount\":58,\"taxes\":[{\"base\":50,\"rate\":0.16,\"type\":\"IVA\",\"factor\":\"Tasa\",\"withholding\":false}]}"));
            });

            var wrapper = new InvoiceWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.GetPaymentSummaryAsync("inv_123", 58);

            Assert.Equal("6CF6CE33-1BD2-4F88-A443-33013C069169", result.Uuid);
            Assert.Equal(1, result.Installment);
            Assert.Equal(50m, result.Taxes[0].Base);
            Assert.False(result.Taxes[0].Withholding);
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
        public async Task ReceiptToInvoiceAsync_UsesToInvoiceRoute()
        {
            var handler = new RecordingHandler(async (request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Post, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/receipts/to-invoice", request.RequestUri.PathAndQuery);
                Assert.NotNull(request.Content);
                var body = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                Assert.Contains("\"keys\":[\"rcp_1\",\"rcp_2\"]", body);
                return JsonResponse("{\"id\":\"inv_123\"}");
            });

            var wrapper = new ReceiptWrapper("test_key", "v2", CreateHttpClient(handler));
            await wrapper.ToInvoiceAsync(new Dictionary<string, object>
            {
                ["keys"] = new[] { "rcp_1", "rcp_2" }
            });
        }

        [Fact]
        public async Task ReceiptPreviewToInvoicePdfAsync_UsesPreviewPdfRoute()
        {
            var payload = Encoding.UTF8.GetBytes("pdf-bytes");
            var handler = new RecordingHandler(async (request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Post, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/receipts/to-invoice/preview", request.RequestUri.PathAndQuery);
                Assert.NotNull(request.Content);
                var body = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                Assert.Contains("\"keys\":[\"rcp_1\"]", body);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(payload)
                };
            });

            var wrapper = new ReceiptWrapper("test_key", "v2", CreateHttpClient(handler));
            using var stream = await wrapper.PreviewToInvoicePdfAsync(new Dictionary<string, object>
            {
                ["keys"] = new[] { "rcp_1" }
            });

            Assert.Equal(0, stream.Position);
            using var reader = new StreamReader(stream, Encoding.UTF8, false, 1024, leaveOpen: true);
            var text = await reader.ReadToEndAsync();
            Assert.Equal("pdf-bytes", text);
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
        public async Task OrganizationListTeamAccessAsync_UsesTeamRoute()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/organizations/org_1/team", request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("[]"));
            });

            var wrapper = new OrganizationWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.ListTeamAccessAsync("org_1");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task OrganizationInviteUserToTeamAsync_UsesInvitesRoute()
        {
            var handler = new RecordingHandler(async (request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Post, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/organizations/org_1/team/invites", request.RequestUri.PathAndQuery);
                Assert.NotNull(request.Content);
                var body = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                Assert.Contains("\"email\":\"dev@example.com\"", body);

                return JsonResponse("{\"id\":\"inv_001\",\"email\":\"dev@example.com\"}");
            });

            var wrapper = new OrganizationWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.InviteUserToTeamAsync("org_1", new Dictionary<string, object>
            {
                ["email"] = "dev@example.com"
            });

            Assert.Equal("inv_001", result.Id);
        }

        [Fact]
        public async Task OrganizationListTeamRoleOperationsAsync_UsesOperationsRoute()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/organizations/org_1/team/roles/operations", request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("[\"invoice:list\"]"));
            });

            var wrapper = new OrganizationWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.ListTeamRoleOperationsAsync("org_1");

            Assert.Single(result);
            Assert.Equal("invoice:list", result[0]);
        }

        [Fact]
        public async Task OrganizationRemoveTeamAccessAsync_ParsesOkResponse()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Delete, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/organizations/org_1/team/acc_1", request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("{\"ok\":true}"));
            });

            var wrapper = new OrganizationWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.RemoveTeamAccessAsync("org_1", "acc_1");

            Assert.True(result);
        }

        [Fact]
        public async Task OrganizationRespondTeamInviteAsync_UsesInviteResponseRoute()
        {
            var handler = new RecordingHandler(async (request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Post, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/organizations/invites/inv_1/response", request.RequestUri.PathAndQuery);
                Assert.NotNull(request.Content);
                var body = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                Assert.Contains("\"accept\":true", body);
                return JsonResponse("{\"ok\":true}");
            });

            var wrapper = new OrganizationWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.RespondTeamInviteAsync("inv_1", new Dictionary<string, object>
            {
                ["accept"] = true
            });

            Assert.True(result);
        }

        [Fact]
        public async Task OrganizationUpdateTeamRoleAsync_UsesRoleRoute()
        {
            var handler = new RecordingHandler(async (request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Put, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/organizations/org_1/team/roles/role_1", request.RequestUri.PathAndQuery);
                Assert.NotNull(request.Content);
                var body = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                Assert.Contains("\"name\":\"Senior billing analyst\"", body);
                return JsonResponse("{\"id\":\"role_1\",\"name\":\"Senior billing analyst\"}");
            });

            var wrapper = new OrganizationWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.UpdateTeamRoleAsync("org_1", "role_1", new Dictionary<string, object>
            {
                ["name"] = "Senior billing analyst"
            });

            Assert.Equal("role_1", result.Id);
            Assert.Equal("Senior billing analyst", result.Name);
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
        public async Task RetentionListAsync_CanFilterDrafts()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/retentions?status=draft", request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("{\"data\":[]}"));
            });

            var wrapper = new RetentionWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.ListAsync(new Dictionary<string, object> { ["status"] = "draft" });

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task InvoiceListAsync_SerializesNestedDateRangeWithBracketNotation()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal(
                    "/v2/invoices?limit=100&date%5Bgte%5D=2026-01-01&date%5Blt%5D=2026-02-01",
                    request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("{\"data\":[]}"));
            });

            var wrapper = new InvoiceWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.ListAsync(new Dictionary<string, object>
            {
                ["limit"] = 100,
                ["date"] = new Dictionary<string, object>
                {
                    ["gte"] = "2026-01-01",
                    ["lt"] = "2026-02-01"
                }
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task InvoiceListAsync_MapsPaginationMetadata()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/invoices?limit=100", request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("{\"data\":[],\"total_results\":3000,\"totals_are_capped\":true,\"next_cursor\":\"next-1\",\"previous_cursor\":null}"));
            });

            var wrapper = new InvoiceWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.ListAsync(new Dictionary<string, object>
            {
                ["limit"] = 100
            });

            Assert.NotNull(result);
            Assert.Equal(3000, result.TotalResults);
            Assert.True(result.TotalsAreCapped);
            Assert.Equal("next-1", result.NextCursor);
            Assert.Null(result.PreviousCursor);
        }

        [Fact]
        public async Task InvoiceListAsync_LaterCursorPageOmitsPageTotals()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/invoices?pagination=cursor&after=next-1", request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("{\"data\":[{\"id\":\"inv_x\"}],\"next_cursor\":\"next-2\",\"previous_cursor\":\"next-1\"}"));
            });

            var wrapper = new InvoiceWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.ListAsync(new Dictionary<string, object>
            {
                ["pagination"] = "cursor",
                ["after"] = "next-1"
            });

            Assert.NotNull(result);
            Assert.Null(result.Page);
            Assert.Null(result.TotalPages);
            Assert.Null(result.TotalResults);
            Assert.Equal("next-2", result.NextCursor);
            Assert.Equal("next-1", result.PreviousCursor);
            Assert.Single(result.Data);
        }

        [Fact]
        public async Task InvoiceListAsync_SerializesArrayParamsWithBracketKeys()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal(
                    "/v2/invoices?status%5B%5D=valid&status%5B%5D=canceled",
                    request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("{\"data\":[]}"));
            });

            var wrapper = new InvoiceWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.ListAsync(new Dictionary<string, object>
            {
                ["status"] = new List<string> { "valid", "canceled" }
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task RetentionCreateAsync_CanCreateDraft()
        {
            var handler = new RecordingHandler(async (request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Post, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/retentions", request.RequestUri.PathAndQuery);
                Assert.NotNull(request.Content);
                var body = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                Assert.Contains("\"status\":\"draft\"", body);
                Assert.Contains("\"customer\":null", body);

                return JsonResponse("{\"id\":\"ret_123\"}");
            });

            var wrapper = new RetentionWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.CreateAsync(new Dictionary<string, object>
            {
                ["status"] = "draft",
                ["customer"] = null!
            });

            Assert.Equal("ret_123", result.Id);
        }

        [Fact]
        public async Task RetentionUpdateDraftAsync_UsesRetentionRoute()
        {
            var handler = new RecordingHandler(async (request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Put, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/retentions/ret_123", request.RequestUri.PathAndQuery);
                Assert.NotNull(request.Content);
                var body = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                Assert.Contains("\"folio_int\":\"R-2026-001\"", body);

                return JsonResponse("{\"id\":\"ret_123\"}");
            });

            var wrapper = new RetentionWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.UpdateDraftAsync("ret_123", new Dictionary<string, object>
            {
                ["folio_int"] = "R-2026-001"
            });

            Assert.Equal("ret_123", result.Id);
        }

        [Fact]
        public async Task RetentionCopyToDraftAsync_UsesCopyRoute()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Post, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/retentions/ret_123/copy", request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("{\"id\":\"ret_copy\"}"));
            });

            var wrapper = new RetentionWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.CopyToDraftAsync("ret_123");

            Assert.Equal("ret_copy", result.Id);
        }

        [Fact]
        public async Task RetentionStampDraftAsync_UsesStampRoute()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Post, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/retentions/ret_123/stamp", request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("{\"id\":\"ret_123\"}"));
            });

            var wrapper = new RetentionWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.StampDraftAsync("ret_123");

            Assert.Equal("ret_123", result.Id);
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
        public async Task ErrorMapping_ExposesApiErrorFieldsAndHeaders()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                var response = new HttpResponseMessage((HttpStatusCode)429)
                {
                    Content = new StringContent(
                        "{\"message\":\"too many requests\",\"status\":429,\"code\":\"RATE_LIMIT_EXCEEDED\",\"path\":\"date\",\"location\":\"query\",\"errors\":[{\"code\":\"required\",\"message\":\"date is required\",\"path\":\"date\",\"location\":\"query\"}]}",
                        Encoding.UTF8,
                        "application/json")
                };
                response.Headers.Add("Retry-After", "3");
                response.Headers.Add("x-facturapi-log-id", "log_123");
                return Task.FromResult(response);
            });

            var wrapper = new CustomerWrapper("test_key", "v2", CreateHttpClient(handler));
            var exception = await Assert.ThrowsAsync<FacturapiException>(() => wrapper.ListAsync());

            Assert.Equal(429, exception.Status);
            Assert.Equal("too many requests", exception.Message);
            Assert.Equal("RATE_LIMIT_EXCEEDED", exception.Code);
            Assert.Equal("date", exception.Path);
            Assert.Equal("query", exception.Location);
            Assert.Equal("log_123", exception.LogId);
            Assert.Equal("required", exception.Errors[0]["code"]?.ToString());
            Assert.Equal("3", exception.Headers["retry-after"]);
            Assert.Equal("log_123", exception.Headers["x-facturapi-log-id"]);
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
        public async Task InvoiceCreateZipRequestAsync_UsesZipRequestsPostRoute()
        {
            var handler = new RecordingHandler(async (request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Post, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/invoices/zip-requests", request.RequestUri.PathAndQuery);
                Assert.NotNull(request.Content);
                var body = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                Assert.Contains("\"year\":2025", body);
                Assert.Contains("\"issuer_type\":\"issuing\"", body);
                Assert.Contains("\"invoice_types\":[\"I\",\"E\"]", body);
                return JsonResponse("{\"id\":\"zip_1\",\"status\":\"pending\"}");
            });

            var wrapper = new InvoiceWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.CreateZipRequestAsync(new Dictionary<string, object>
            {
                ["year"] = 2025,
                ["month"] = 3,
                ["issuer_type"] = "issuing",
                ["invoice_types"] = new[] { "I", "E" }
            });

            Assert.Equal("zip_1", result["id"]?.ToString());
        }

        [Fact]
        public async Task InvoiceListZipRequestsAsync_UsesZipRequestsQueryRoute()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/invoices/zip-requests", request.RequestUri.AbsolutePath);
                Assert.Equal(
                    new[] { "limit=20", "month=3", "page=1", "status=finished", "year=2025" },
                    request.RequestUri.Query.TrimStart('?').Split('&').OrderBy(parameter => parameter)
                );
                return Task.FromResult(JsonResponse("{\"page\":1,\"total_pages\":1,\"total_results\":1,\"data\":[{\"id\":\"zip_1\"}]}"));
            });

            var wrapper = new InvoiceWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.ListZipRequestsAsync(new Dictionary<string, object>
            {
                ["year"] = 2025,
                ["month"] = 3,
                ["status"] = "finished",
                ["limit"] = 20,
                ["page"] = 1
            });

            Assert.Single(result.Data);
            Assert.Equal("zip_1", result.Data[0]["id"]?.ToString());
        }

        [Fact]
        public async Task InvoiceRetrieveZipRequestAsync_UsesZipRequestRoute()
        {
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/invoices/zip-requests/zip_1", request.RequestUri.PathAndQuery);
                return Task.FromResult(JsonResponse("{\"id\":\"zip_1\",\"status\":\"finished\"}"));
            });

            var wrapper = new InvoiceWrapper("test_key", "v2", CreateHttpClient(handler));
            var result = await wrapper.RetrieveZipRequestAsync("zip_1");

            Assert.Equal("finished", result["status"]?.ToString());
        }

        [Fact]
        public async Task InvoiceDownloadZipRequestAsync_ReturnsSeekableStreamAtPositionZero()
        {
            var payload = Encoding.UTF8.GetBytes("zip-request-content");
            var handler = new RecordingHandler((request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.NotNull(request.RequestUri);
                Assert.Equal("/v2/invoices/zip-requests/zip_1/zip", request.RequestUri.PathAndQuery);
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(payload)
                });
            });

            var wrapper = new InvoiceWrapper("test_key", "v2", CreateHttpClient(handler));
            using var stream = await wrapper.DownloadZipRequestAsync("zip_1");

            Assert.Equal(0, stream.Position);
            using var reader = new StreamReader(stream, Encoding.UTF8, false, 1024, leaveOpen: true);
            var text = await reader.ReadToEndAsync();
            Assert.Equal("zip-request-content", text);
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
