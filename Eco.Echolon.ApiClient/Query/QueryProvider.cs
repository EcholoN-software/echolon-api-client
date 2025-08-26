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

        public string GetViewQuerySingle<T>(string endpoint, string version, IDictionary<string, object?>? input)
        {
            var query = QueryBuilder.Query(_configurator)
                .AddField("views",
                    views => views
                        .AddField(endpoint,
                            view => view
                                .AddField(version,
                                    ver => ver
                                        .AddField("one",
                                            one => one
                                                .AddField("item", typeof(T)),
                                            input))));
            return query.ToString();
        }

        public string GetViewQueryMultiple<T>(string endpoint, string version, IDictionary<string, object?>? input)
        {
            var query = QueryBuilder.Query(_configurator)
                .AddField("views",
                    views => views
                        .AddField(endpoint,
                            view => view
                                .AddField(version,
                                    ver => ver
                                        .AddField("all",
                                            all => all
                                                .AddField("count")
                                                .AddField("skip")
                                                .AddField("first")
                                                .AddField("data",
                                                    data => data
                                                        .AddField("item", typeof(T))
                                                ),
                                            input))));
            return query.ToString();
        }

        public string GetMutationQuery<T>(string[] endpoint, WorkingEnqueueInput<T> input)
        {
            return GetMutationQuery(endpoint,
                new Dictionary<string, object?>() { { "input", input } },
                typeof(MutationOutput));
        }

        public string GetMutationQuery(string[] endpoint, IDictionary<string, object?>? input, Type outputType)
        {
            var query = QueryBuilder.Mutation(_configurator);

            return AppendPathInputAndType(query, endpoint, input, outputType);
        }

        public string GetGraphQlQuery(string[] endpointPath, IDictionary<string, object?>? input, Type returnType)
        {
            var query = QueryBuilder.Query(_configurator);

            return AppendPathInputAndType(query, endpointPath, input, returnType);
        }

        [Obsolete(
            "Please use overload without isMutation Flag or GetMutationQuery. This will be removed in next major version.")]
        public string GetGraphQlQuery(string[] endpointPath,
            IDictionary<string, object?>? input,
            Type returnType,
            bool isMutation = false)
        {
            var query = isMutation ? QueryBuilder.Mutation(_configurator) : QueryBuilder.Query(_configurator);

            return AppendPathInputAndType(query, endpointPath, input, returnType);
        }

        private string AppendPathInputAndType(QueryBuilder qb,
            string[] path,
            IDictionary<string, object?>? input,
            Type returnType)
        {
            var currentQueryScope = qb;
            for (int i = 0; i < path.Length; i++)
            {
                if (i + 1 == path.Length)
                {
                    currentQueryScope.AddField(path[i], returnType, input);
                }
                else currentQueryScope.AddField(path[i], x => currentQueryScope = x);
            }

            return qb.ToString();
        }
    }
}