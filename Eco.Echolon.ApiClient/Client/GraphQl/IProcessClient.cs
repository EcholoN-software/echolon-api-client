using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.Results;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public interface IProcessClient
    {
        Task<GraphQlResponse<ProcessDefinitionResult?>> Get(string id);
        // Task<GraphQlResponse<object>> ProcessDefinitionsStore(ProcessDefinitionInput input);
        // Task<GraphQlResponse<object>> ProcessDefinitionsDelete(string id);
    }
}