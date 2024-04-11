using System;
using System.Collections.Generic;
using Eco.Echolon.ApiClient.Model;

namespace Eco.Echolon.ApiClient.Query
{
    public class QueryProvider
    {
        private readonly QueryConfigurator _configurator;

        public QueryProvider(QueryConfigurator configurator)
        {
            _configurator = configurator;
        }

        public string GetMutationQuery<T>(string[] endpoint, WorkingEnqueueInput<T> input)
        {
            var mutation = QueryBuilder.Mutation(_configurator);
            var m = mutation;
            for (int i = 0; i < endpoint.Length; i++)
            {
                if (i + 1 == endpoint.Length)
                {
                    m.AddField(endpoint[i], typeof(MutationOutput), new Dictionary<string, object>() { { "input", input } });
                }
                else m.AddField(endpoint[i], x => m = x);
            }

            return mutation.ToString();
        }

        public string GetViewQueryMultiple<T>(string endpoint, string version, IDictionary<string, object>? input)
        {
            var query = QueryBuilder.Query(_configurator)
                .AddField("views", views => views
                    .AddField(endpoint, view => view
                        .AddField(version, ver => ver
                            .AddField("all", all => all
                                .AddField("count")
                                .AddField("skip")
                                .AddField("first")
                                .AddField("data", data => data
                                    .AddField("item", typeof(T))
                                ), input))));
            return query.ToString();
        }

        public string GetViewQuerySingle<T>(string endpoint, string version, IDictionary<string, object>? input)
        {
            return GetGraphQlQuery(new[] { "views", endpoint, version, "one", "item" }, input, typeof(T));
        }

        public string GetGraphQlQuery(string[] endpointPath, IDictionary<string, object>? input, Type returnType)
        {
            var query = QueryBuilder.Query(_configurator);
            var q = query;
            for (int i = 0; i < endpointPath.Length; i++)
            {
                if (i + 1 == endpointPath.Length)
                {
                    q.AddField(endpointPath[i], returnType, input);
                }
                else q.AddField(endpointPath[i], x => q = x);
            }

            return query.ToString();
        }
    }
}