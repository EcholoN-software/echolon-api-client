using System.Threading;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.Results;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public interface ISystemClient
    {
        Task<GraphQlResponse<SystemDataSources[]>> SystemDataSources(string[] ids, CancellationToken cancellationToken = default);
        Task<GraphQlResponse<SystemViews[]>> SystemViews(SystemViewInput? input, CancellationToken cancellationToken = default);
        Task<GraphQlResponse<SystemPrivileges[]>> SystemPrivileges(CancellationToken cancellationToken = default);
        Task<GraphQlResponse<SystemPropertySets[]>> SystemProperties(CancellationToken cancellationToken = default);
        Task<GraphQlResponse<SystemIndividuals[]>> SystemIndividuals(string[]? subjects, CancellationToken cancellationToken = default);
    }
}
