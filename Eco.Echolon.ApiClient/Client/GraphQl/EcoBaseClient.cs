using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Authentication;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public class EcoBaseClient : IBaseClient
    {
        private readonly EcholonApiClientConfiguration _config;
        private readonly QueryProvider _queryProvider;
        private HttpClient HttpClient { get; }


        public EcoBaseClient(IHttpClientFactory factory,
            EcholonApiClientConfiguration config,
            QueryProvider queryProvider)
        {
            _config = config;
            _queryProvider = queryProvider;
            HttpClient = factory.CreateClient(Variables.HttpClientForApi);
        }

        public async Task<GraphQlResponse<MutationOutput[]?>> EnqueueWorkingMutation<T>(string endpoint, int? version,
            WorkingEnqueueInput<T> payload)
        {
            var verStr = version is null ? "latest" : "r" + version.ToString();
            var path = new string[] { "working", endpoint, verStr };
            var query = _queryProvider.GetMutationQuery(path, payload);
            var r = await SendRequest(query);

            var result = new GraphQlResponse<MutationOutput[]?>(Deserialize<MutationOutput[]>(path, r.Data), r.Errors);

            return result;
        }

        public async Task<GraphQlResponse<T?>> QueryViewSingle<T>(string viewName, uint? version = null,
            IDictionary<string, object>? input = null)
            where T : class
        {
            var verStr = version is null ? "latest" : "r" + version.ToString();
            var path = new string[] { "views", viewName, verStr, "one" };
            var request = _queryProvider.GetViewQuerySingle<T>(viewName, verStr, input);
            var result = await SendRequest(request);

            return new GraphQlResponse<T?>(Deserialize<ItemWrapper<T>>(path, result.Data)?.Item, result.Errors);
        }

        public async Task<GraphQlResponse<CollectionWrapper<T>?>> QueryViewMultiple<T>(string viewName,
            uint? version = null, IDictionary<string, object>? input = null)
            where T : class
        {
            var verStr = version is null ? "latest" : "r" + version;
            var query = _queryProvider.GetViewQueryMultiple<T>(viewName, verStr, input);
            var request = await SendRequest(query);

            var path = new string[] { "views", viewName, verStr, "all" };

            return new GraphQlResponse<CollectionWrapper<T>?>(Deserialize<CollectionWrapper<T>>(path, request.Data),
                request.Errors);
        }

        public async Task<GraphQlResponse<T>> QueryCustom<T>(string[] path, IDictionary<string, object?>? input = null,
            bool isMutation = false)
            where T : class
        {
            var query = _queryProvider.GetGraphQlQuery(path, input, typeof(T), isMutation);
            var result = await SendRequest(query);

            return new GraphQlResponse<T>(Deserialize<T>(path, result.Data), result.Errors);
        }

        public async Task<GraphQlResponse<JObject>> SendRequest(string query)
        {
            var httpResp = await HttpClient.PostAsync(GetUri(), new GraphQlRequest(query));

            var contentResp = JObject.Parse(await httpResp.Content.ReadAsStringAsync(), new JsonLoadSettings());
            var errorList = new List<GraphQlError>();
            JObject? data = null;

            if (contentResp.ContainsKey("errors"))
            {
                var errors = contentResp.GetValue("errors") as JArray;

                if (errors is not null)
                    foreach (var error in errors)
                    {
                        if (error is not JObject errorObj)
                            continue;

                        var message = errorObj["message"]?.ToString();
                        var location = Deserialize<ErrorLocation[]>(new[] {"locations"}, errorObj);

                        errorList.Add(new GraphQlError(message, location ?? Array.Empty<ErrorLocation>()));
                    }
            }
            else
            {
                if (!httpResp.IsSuccessStatusCode)
                {
                    throw new GraphQlRequestException(httpResp.StatusCode, query, httpResp);
                }
            }

            if (contentResp.ContainsKey("data"))
            {
                data = contentResp.GetValue("data") as JObject;
            }

            return new GraphQlResponse<JObject>(data, errorList.ToArray());
        }

        private T? Deserialize<T>(string[] path, JObject? obj) where T : class
        {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new DictionaryJsonConverter());
            T? data = null;

            for (var i = 0; i < path.Length; i++)
            {
                if (i == path.Length - 1)
                    data = obj?[path[i]]?.ToObject<T>(serializer);
                else
                    obj = obj?[path[i]] as JObject;
            }

            return data;
        }

        private Uri GetUri()
        {
            var apiUri = _config.ApiUri.ToString()[_config.ApiUri.ToString().Length - 1] == '/'
                ? _config.ApiUri + "graphql"
                : _config.ApiUri + "/graphql";
            return new Uri(apiUri, UriKind.Absolute);
        }
        
        private class GraphQlRequest : StringContent
        {
            public GraphQlRequest(string query) : base(MakeQuery(query), Encoding.UTF8, "application/json")
            {
            }

            private static string MakeQuery(string query)
            {
                return $"{{\"query\": {JsonConvert.ToString(query)}}}";
            }
        }
    }
}