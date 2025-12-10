using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Facturapi.Wrappers
{
    public class OrganizationWrapper : BaseWrapper
    {
        internal OrganizationWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<Organization>> ListAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListOrganizations(query), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();

                var searchResult = JsonConvert.DeserializeObject<SearchResult<Organization>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<Organization> CreateAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateOrganization(), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<Organization> RetrieveAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.RetrieveOrganization(id), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<Organization> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.DeleteOrganization(id), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<Organization> UploadLogoAsync(string id, Stream file, CancellationToken cancellationToken = default)
        {
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StreamContent(file), "file", "logo.jpg");
                using (var response = await client.PutAsync(Router.UploadLogo(id), form, cancellationToken))
                {
                    await this.ThrowIfErrorAsync(response, cancellationToken);
                    var resultString = await response.Content.ReadAsStringAsync();
                    var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                    return organization;
                }
            }
        }

        public async Task<Organization> UploadCertificateAsync(string id, Stream cerFile, Stream keyFile, string password, CancellationToken cancellationToken = default)
        {
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StreamContent(cerFile), "cer", "certificate.cer");
                form.Add(new StreamContent(keyFile), "key", "key.key");
                form.Add(new StringContent(password), "password");
                using (var response = await client.PutAsync(Router.UploadCertificate(id), form, cancellationToken))
                {
                    await this.ThrowIfErrorAsync(response, cancellationToken);
                    var resultString = await response.Content.ReadAsStringAsync();
                    var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                    return organization;
                }
            }
        }

        public async Task<Certificate> DeleteCertificateAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.DeleteCertificate(id), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var certificateInfo = JsonConvert.DeserializeObject<Certificate>(resultString, this.jsonSettings);
                return certificateInfo;
            }
        }

        public async Task<Organization> UpdateLegalAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateLegal(id), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<Organization> UpdateCustomizationAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateCustomization(id), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<string> GetTestApiKeyAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.GetTestApiKey(id), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                return resultString;
            }
        }


        public async Task<string> RenewTestApiKeyAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(""))
            using (var response = await client.PutAsync(Router.RenewTestApiKey(id), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                return resultString;
            }
        }

        public async Task<LiveApiKey> ListLiveApiKeysAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListAsyncLiveApiKeys(id), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var listResult = JsonConvert.DeserializeObject<LiveApiKey>(resultString, this.jsonSettings);

                return listResult;
            }
        }

        public async Task<string> RenewLiveApiKeyAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(""))
            using (var response = await client.PutAsync(Router.RenewLiveApiKey(id), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                return resultString;
            }
        }


        public async Task<List<SeriesGroup>> ListSeriesGroupAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListSeriesGroup(id), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();

                var searchResult = JsonConvert.DeserializeObject<List<SeriesGroup>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<SeriesGroup> CreateSeriesGroupAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.CreateSeriesGroup(id), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var series = JsonConvert.DeserializeObject<SeriesGroup>(resultString, this.jsonSettings);
                return series;
            }
        }

        public async Task<SeriesGroup> UpdateSeriesGroupAsync(string id, string seriesName, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateSeriesGroup(id, seriesName), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var series = JsonConvert.DeserializeObject<SeriesGroup>(resultString, this.jsonSettings);
                return series;
            }
        }
        
        public async Task<SeriesGroup> DeleteSeriesGroupAsync(string id, string seriesName, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.UpdateSeriesGroup(id, seriesName), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var series = JsonConvert.DeserializeObject<SeriesGroup>(resultString, this.jsonSettings);
                return series;
            }
        }
        
        public async Task<List<LiveApiKey>> DeleteLiveApiKeyAsync(string id, string apiKeyId, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.DeleteLiveApiKey(id, apiKeyId), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var deserializeJson = JsonConvert.DeserializeObject<List<LiveApiKey>>(resultString, this.jsonSettings);
                return deserializeJson;
            }
        }

        public async Task<Organization> UpdateSelfInvoiceSettingsAsync(string organizationId, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateSelfInvoiceSettings(organizationId), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }
    }
}
