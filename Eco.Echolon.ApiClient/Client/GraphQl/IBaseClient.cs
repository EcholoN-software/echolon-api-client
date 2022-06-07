using System.Collections.Generic;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public interface IBaseClient
    {
        Task<GraphQlResponse<MutationOutput[]?>> EnqueueWorkingMutation<T>(string endpoint, WorkingEnqueueInput<T> payload);
        Task<GraphQlResponse<T?>> QueryViewCustom<T>(string viewName, IDictionary<string, object>? input) where T : class;
        Task<GraphQlResponse<T?>> QueryCustom<T>(string[] path, IDictionary<string, object>? input = null) where T : class;
    }
}