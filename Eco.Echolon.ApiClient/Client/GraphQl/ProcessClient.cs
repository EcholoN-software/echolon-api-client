using System.Collections.Generic;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.Results;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public class ProcessClient : IProcessClient
    {
        private readonly IBaseClient _baseClient;

        public ProcessClient(IBaseClient baseClient)
        {
            _baseClient = baseClient;
        }
        
        public async Task<GraphQlResponse<ProcessDefinitionResult?>> Get(string id)
        {
            return await _baseClient.QueryCustom<ProcessDefinitionResult>(new[] { "textTemplates", "resolve" },
                new Dictionary<string, object>(){{"id", id}});
        }
    }
}