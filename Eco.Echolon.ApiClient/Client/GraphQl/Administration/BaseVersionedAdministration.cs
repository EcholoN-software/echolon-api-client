using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;
using Eco.Echolon.ApiClient.Query;

namespace Eco.Echolon.ApiClient.Client.GraphQl.Administration
{
    public class BaseVersionedAdministration<TItem, TItemInput, TListItem> :
        IVersionedAdministrationFor<TItem, TItemInput, TListItem> where TItem : class
    {
        private readonly string _moduleName;
        private readonly IBaseClient _graphClient;

        public BaseVersionedAdministration(string moduleName, IBaseClient graphClient)
        {
            _moduleName = moduleName;
            _graphClient = graphClient;
        }

        public Task<GraphQlResponse<string[]>> UnsupportedFeatures(CancellationToken cancellationToken = default)
        {
            return _graphClient.Query<string[]>(GetPath(nameof(UnsupportedFeatures)),
                cancellationToken: cancellationToken);
        }

        public Task<GraphQlResponse<TItem[]>> All(CancellationToken cancellationToken = default)
        {
            return _graphClient.Query<TItem[]>(GetPath(nameof(All)), cancellationToken: cancellationToken);
        }

        public Task<GraphQlResponse<TItem[]>> AllDeployed(CancellationToken cancellationToken = default)
        {
            return _graphClient.Query<TItem[]>(GetPath(nameof(AllDeployed)), cancellationToken: cancellationToken);
        }

        public Task<GraphQlResponse<TListItem[]>> AllList(CancellationToken cancellationToken = default)
        {
            return _graphClient.Query<TListItem[]>(GetPath(nameof(AllList)), cancellationToken: cancellationToken);
        }

        public Task<GraphQlResponse<TItem[]>> Revisions(CorrelationId<TItem> id,
            CancellationToken cancellationToken = default)
        {
            return _graphClient.Query<TItem[]>(GetPath(nameof(Revisions)),
                new Dictionary<string, object?>() { ["id"] = id }, cancellationToken);
        }

        public Task<GraphQlResponse<TItem>> One(ItemId<TItem> id, CancellationToken cancellationToken = default)
        {
            return _graphClient.Query<TItem>(GetPath(nameof(One)),
                new Dictionary<string, object?>() { ["id"] = id }, cancellationToken);
        }

        public Task<GraphQlResponse<TItem>> Latest(CorrelationId<TItem> id,
            CancellationToken cancellationToken = default)
        {
            return _graphClient.Query<TItem>(GetPath(nameof(One)),
                new Dictionary<string, object?>() { ["id"] = id }, cancellationToken);
        }

        public Task<GraphQlResponse<TItem>> Default(CancellationToken cancellationToken = default)
        {
            return _graphClient.Query<TItem>(GetPath(nameof(Default)), cancellationToken: cancellationToken);
        }

        public Task<GraphQlResponse<TItem>> Store(TItemInput item, CancellationToken cancellationToken = default)
        {
            return _graphClient.Mutation<TItem>(GetPath(nameof(Store)),
                new Dictionary<string, object?>() { [nameof(item)] = item }, cancellationToken);
        }

        // public Task<GraphQlResponse> Delete(CorrelationId<TItem> id)
        // {
        //     return _graphClient.QueryCustom<TItem>(GetPath(nameof(Store)),
        //         new Dictionary<string, object?>() { [nameof(id)] = id }, true);
        // }
        //
        // public Task<GraphQlResponse> DeleteRevision(ItemId<TItem> id)
        // {
        //     return _graphClient.QueryCustom<void>(GetPath(nameof(Store)),
        //         new Dictionary<string, object?>() { [nameof(id)] = id }, true);
        // }

        public Task<GraphQlResponse<TItem>> Activate(CorrelationId<TItem> id,
            CancellationToken cancellationToken = default)
        {
            return _graphClient.Mutation<TItem>(GetPath(nameof(Activate)),
                new Dictionary<string, object?>() { [nameof(id)] = id }, cancellationToken);
        }

        public Task<GraphQlResponse<TItem>> Deactivate(CorrelationId<TItem> id,
            CancellationToken cancellationToken = default)
        {
            return _graphClient.Mutation<TItem>(GetPath(nameof(Deactivate)),
                new Dictionary<string, object?>() { [nameof(id)] = id }, cancellationToken);
        }

        public Task<GraphQlResponse<TItem>> Deploy(ItemId<TItem> id, CancellationToken cancellationToken = default)
        {
            return _graphClient.Mutation<TItem>(GetPath(nameof(One)),
                new Dictionary<string, object?>() { [nameof(id)] = id }, cancellationToken);
        }

        public Task<GraphQlResponse<TItem>> Revoke(ItemId<TItem> id, CancellationToken cancellationToken = default)
        {
            return _graphClient.Mutation<TItem>(GetPath(nameof(One)),
                new Dictionary<string, object?>() { [nameof(id)] = id }, cancellationToken);
        }

        public Task<GraphQlResponse<TItem>> Revert(ItemId<TItem> id, CancellationToken cancellationToken = default)
        {
            return _graphClient.Mutation<TItem>(GetPath(nameof(One)),
                new Dictionary<string, object?>() { [nameof(id)] = id }, cancellationToken);
        }

        private string[] GetPath(string endpoint)
        {
            return new[] { "admin", _moduleName, endpoint.Uncapitalize() };
        }
    }
}
