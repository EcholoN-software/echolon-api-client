using System.Collections.Generic;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Filter;
using Eco.Echolon.ApiClient.Model;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public interface IViewClient
    {
        Task<GraphQlResponse<T?>> ViewSingle<T>(string viewName, Identity identity) where T : class;
        Task<GraphQlResponse<T[]?>> ViewMultiple<T>(string viewName, int skip = 0, int first = 0,
            IEnumerable<string>? orderBy = null, IGraphQlFilter? filter = null) where T : class;
    }
}