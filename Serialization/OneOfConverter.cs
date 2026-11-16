#nullable enable
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace Facturapi
{
    /// <summary>Valor de una alternativa del contrato.</summary>
    public interface IOneOfValue
    {
        /// <summary>Valor seleccionado, sin envoltura JSON.</summary>
        object? Value { get; }
    }

    internal sealed class OneOfConverter : JsonConverter
    {
        public override bool CanConvert(Type type) => typeof(IOneOfValue).IsAssignableFrom(Nullable.GetUnderlyingType(type) ?? type);

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, (value as IOneOfValue)?.Value);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Null) return null;
            var unionType = Nullable.GetUnderlyingType(objectType) ?? objectType;
            if (token is JObject data)
            {
                // A shape must fit exactly one alternative. Never silently discard
                // fields by selecting the first model that happens to accept JSON.
                var candidates = unionType.GetGenericArguments().Where(type =>
                {
                    var contract = serializer.ContractResolver.ResolveContract(type);
                    return type == typeof(object) || contract is JsonDictionaryContract ||
                        contract is JsonObjectContract model && data.Properties().All(property =>
                            model.ExtensionDataSetter != null ||
                            model.Properties.GetClosestMatchProperty(property.Name) is JsonProperty field &&
                            !field.Ignored && field.Writable);
                }).ToArray();
                if (candidates.Length != 1)
                    throw new JsonSerializationException("The JSON object must match exactly one documented alternative; its shape is ambiguous or contains unknown fields.");
                return Activator.CreateInstance(unionType, BindingFlags.Instance | BindingFlags.NonPublic, null,
                    new[] { token.ToObject(candidates[0], serializer) }, null);
            }
            foreach (var type in unionType.GetGenericArguments())
            {
                if (type == typeof(object) ||
                    token.Type == JTokenType.String && (type == typeof(string) || type == typeof(DateTime) || type == typeof(DateTimeOffset)) ||
                    token.Type == JTokenType.Date && (type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(string)) ||
                    token.Type == JTokenType.Boolean && type == typeof(bool) ||
                    (token.Type == JTokenType.Integer || token.Type == JTokenType.Float) && (type == typeof(int) || type == typeof(decimal) || type == typeof(double)) ||
                    token.Type == JTokenType.Array && typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string) && !typeof(IDictionary).IsAssignableFrom(type))
                    return Activator.CreateInstance(unionType, BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { token.ToObject(type, serializer) }, null);
            }
            throw new JsonSerializationException("The JSON value does not match any documented alternative.");
        }
    }
}
