using System.Collections.Generic;
using System.Threading;
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

        public async Task<GraphQlResponse<FormattedTextTemplateAdminResult[]>> Get(
            CancellationToken cancellationToken = default)
        {
            return await _baseClient.Query<FormattedTextTemplateAdminResult[]>(new[] { "textTemplates", "all" },
                cancellationToken: cancellationToken);
        }

        public async Task<GraphQlResponse<FormattedTextTemplateAdminResult>> Get(string id,
            CancellationToken cancellationToken = default)
        {
            return await _baseClient.Query<FormattedTextTemplateAdminResult>(new[] { "textTemplates", "one" },
                new Dictionary<string, object?>() { { "id", id } }, cancellationToken);
        }

        public async Task<GraphQlResponse<string>> Resolve(string id,
            Identity? identity,
            CancellationToken cancellationToken = default)
        {
            var dicInput = new Dictionary<string, object?>() { { "id", id } };
            if (identity != null)
                dicInput.Add("rootItemIdentity", identity);
            return await _baseClient.Query<string>(new[] { "textTemplates", "resolve" },
                dicInput, cancellationToken);
        }
    }
}
