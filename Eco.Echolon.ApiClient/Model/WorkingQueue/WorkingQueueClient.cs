using System.Threading;
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
        
        public Task<ApiResult<WorkQueuePointer[]>> Get(CancellationToken cancellationToken = default)
        {
            return _baseClient.Get(cancellationToken);
        }

        public Task<ApiResult> Dequeue(WorkingQueueId id, CancellationToken cancellationToken = default)
        {
            return _baseClient.Dequeue(id, cancellationToken);
        }
    }
}
