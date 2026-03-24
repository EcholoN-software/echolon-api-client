using System.Threading;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.Results;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public interface ITextTemplatesClient
    {
        Task<GraphQlResponse<FormattedTextTemplateAdminResult[]>> Get(CancellationToken cancellationToken = default);
        Task<GraphQlResponse<FormattedTextTemplateAdminResult>> Get(string id, CancellationToken cancellationToken = default);
        Task<GraphQlResponse<string>> Resolve(string id, Identity identity, CancellationToken cancellationToken = default);
    }
}
