using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;
using Eco.Echolon.ApiClient.Model.WorkingQueue;

namespace Eco.Echolon.ApiClient.Client.RestApi
{
    public interface IWorkingQueueClient
    {
        Task<ApiResult<WorkQueuePointer[]>> Get();
        Task<ApiResult> Dequeue(WorkingQueueId id);
    }
}