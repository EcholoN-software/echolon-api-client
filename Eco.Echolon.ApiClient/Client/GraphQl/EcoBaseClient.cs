using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Authentication;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Query;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json.Linq;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public class EcoBaseClient : IBaseClient
    {
        private readonly EcholonApiClientConfiguration _config;
        private readonly QueryProvider _queryProvider;
        private GraphQLHttpClient Client { get; }


        public EcoBaseClient(IHttpClientFactory factory, EcholonApiClientConfiguration config,
            QueryProvider queryProvider)
        {
            _config = config;
            _queryProvider = queryProvider;
            Client = new GraphQLHttpClient(GetOptions(),
                new NewtonsoftJsonSerializer(),
                factory.CreateClient(Variables.HttpClientForApi));
        }

        public async Task<GraphQlResponse<MutationOutput[]?>> EnqueueWorkingMutation<T>(string endpoint,
            WorkingEnqueueInput<T> payload)
        {
            var r = new GraphQLHttpRequest(_queryProvider.GetMutationQuery(new[] { "working", endpoint }, payload));

            var rr = await Client
                .SendMutationAsync(r, () => new { working = new Dictionary<string, MutationOutput[]>() });

            var response = rr.Data.working[endpoint];
            var result = new GraphQlResponse<MutationOutput[]?>(response, TranslateError(rr?.Errors));

            return result;
        }

        public async Task<GraphQlResponse<T?>> QueryViewSingle<T>(string viewName, uint? version = null,
            IDictionary<string, object>? input = null)
            where T : class
        {
            var verStr = version is null ? "latest" : "r" + version.ToString();
            var request = new GraphQLHttpRequest(_queryProvider.GetViewQuerySingle<T>(viewName, verStr, input));
            var result = await Client.SendQueryAsync(request, () => new
            {   // views - view - version - one - item
                views = new Dictionary<string, Dictionary<string, Dictionary<string, ItemWrapper<T>>>>()
            });

            return new GraphQlResponse<T?>(result.Data.views[viewName][verStr]["one"].Item, TranslateError(result?.Errors));
        }

        public async Task<GraphQlResponse<CollectionWrapper<T>?>> QueryViewMultiple<T>(string viewName,
            uint? version = null, IDictionary<string, object>? input = null)
            where T : class
        {
            var verStr = version is null ? "latest" : "r" + version;
            var request = new GraphQLHttpRequest(_queryProvider.GetViewQueryMultiple<T>(viewName, verStr, input));
            var result = await Client.SendQueryAsync(request,
                () => new
                {
                    // views - view - version - all - data item
                    views = new Dictionary<string, Dictionary<string, Dictionary<string, CollectionWrapper<T>?>>>()
                });
            return new GraphQlResponse<CollectionWrapper<T>?>(result.Data.views[viewName][verStr]["all"],
                TranslateError(result?.Errors));
        }

        public async Task<GraphQlResponse<T?>> QueryCustom<T>(string[] path, IDictionary<string, object>? input = null)
            where T : class
        {
            var request = new GraphQLRequest(_queryProvider.GetGraphQlQuery(path, input, typeof(T)));
            var result = await Client.SendQueryAsync<JObject>(request);

            var obj = result.Data;
            T? data = null;

            for (var i = 0; i < path.Length; i++)
            {
                if (i == path.Length - 1)
                    data = obj?[path[i]]?.ToObject<T>();
                else
                    obj = obj?[path[i]] as JObject;
            }

            return new GraphQlResponse<T?>(data, TranslateError(result.Errors));
        }

        private GraphQlError[] TranslateError(GraphQLError[]? errors)
        {
            if (errors == null)
                return Array.Empty<GraphQlError>();
            var list = new List<GraphQlError>();

            foreach (var graphQlError in errors)
            {
                var locList = new List<ErrorLocation>();
                if (graphQlError.Locations != null)
                    foreach (var loc in graphQlError.Locations)
                        locList.Add(new ErrorLocation(Convert.ToInt32(loc.Line), Convert.ToInt32(loc.Column)));
                list.Add(new GraphQlError(graphQlError.Message, locList.ToArray()));
            }

            return list.ToArray();
        }

        private GraphQLHttpClientOptions GetOptions()
        {
            var apiUri = _config.ApiUri + "/graphql";
            return new GraphQLHttpClientOptions()
            {
                EndPoint = new Uri(apiUri),
            };
        }
    }
}