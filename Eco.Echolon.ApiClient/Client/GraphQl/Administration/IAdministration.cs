using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Client.GraphQl.Administration
{
    public interface IAdministration<TItem, TItemInput>
    {
        public Task<GraphQlResponse<TItem[]>> All();

        public Task<GraphQlResponse<TItem>> One(ItemId<TItem> id);

        public Task<GraphQlResponse<TItem>> Default();

        public Task<GraphQlResponse<TItem>> Store(TItemInput item);

        // public Task<GraphQlResponse> Delete(ItemId<TItem> id);

        public Task<GraphQlResponse<TItem>> Activate(ItemId<TItem> id);
        
        public Task<GraphQlResponse<TItem>> Deactivate(ItemId<TItem> id);
    }
}