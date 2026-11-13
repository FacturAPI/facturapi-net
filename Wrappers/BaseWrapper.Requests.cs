#nullable enable
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public abstract partial class BaseWrapper
    {
        /// <summary>Envía JSON o consulta parámetros y materializa la respuesta tipada.</summary>
        protected async Task<T> RequestAsync<T>(HttpMethod method, string path, Dictionary<string, object?>? query, object? body, bool hasJsonBody, CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(method, Router.UriWithQuery(path, query)))
            {
                if (hasJsonBody)
                    request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                    return await ReadResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>Envía archivos multipart usando el transporte y errores del cliente.</summary>
        protected async Task<T> RequestMultipartAsync<T>(HttpMethod method, string path, MultipartFormDataContent form, CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(method, path) { Content = form })
            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                return await ReadResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
        }

        private async Task<T> ReadResponseAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            await ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
            if (typeof(T) == typeof(Stream))
            {
                var memory = new MemoryStream();
                try
                {
                    using (var source = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        await source.CopyToAsync(memory, 81920, cancellationToken).ConfigureAwait(false);
                    memory.Position = 0;
                    return (T)(object)memory;
                }
                catch { memory.Dispose(); throw; }
            }
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync().ConfigureAwait(false), jsonSettings)!;
        }
    }
}
