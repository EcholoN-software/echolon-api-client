using System.Collections.Generic;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Filter;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Query;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public interface IViewClient
    {
        Task<GraphQlResponse<T?>> ViewSingle<T>(string viewName, Identity identity, object? parameter = null) where T : class;
        
        Task<GraphQlResponse<T?>> ViewSingle<T>(string viewName, Identity identity, uint? version, object? parameter = null) where T : class;
        
        Task<GraphQlResponse<CollectionWrapper<T>?>> ViewMultiple<T>(string viewName, int skip = 0, int first = 0,
            object? parameter = null, IEnumerable<string>? orderBy = null, IFilter? filter = null) where T : class;
        
        Task<GraphQlResponse<CollectionWrapper<T>?>> ViewMultiple<T>(string viewName, uint? version, int skip = 0, int first = 0,
            object? parameter = null, IEnumerable<(string fieldName, bool ascending)>? orderBy = null, IFilter? filter = null) where T : class;
    }
}