using System;
using System.Collections.Generic;
using System.IO;
using Eco.Echolon.ApiClient.Filter;
using Eco.Echolon.ApiClient.Filter.Visitor;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Eco.Echolon.ApiClient.Query
{
    public static class GraphQLConvert
    {
        public static string Serialize(object? input)
        {
            if (input is null)
                return "null";
            
            if (input is IFilter filter)
                return filter.Accept(new GraphQlFilterStringVisitor(new Dictionary<string, object>()));
            
            var jsonSerializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { 
                    new KeyValueDictionaryJsonConverter(),
                    new StringEnumNonQuotesConverter()
                }
            };
            using var stringWriter = new StringWriter();
            using var jsonTextWriter = new JsonTextWriter(stringWriter);
            
            jsonTextWriter.QuoteName = false;
            jsonSerializer.Serialize(jsonTextWriter, input);

            return stringWriter.ToString();
        }
    }

    public class StringEnumNonQuotesConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is null)
            {
                writer.WriteNull();
                return;
            }
            
            writer.WriteRawValue(Enum.GetName(value.GetType(), value));
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }

        public override bool CanRead => false;
    }
}