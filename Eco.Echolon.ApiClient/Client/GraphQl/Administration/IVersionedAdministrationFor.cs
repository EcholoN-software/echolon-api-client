using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Client.GraphQl.Administration
{
    public interface IVersionedAdministrationFor<TItem, TItemInput, TListItem> 
        // where TItem : VersionedItem<TItem>
    {
        public Task<GraphQlResponse<string[]>> UnsupportedFeatures();

        public Task<GraphQlResponse<TItem[]>> All();

        public Task<GraphQlResponse<TItem[]>> AllDeployed();

        public Task<GraphQlResponse<TListItem[]>> AllList();

        public Task<GraphQlResponse<TItem[]>> Revisions(CorrelationId<TItem> id);

        public Task<GraphQlResponse<TItem>> One(ItemId<TItem> id);

        public Task<GraphQlResponse<TItem>> Latest(CorrelationId<TItem> id);

        public Task<GraphQlResponse<TItem>> Default();

        // public Task<GraphQlResponse<ItemInfo[]>> Dependants(ItemId<TItem> id);

        public Task<GraphQlResponse<TItem>> Store(TItemInput item);

        // public Task<GraphQlResponse> Delete(CorrelationId<TItem> id);
        //
        // public Task<GraphQlResponse> DeleteRevision(ItemId<TItem> id);

        public Task<GraphQlResponse<TItem>> Activate(CorrelationId<TItem> id);

        public Task<GraphQlResponse<TItem>> Deactivate(CorrelationId<TItem> id);

        public Task<GraphQlResponse<TItem>> Deploy(ItemId<TItem> id);

        public Task<GraphQlResponse<TItem>> Revoke(ItemId<TItem> id);

        public Task<GraphQlResponse<TItem>> Revert(ItemId<TItem> id);
        
    }
}