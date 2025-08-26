using System;
using Newtonsoft.Json;

namespace Eco.Echolon.ApiClient.Model.DomainTypes
{
    public class DomainTypeConverter<T> : JsonConverter<DomainType<T>> where T : notnull
    {
        public override void WriteJson(JsonWriter writer, DomainType<T>? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value == null ? null : value.Value);
        }

        public override DomainType<T>? ReadJson(JsonReader reader, Type objectType, DomainType<T>? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var value = serializer.Deserialize(reader, typeof(T));
            return Activator.CreateInstance(objectType, value == null ? null : (T) value) as DomainType<T>;
        }
    }
}