using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eco.Echolon.ApiClient.Model.CommonModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Eco.Echolon.ApiClient.Query
{
    public class KeyValueDictionaryJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var dictionary = value as KeyValueDictionary;
            if (dictionary is null)
                return;
            writer.WriteStartArray();
            foreach (DictionaryEntry enty in dictionary)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("key");
                writer.WriteValue(enty.Key.ToString());
                writer.WritePropertyName("value");
                serializer.Serialize(writer, enty.Value);
                writer.WriteEndObject();
            }
            
            writer.WriteEndArray();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(KeyValueDictionary);
        }

        public override bool CanWrite => true;
        public override bool CanRead => false;
    }
}