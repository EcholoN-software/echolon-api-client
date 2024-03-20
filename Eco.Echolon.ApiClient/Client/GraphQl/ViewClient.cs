using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Filter;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Query;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public class ViewClient : IViewClient
    {
        private readonly IBaseClient _baseClient;

        public ViewClient(IBaseClient baseClient)
        {
            _baseClient = baseClient;
        }

        public Task<GraphQlResponse<T?>> ViewSingle<T>(string viewName, Identity identity,
            object? parameter = null) where T : class
        {
            return ViewSingle<T>(viewName, identity, null, parameter);
        }

        public async Task<GraphQlResponse<T?>> ViewSingle<T>(string viewName,
            Identity identity,
            uint? version = null,
            object? parameter = null) where T : class
        {
            var input = new Dictionary<string, object>() { ["id"] = identity };

            if (parameter is not null)
                input["params"] = parameter;
            //TODO: version
            return await _baseClient.QueryViewSingle<T>(viewName, version, input);
        }

        public Task<GraphQlResponse<CollectionWrapper<T>?>> ViewMultiple<T>(string viewName,
            int skip = 0,
            int first = 0,
            object? parameter = null,
            IEnumerable<string>? orderBy = null,
            IFilter? filter = null) where T : class
        {
            var order = orderBy?.Select(x => (x, false));
            return ViewMultiple<T>(viewName,
                null,
                skip,
                first,
                parameter,
                order,
                filter);
        }

        public async Task<GraphQlResponse<CollectionWrapper<T>?>> ViewMultiple<T>(string viewName,
            uint? version = null,
            int skip = 0,
            int first = 0,
            object? parameter = null,
            IEnumerable<(string fieldName, bool ascending)>? orderBy = null,
            IFilter? filter = null) where T : class
        {
            var input = new Dictionary<string, object> { };

            if (skip != 0)
                input["skip"] = skip;
            if (first != 0)
                input["first"] = first;
            if (orderBy != null && orderBy.Any())
                input["orderBy"] = orderBy;
            if (filter != null)
                input["where"] = filter;
            if (parameter is not null)
                input["params"] = parameter;

            return await _baseClient.QueryViewMultiple<T>(viewName, version, input);
        }
    }
}