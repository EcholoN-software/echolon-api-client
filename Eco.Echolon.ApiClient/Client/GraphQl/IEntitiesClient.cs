using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.Results;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public interface IEntitiesClient
    {
        Task<GraphQlResponse<EntityDefinitionResult[]?>> GetAll();
        Task<GraphQlResponse<EntityDefinitionResult?>> Get(string id);
        Task<GraphQlResponse<EntityDefinitionResult?>> GetNew();
        // Task<GraphQlResponse<object>> EntitiesStore(EntityDefinitionInput entity);
        // Task<GraphQlResponse<object>> EntitiesDelete(string id);
        // Task<GraphQlResponse<object>> EntitiesActivate(string id);
        // Task<GraphQlResponse<object>> EntitiesDeactivate(string id);
    }
}