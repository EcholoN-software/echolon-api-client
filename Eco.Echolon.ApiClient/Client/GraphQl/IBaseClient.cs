using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Query;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public interface IBaseClient
    {
        Task<GraphQlResponse<MutationOutput[]?>> EnqueueWorkingMutation<T>(string endpoint,
            int? version,
            WorkingEnqueueInput<T> payload);

        Task<GraphQlResponse<T>> QueryViewSingle<T>(string viewName,
            uint? version = null,
            IDictionary<string, object?>? input = null) where T : class;

        Task<GraphQlResponse<CollectionWrapper<T>>> QueryViewMultiple<T>(string viewName,
            uint? version = null,
            IDictionary<string, object?>? input = null) where T : class;

        [Obsolete(
            "Please use Mutation() for mutations and Query() for queries. This will be deleted next major version.")]
        Task<GraphQlResponse<T>> QueryCustom<T>(string[] path,
            IDictionary<string, object?>? input = null,
            bool isMutation = false) where T : class;

        Task<GraphQlResponse<T>> Query<T>(string[] path, IDictionary<string, object?>? input = null) where T : class;

        Task<GraphQlResponse<T>> Mutation<T>(string[] path, IDictionary<string, object?>? input = null) where T : class;
    }
}