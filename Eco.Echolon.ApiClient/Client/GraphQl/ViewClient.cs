using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Filter;
using Eco.Echolon.ApiClient.Model;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public class ViewClient : IViewClient
    {
        private readonly IBaseClient _baseClient;

        public ViewClient(IBaseClient baseClient)
        {
            _baseClient = baseClient;
        }
        
        public async Task<GraphQlResponse<T?>> ViewSingle<T>(string viewName, Identity identity) where T : class
        {
            return await _baseClient.QueryViewCustom<T>(viewName, new Dictionary<string, object>() { { "id", identity } });

        }

        public async Task<GraphQlResponse<T[]?>> ViewMultiple<T>(string viewName, int skip = 0, int first = 0, IEnumerable<string>? orderBy = null,
            IGraphQlFilter? filter = null) where T : class
        {
            var parameter = new Dictionary<string, object>();
            parameter["skip"] = skip;

            if (first != 0)
                parameter["first"] = first;
            if (orderBy != null && orderBy.Any())
                parameter["orderBy"] = orderBy;
            if (filter != null)
                parameter["where"] = filter;

            return await _baseClient.QueryViewCustom<T[]>(viewName, parameter);
        }
    }
}