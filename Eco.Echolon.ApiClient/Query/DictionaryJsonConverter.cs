using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Eco.Echolon.ApiClient.Query
{
    public class DictionaryJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
            JsonSerializer serializer)
        {
            IDictionary? result;
            var genericArgs = objectType.GetGenericArguments();
            var keyType = genericArgs.Length == 2 ? genericArgs[0] : typeof(string);
            var valueType = genericArgs.Length == 2 ? genericArgs[1] : typeof(object);
            
                
            if (reader.TokenType == JsonToken.StartArray)
            {
                result = (IDictionary)typeof(Dictionary<,>).MakeGenericType(genericArgs).GetConstructor(Type.EmptyTypes)!.Invoke(null);
                JArray legacyArray = (JArray)JToken.ReadFrom(reader);
                for (var i = 0; i < legacyArray.Count; i++)
                {
                    var key = legacyArray[i]["key"]?.ToString();
                    var val = legacyArray[i]["value"]?.ToObject(valueType);
                    if (key is not null)
                    {
                        result.Add(key, val);
                    }
                }
            }
            else 
            {
                result = 
                    (IDictionary?)
                    serializer.Deserialize(reader, typeof(IDictionary));
            }

            return result;
        }

        public override bool CanConvert(Type objectType)
        {
            Type[] interfaces = objectType.IsInterface ? new[] { objectType } : objectType.GetInterfaces();
            if (objectType.Assembly.FullName.Contains("Newtonsoft"))
                return false;

            foreach (var inter in interfaces)
            {
                if (inter == typeof(IDictionary) ||
                    (inter.IsGenericType && inter.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool CanRead => true;
        public override bool CanWrite => false;
    }
}