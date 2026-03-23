using System.Threading;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Client.GraphQl.Administration
{
    public interface IVersionedAdministrationFor<TItem, TItemInput, TListItem> 
        // where TItem : VersionedItem<TItem>
    {
        public Task<GraphQlResponse<string[]>> UnsupportedFeatures(CancellationToken cancellationToken = default);

        public Task<GraphQlResponse<TItem[]>> All(CancellationToken cancellationToken = default);

        public Task<GraphQlResponse<TItem[]>> AllDeployed(CancellationToken cancellationToken = default);

        public Task<GraphQlResponse<TListItem[]>> AllList(CancellationToken cancellationToken = default);

        public Task<GraphQlResponse<TItem[]>> Revisions(CorrelationId<TItem> id, CancellationToken cancellationToken = default);

        public Task<GraphQlResponse<TItem>> One(ItemId<TItem> id, CancellationToken cancellationToken = default);

        public Task<GraphQlResponse<TItem>> Latest(CorrelationId<TItem> id, CancellationToken cancellationToken = default);

        public Task<GraphQlResponse<TItem>> Default(CancellationToken cancellationToken = default);

        // public Task<GraphQlResponse<ItemInfo[]>> Dependants(ItemId<TItem> id);

        public Task<GraphQlResponse<TItem>> Store(TItemInput item, CancellationToken cancellationToken = default);

        // public Task<GraphQlResponse> Delete(CorrelationId<TItem> id);
        //
        // public Task<GraphQlResponse> DeleteRevision(ItemId<TItem> id);

        public Task<GraphQlResponse<TItem>> Activate(CorrelationId<TItem> id, CancellationToken cancellationToken = default);

        public Task<GraphQlResponse<TItem>> Deactivate(CorrelationId<TItem> id, CancellationToken cancellationToken = default);

        public Task<GraphQlResponse<TItem>> Deploy(ItemId<TItem> id, CancellationToken cancellationToken = default);

        public Task<GraphQlResponse<TItem>> Revoke(ItemId<TItem> id, CancellationToken cancellationToken = default);

        public Task<GraphQlResponse<TItem>> Revert(ItemId<TItem> id, CancellationToken cancellationToken = default);
        
    }
}
