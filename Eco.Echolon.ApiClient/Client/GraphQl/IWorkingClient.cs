using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public interface IWorkingClient
    {
        Task<GraphQlResponse<MutationOutput?>> WorkingWaitForResult<T>(string endpoint, WorkingEnqueueInput<T> payload);
    }
}