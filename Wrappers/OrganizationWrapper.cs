using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class OrganizationWrapper : BaseWrapper
    {
        public OrganizationWrapper(string apiKey, string apiVersion = "v2") : base(apiKey, apiVersion)
        {
        }

        public async Task<SearchResult<Organization>> ListAsync(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.ListOrganizations(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<Organization>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<Organization> CreateAsync(Dictionary<string, object> data)
        {
            var response = await client.PostAsync(Router.CreateOrganization(), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
            return organization;
        }

        public async Task<Organization> RetrieveAsync(string id)
        {
            var response = await client.GetAsync(Router.RetrieveOrganization(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
            return organization;
        }

        public async Task<Organization> DeleteAsync(string id)
        {
            var response = await client.DeleteAsync(Router.DeleteOrganization(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
            return organization;
        }

        public async Task<Organization> UploadLogoAsync(string id, Stream file)
        {
            var form = new MultipartFormDataContent();
            form.Add(new StreamContent(file), "file", "logo.jpg");
            var response = await client.PutAsync(Router.UploadLogo(id), form);
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
            return organization;
        }

        public async Task<Organization> UploadCertificateAsync(string id, Stream cerFile, Stream keyFile, string password)
        {
            var form = new MultipartFormDataContent();
            form.Add(new StreamContent(cerFile), "cer", "certificate.cer");
            form.Add(new StreamContent(keyFile), "key", "key.key");
            form.Add(new StringContent(password), "password");
            var response = await client.PutAsync(Router.UploadCertificate(id), form);
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
            return organization;
        }

        public async Task<Certificate> DeleteCertificateAsync(string id)
        {
            var response = await client.DeleteAsync(Router.DeleteCertificate(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var certificateInfo = JsonConvert.DeserializeObject<Certificate>(resultString, this.jsonSettings);
            return certificateInfo;
        }

        public async Task<Organization> UpdateLegalAsync(string id, Dictionary<string, object> data)
        {
            var response = await client.PutAsync(Router.UpdateLegal(id), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				var error = JsonConvert.DeserializeObject<JObject>(resultString);
				throw new FacturapiException(error["message"].ToString());
			}
			var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
			return organization;
        }

        public async Task<Organization> UpdateCustomizationAsync(string id, Dictionary<string, object> data)
        {
            var response = await client.PutAsync(Router.UpdateCustomization(id), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
            return organization;
        }

        public async Task<string> GetTestApiKeyAsync(string id)
        {
            var response = await client.GetAsync(Router.GetTestApiKey(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            return resultString;
        }


        public async Task<string> RenewTestApiKeyAsync(string id)
        {
            var response = await client.PutAsync(Router.RenewTestApiKey(id), new StringContent(""));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            return resultString;
        }

        public async Task<LiveApiKey> ListLiveApiKeysAsync(string id)
        {
            var response = await client.GetAsync(Router.ListAsyncLiveApiKeys(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var listResult = JsonConvert.DeserializeObject<LiveApiKey>(resultString, this.jsonSettings);

            return listResult;
        }

        public async Task<string> RenewLiveApiKeyAsync(string id)
        {
            var response = await client.PutAsync(Router.RenewLiveApiKey(id), new StringContent(""));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            return resultString;
        }


        public async Task<List<SeriesGroup>> ListSeriesGroupAsync(string id)
        {
            var response = await client.GetAsync(Router.ListSeriesGroup(id));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<List<SeriesGroup>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<SeriesGroup> CreateSeriesGroupAsync(string id, Dictionary<string, object> data)
        {
            var response = await client.PutAsync(Router.CreateSeriesGroup(id), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				var error = JsonConvert.DeserializeObject<JObject>(resultString);
				throw new FacturapiException(error["message"].ToString());
			}
			var series = JsonConvert.DeserializeObject<SeriesGroup>(resultString, this.jsonSettings);
			return series;
        }

        public async Task<SeriesGroup> UpdateSeriesGroupAsync(string id, string seriesName, Dictionary<string, object> data)
        {
            var response = await client.PutAsync(Router.UpdateSeriesGroup(id, seriesName), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var series = JsonConvert.DeserializeObject<SeriesGroup>(resultString, this.jsonSettings);
            return series;
        }
        
        public async Task<SeriesGroup> DeleteSeriesGroupAsync(string id, string seriesName)
        {
            var response = await client.DeleteAsync(Router.UpdateSeriesGroup(id, seriesName));

            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var series = JsonConvert.DeserializeObject<SeriesGroup>(resultString, this.jsonSettings);
            return series;
        }
        
        public async Task<List<LiveApiKey>> DeleteLiveApiKeyAsync(string id, string apiKeyId)
        {
            var response = await client.DeleteAsync(Router.DeleteLiveApiKey(id, apiKeyId));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var deserializeJson = JsonConvert.DeserializeObject<List<LiveApiKey>>(resultString, this.jsonSettings);
            return deserializeJson;
        }

        public async Task<Organization> UpdateSelfInvoiceSettingsAsync(string organizationId, Dictionary<string, object> data)
        {
            var response = await client.PutAsync(Router.UpdateSelfInvoiceSettings(organizationId), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
            return organization;
        }
    }
}
