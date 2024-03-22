using System.Collections.Generic;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.Results;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public class TextTemplatesClient : ITextTemplatesClient
    {
        private readonly IBaseClient _baseClient;

        public TextTemplatesClient(IBaseClient baseClient)
        {
            _baseClient = baseClient;
        }
        
        public async Task<GraphQlResponse<FormattedTextTemplateAdminResult[]?>> Get()
        {
            return await _baseClient.QueryCustom<FormattedTextTemplateAdminResult[]>(new[] { "textTemplates", "all" });
        }

        public async Task<GraphQlResponse<FormattedTextTemplateAdminResult?>> Get(string id)
        {
            return await _baseClient.QueryCustom<FormattedTextTemplateAdminResult>(new[] { "textTemplates", "one" },
                new Dictionary<string, object>(){{"id", id}});
        }

        public async Task<GraphQlResponse<string?>> Resolve(string id,
            Identity? identity)
        {
            var dicInput = new Dictionary<string, object>(){{"id",id}};
            if(identity != null)
                dicInput.Add("rootItemIdentity", identity);
            return await _baseClient.QueryCustom<string>(new[] { "textTemplates", "resolve" },
                dicInput);
        }
    }
}