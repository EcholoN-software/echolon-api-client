using System.Threading;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Client.GraphQl.Administration
{
    public interface IAdministration<TItem, TItemInput>
    {
        public Task<GraphQlResponse<TItem[]>> All(CancellationToken cancellationToken = default);

        public Task<GraphQlResponse<TItem>> One(ItemId<TItem> id, CancellationToken cancellationToken = default);

        public Task<GraphQlResponse<TItem>> Default(CancellationToken cancellationToken = default);

        public Task<GraphQlResponse<TItem>> Store(TItemInput item, CancellationToken cancellationToken = default);

        // public Task<GraphQlResponse> Delete(ItemId<TItem> id);

        public Task<GraphQlResponse<TItem>> Activate(ItemId<TItem> id, CancellationToken cancellationToken = default);
        
        public Task<GraphQlResponse<TItem>> Deactivate(ItemId<TItem> id, CancellationToken cancellationToken = default);
    }
}
