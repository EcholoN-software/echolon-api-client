using System.Collections.Generic;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.Results;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public class EntitiesClient : IEntitiesClient
    {
        private readonly IBaseClient _baseClient;

        public EntitiesClient(IBaseClient baseClient)
        {
            _baseClient = baseClient;
        }
        public async Task<GraphQlResponse<EntityDefinitionResult[]?>> GetAll()
        {
            return await _baseClient.QueryCustom<EntityDefinitionResult[]>(new[] { "entities", "all" });
        }

        public async Task<GraphQlResponse<EntityDefinitionResult?>> Get(string id)
        {
            return await _baseClient.QueryCustom<EntityDefinitionResult>(new[] { "entities", "one" },
                new Dictionary<string, object>() { { "id", id } });
        }

        public async Task<GraphQlResponse<EntityDefinitionResult?>> GetNew()
        {
            return await _baseClient.QueryCustom<EntityDefinitionResult>(new[] { "entities", "new" });
        }

    }
}