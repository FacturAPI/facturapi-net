using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Facturapi.Wrappers
{
    public class OrganizationWrapper : BaseWrapper, IOrganizationWrapper
    {
        internal OrganizationWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<Organization>> ListAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListOrganizations(query), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var searchResult = JsonConvert.DeserializeObject<SearchResult<Organization>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<Organization> MeAsync(CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.OrganizationMe(), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<DomainAvailability> CheckDomainIsAvailableAsync(string domain, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.CheckDomainAvailability(new Dictionary<string, object> { ["domain"] = domain }), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var availability = JsonConvert.DeserializeObject<DomainAvailability>(resultString, this.jsonSettings);
                return availability;
            }
        }

        public async Task<Organization> CreateAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateOrganization(), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<Organization> RetrieveAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.RetrieveOrganization(id), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<Organization> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.DeleteOrganization(id), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<Organization> UploadLogoAsync(string id, Stream file, CancellationToken cancellationToken = default)
        {
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StreamContent(file), "file", "logo.jpg");
                using (var response = await client.PutAsync(Router.UploadLogo(id), form, cancellationToken).ConfigureAwait(false))
                {
                    await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                    var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
                using (var response = await client.PutAsync(Router.UploadCertificate(id), form, cancellationToken).ConfigureAwait(false))
                {
                    await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                    var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                    return organization;
                }
            }
        }

        public async Task<Certificate> DeleteCertificateAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.DeleteCertificate(id), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var certificateInfo = JsonConvert.DeserializeObject<Certificate>(resultString, this.jsonSettings);
                return certificateInfo;
            }
        }

        public async Task<Organization> UpdateLegalAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateLegal(id), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<Organization> UpdateReceiptSettingsAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateReceipts(id), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<Organization> UpdateCustomizationAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateCustomization(id), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<Organization> UpdateDomainAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateDomain(id), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<string> GetTestApiKeyAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.GetTestApiKey(id), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return resultString;
            }
        }


        public async Task<string> RenewTestApiKeyAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(""))
            using (var response = await client.PutAsync(Router.RenewTestApiKey(id), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return resultString;
            }
        }

        public async Task<LiveApiKey> ListLiveApiKeysAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListAsyncLiveApiKeys(id), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listResult = JsonConvert.DeserializeObject<LiveApiKey>(resultString, this.jsonSettings);

                return listResult;
            }
        }

        public async Task<string> RenewLiveApiKeyAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(""))
            using (var response = await client.PutAsync(Router.RenewLiveApiKey(id), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return resultString;
            }
        }


        public async Task<List<SeriesGroup>> ListSeriesAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListSeriesGroup(id), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var searchResult = JsonConvert.DeserializeObject<List<SeriesGroup>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<SeriesGroup> CreateSeriesAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateSeriesGroup(id), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var series = JsonConvert.DeserializeObject<SeriesGroup>(resultString, this.jsonSettings);
                return series;
            }
        }

        public async Task<SeriesGroup> UpdateSeriesAsync(string id, string seriesName, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateSeriesGroup(id, seriesName), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var series = JsonConvert.DeserializeObject<SeriesGroup>(resultString, this.jsonSettings);
                return series;
            }
        }
        
        public async Task<SeriesGroup> DeleteSeriesAsync(string id, string seriesName, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.DeleteSeriesGroup(id, seriesName), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var series = JsonConvert.DeserializeObject<SeriesGroup>(resultString, this.jsonSettings);
                return series;
            }
        }

        public async Task<Organization> UpdateDefaultSeriesAsync(string organizationId, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateDefaultSeries(organizationId), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }
        
        public async Task<List<LiveApiKey>> DeleteLiveApiKeyAsync(string id, string apiKeyId, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.DeleteLiveApiKey(id, apiKeyId), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var deserializeJson = JsonConvert.DeserializeObject<List<LiveApiKey>>(resultString, this.jsonSettings);
                return deserializeJson;
            }
        }

        public async Task<Organization> UpdateSelfInvoiceSettingsAsync(string organizationId, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateSelfInvoiceSettings(organizationId), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var organization = JsonConvert.DeserializeObject<Organization>(resultString, this.jsonSettings);
                return organization;
            }
        }

        public async Task<List<OrganizationUserAccess>> ListTeamAccessAsync(string organizationId, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListTeamAccess(organizationId), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<List<OrganizationUserAccess>>(resultString, this.jsonSettings);
            }
        }

        public async Task<OrganizationUserAccess> RetrieveTeamAccessAsync(string organizationId, string accessId, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.RetrieveTeamAccess(organizationId, accessId), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<OrganizationUserAccess>(resultString, this.jsonSettings);
            }
        }

        public async Task<OrganizationUserAccess> UpdateTeamAccessRoleAsync(string organizationId, string accessId, string role, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(new Dictionary<string, object> { ["role"] = role }), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateTeamAccessRole(organizationId, accessId), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<OrganizationUserAccess>(resultString, this.jsonSettings);
            }
        }

        public async Task<bool> RemoveTeamAccessAsync(string organizationId, string accessId, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.RetrieveTeamAccess(organizationId, accessId), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return DeserializeOk(resultString);
            }
        }

        public async Task<List<OrganizationInvite>> ListSentTeamInvitesAsync(string organizationId, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListSentTeamInvites(organizationId), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<List<OrganizationInvite>>(resultString, this.jsonSettings);
            }
        }

        public async Task<OrganizationInvite> InviteUserToTeamAsync(string organizationId, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.ListSentTeamInvites(organizationId), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<OrganizationInvite>(resultString, this.jsonSettings);
            }
        }

        public async Task<bool> CancelTeamInviteAsync(string organizationId, string inviteKey, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.CancelTeamInvite(organizationId, inviteKey), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return DeserializeOk(resultString);
            }
        }

        public async Task<List<OrganizationInvite>> ListReceivedTeamInvitesAsync(CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListReceivedTeamInvites(), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<List<OrganizationInvite>>(resultString, this.jsonSettings);
            }
        }

        public async Task<bool> RespondTeamInviteAsync(string inviteKey, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.RespondTeamInvite(inviteKey), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return DeserializeOk(resultString);
            }
        }

        public async Task<List<OrganizationTeamRole>> ListTeamRolesAsync(string organizationId, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListTeamRoles(organizationId), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<List<OrganizationTeamRole>>(resultString, this.jsonSettings);
            }
        }

        public async Task<List<OrganizationTeamRoleTemplate>> ListTeamRoleTemplatesAsync(string organizationId, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListTeamRoleTemplates(organizationId), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<List<OrganizationTeamRoleTemplate>>(resultString, this.jsonSettings);
            }
        }

        public async Task<List<string>> ListTeamRoleOperationsAsync(string organizationId, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListTeamRoleOperations(organizationId), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<List<string>>(resultString, this.jsonSettings);
            }
        }

        public async Task<OrganizationTeamRole> RetrieveTeamRoleAsync(string organizationId, string roleId, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.RetrieveTeamRole(organizationId, roleId), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<OrganizationTeamRole>(resultString, this.jsonSettings);
            }
        }

        public async Task<OrganizationTeamRole> CreateTeamRoleAsync(string organizationId, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.ListTeamRoles(organizationId), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<OrganizationTeamRole>(resultString, this.jsonSettings);
            }
        }

        public async Task<OrganizationTeamRole> UpdateTeamRoleAsync(string organizationId, string roleId, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.RetrieveTeamRole(organizationId, roleId), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<OrganizationTeamRole>(resultString, this.jsonSettings);
            }
        }

        public async Task<bool> DeleteTeamRoleAsync(string organizationId, string roleId, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.RetrieveTeamRole(organizationId, roleId), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return DeserializeOk(resultString);
            }
        }

        private bool DeserializeOk(string resultString)
        {
            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(resultString, this.jsonSettings);
            if (result != null && result.TryGetValue("ok", out var okValue))
            {
                if (okValue is bool okBool)
                {
                    return okBool;
                }
                if (bool.TryParse(okValue.ToString(), out var parsed))
                {
                    return parsed;
                }
            }

            return false;
        }
    }
}
