using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.Results;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public interface ITextTemplatesClient
    {
        Task<GraphQlResponse<FormattedTextTemplateAdminResult[]?>> Get();
        Task<GraphQlResponse<FormattedTextTemplateAdminResult?>> Get(string id);
        Task<GraphQlResponse<string?>> Resolve(string id, Identity identity);
    }
}