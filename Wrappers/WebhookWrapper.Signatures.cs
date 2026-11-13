#nullable enable
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public partial interface IWebhookWrapper
    {
        /// <summary>Verifica localmente la firma HMAC SHA-256 y devuelve el evento. Prefiere el payload JSON original.</summary>
        Task<ApiEvent> ValidateSignatureAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default);
        /// <summary>Verifica los bytes UTF-8 del payload JSON original, sin realizar solicitudes HTTP.</summary>
        Task<ApiEvent> ValidateSignatureAsync(string secret, string signature, string payload, CancellationToken cancellationToken = default);
    }

    public partial class WebhookWrapper
    {
        /// <summary>Verifica localmente una firma. El diccionario debe incluir secret, signature y payload.</summary>
        public Task<ApiEvent> ValidateSignatureAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (!data.TryGetValue("secret", out var secret) || !(secret is string secretText)) throw new ArgumentException("A webhook secret is required.", nameof(data));
            if (!data.TryGetValue("signature", out var signature) || !(signature is string signatureText)) throw new ArgumentException("A webhook signature is required.", nameof(data));
            if (!data.TryGetValue("payload", out var payload) || payload == null) throw new ArgumentException("A webhook payload is required.", nameof(data));
            return ValidateSignatureAsync(secretText, signatureText, payload is string json ? json : JsonConvert.SerializeObject(payload), cancellationToken);
        }

        /// <summary>Verifica la firma hexadecimal en tiempo constante y devuelve el evento firmado.</summary>
        public Task<ApiEvent> ValidateSignatureAsync(string secret, string signature, string payload, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (secret == null) throw new ArgumentNullException(nameof(secret));
            if (payload == null) throw new ArgumentNullException(nameof(payload));
            if (signature == null || signature.Length != 64) throw new ArgumentException("Invalid webhook signature.", nameof(signature));
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                var digest = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                var difference = 0;
                for (var index = 0; index < digest.Length; index++)
                {
                    byte supplied;
                    try { supplied = Convert.ToByte(signature.Substring(index * 2, 2), 16); }
                    catch (FormatException exception) { throw new ArgumentException("Invalid webhook signature.", nameof(signature), exception); }
                    difference |= digest[index] ^ supplied;
                }
                if (difference != 0) throw new ArgumentException("Invalid webhook signature.", nameof(signature));
            }
            return Task.FromResult(JsonConvert.DeserializeObject<ApiEvent>(payload, jsonSettings) ?? throw new JsonSerializationException("Invalid webhook event JSON."));
        }
    }
}
