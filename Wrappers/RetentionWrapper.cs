﻿using Newtonsoft.Json;
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
    public class RetentionWrapper : BaseWrapper
    {
        public RetentionWrapper(string apiKey, string apiVersion = "v2") : base(apiKey, apiVersion)
        {
        }

        public async Task<SearchResult<Invoice>> ListAsync(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.ListRetentionss(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<Invoice>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<Invoice> CreateAsync(Dictionary<string, object> data)
        {
            var response = await client.PostAsync(Router.CreateRetention(), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task<Invoice> RetrieveAsync(string id)
        {
            var response = await client.GetAsync(Router.RetrieveRetention(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task<Invoice> CancelAsync(string id)
        {
            var response = await client.DeleteAsync(Router.CancelRetention(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task SendByEmailAsync(string id, Dictionary<string, object> data = null)
        {
            var response = await client.PostAsync(Router.SendRetentionByEmail(id), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
        }

        private async Task<Stream> DownloadAsync(string id, string format)
        {
            var response = await client.GetAsync(Router.DownloadRetention(id, format));
            if (!response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var stream = await response.Content.ReadAsStreamAsync();
            return stream;
        }

        public Task<Stream> DownloadZipAsync(string id)
        {
            return this.DownloadAsync(id, "zip");
        }

        public Task<Stream> DownloadPdfAsync(string id)
        {
            return this.DownloadAsync(id, "pdf");
        }

        public Task<Stream> DownloadXmlAsync(string id)
        {
            return this.DownloadAsync(id, "xml");
        }
    }
}
