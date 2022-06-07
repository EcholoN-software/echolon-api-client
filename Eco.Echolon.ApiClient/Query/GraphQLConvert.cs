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
        public static string Serialize(object input)
        {
            if (input is IGraphQlFilter filter)
                return filter.Accept(new GraphQlFilterStringVisitor(new Dictionary<string, object>()));
            
            var jsonSerializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            using var stringWriter = new StringWriter();
            using var jsonTextWriter = new JsonTextWriter(stringWriter);
            
            jsonTextWriter.QuoteName = false;
            jsonSerializer.Serialize(jsonTextWriter, input);

            return stringWriter.ToString();
        }
    }
}