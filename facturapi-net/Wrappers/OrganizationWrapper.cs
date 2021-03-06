using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class OrganizationWrapper : BaseWrapper
    {
        public OrganizationWrapper(string apiKey) : base (apiKey) { }

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

        public async Task<ApiKeys> GetApiKeysAsync(string id)
        {
            var response = await client.GetAsync(Router.GetApiKeys(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var organization = JsonConvert.DeserializeObject<ApiKeys>(resultString, this.jsonSettings);
            return organization;
        }
    }
}
