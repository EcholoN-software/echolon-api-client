using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.Results;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public interface ISystemClient
    {
        Task<GraphQlResponse<SystemDataSources[]>> SystemDataSources(string[] ids);
        Task<GraphQlResponse<SystemViews[]>> SystemViews(SystemViewInput? input);
        Task<GraphQlResponse<SystemPrivileges[]>> SystemPrivileges();
        Task<GraphQlResponse<SystemPropertySets[]>> SystemProperties();
        Task<GraphQlResponse<SystemIndividuals[]>> SystemIndividuals(string[]? subjects);
    }
}