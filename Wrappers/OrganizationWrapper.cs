using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class OrganizationWrapper : BaseWrapper
    {
        internal OrganizationWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<Organization>> ListAsync(Dictionary<string, object> query = null)
        {
            using (var response = await client.GetAsync(Router.ListOrganizations(query)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();

                var searchResult = JsonConvert.DeserializeObject<SearchResult<Organization>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<Organization> CreateAsync(Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateOrganization(), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<Organization> RetrieveAsync(string id)
        {
            using (var response = await client.GetAsync(Router.RetrieveOrganization(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<Organization> DeleteAsync(string id)
        {
            using (var response = await client.DeleteAsync(Router.DeleteOrganization(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<Organization> UploadLogoAsync(string id, Stream file)
        {
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StreamContent(file), "file", "logo.jpg");
                using (var response = await client.PutAsync(Router.UploadLogo(id), form))
                {
                    await this.ThrowIfErrorAsync(response);
                    var resultString = await response.Content.ReadAsStringAsync();
                    var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                    return organization;
                }
            }
        }

        public async Task<Organization> UploadCertificateAsync(string id, Stream cerFile, Stream keyFile, string password)
        {
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StreamContent(cerFile), "cer", "certificate.cer");
                form.Add(new StreamContent(keyFile), "key", "key.key");
                form.Add(new StringContent(password), "password");
                using (var response = await client.PutAsync(Router.UploadCertificate(id), form))
                {
                    await this.ThrowIfErrorAsync(response);
                    var resultString = await response.Content.ReadAsStringAsync();
                    var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                    return organization;
                }
            }
        }

        public async Task<Certificate> DeleteCertificateAsync(string id)
        {
            using (var response = await client.DeleteAsync(Router.DeleteCertificate(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var certificateInfo = JsonConvert.DeserializeObject<Certificate>(resultString, this.jsonSettings);
                return certificateInfo;
            }
        }

        public async Task<Organization> UpdateLegalAsync(string id, Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateLegal(id), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<Organization> UpdateCustomizationAsync(string id, Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateCustomization(id), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<string> GetTestApiKeyAsync(string id)
        {
            using (var response = await client.GetAsync(Router.GetTestApiKey(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                return resultString;
            }
        }


        public async Task<string> RenewTestApiKeyAsync(string id)
        {
            using (var content = new StringContent(""))
            using (var response = await client.PutAsync(Router.RenewTestApiKey(id), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                return resultString;
            }
        }

        public async Task<LiveApiKey> ListLiveApiKeysAsync(string id)
        {
            using (var response = await client.GetAsync(Router.ListAsyncLiveApiKeys(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var listResult = JsonConvert.DeserializeObject<LiveApiKey>(resultString, this.jsonSettings);

                return listResult;
            }
        }

        public async Task<string> RenewLiveApiKeyAsync(string id)
        {
            using (var content = new StringContent(""))
            using (var response = await client.PutAsync(Router.RenewLiveApiKey(id), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                return resultString;
            }
        }


        public async Task<List<SeriesGroup>> ListSeriesGroupAsync(string id)
        {
            using (var response = await client.GetAsync(Router.ListSeriesGroup(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();

                var searchResult = JsonConvert.DeserializeObject<List<SeriesGroup>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<SeriesGroup> CreateSeriesGroupAsync(string id, Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.CreateSeriesGroup(id), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var series = JsonConvert.DeserializeObject<SeriesGroup>(resultString, this.jsonSettings);
                return series;
            }
        }

        public async Task<SeriesGroup> UpdateSeriesGroupAsync(string id, string seriesName, Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateSeriesGroup(id, seriesName), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var series = JsonConvert.DeserializeObject<SeriesGroup>(resultString, this.jsonSettings);
                return series;
            }
        }
        
        public async Task<SeriesGroup> DeleteSeriesGroupAsync(string id, string seriesName)
        {
            using (var response = await client.DeleteAsync(Router.UpdateSeriesGroup(id, seriesName)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var series = JsonConvert.DeserializeObject<SeriesGroup>(resultString, this.jsonSettings);
                return series;
            }
        }
        
        public async Task<List<LiveApiKey>> DeleteLiveApiKeyAsync(string id, string apiKeyId)
        {
            using (var response = await client.DeleteAsync(Router.DeleteLiveApiKey(id, apiKeyId)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var deserializeJson = JsonConvert.DeserializeObject<List<LiveApiKey>>(resultString, this.jsonSettings);
                return deserializeJson;
            }
        }

        public async Task<Organization> UpdateSelfInvoiceSettingsAsync(string organizationId, Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateSelfInvoiceSettings(organizationId), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }
    }
}
