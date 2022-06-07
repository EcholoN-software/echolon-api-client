using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Client.RestApi;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model.WorkingQueue
{
    public class WorkingQueueClient : IWorkingQueueClient
    {
        private readonly IBaseRestClient _baseClient;

        public WorkingQueueClient(IBaseRestClient baseClient)
        {
            _baseClient = baseClient;
        }
        
        public Task<ApiResult<WorkQueuePointer[]>> Get()
        {
            return _baseClient.Get();
        }

        public Task<ApiResult> Dequeue(WorkingQueueId id)
        {
            return _baseClient.Dequeue(id);
        }
    }
}