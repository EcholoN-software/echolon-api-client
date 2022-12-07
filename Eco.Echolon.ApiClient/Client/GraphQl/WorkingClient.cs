using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Client.RestApi;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public class WorkingClient : IWorkingClient
    {
        private readonly IBaseClient _baseClient;
        private readonly IBaseRestClient _restClient;

        public WorkingClient(IBaseClient baseClient, IBaseRestClient restClient)
        {
            _baseClient = baseClient;
            _restClient = restClient;
        }

        public async Task<GraphQlResponse<MutationOutput?>> WorkingWaitForResult<T>(string endpoint,
            WorkingEnqueueInput<T> payload)
        {
            var done = false;
            MutationOutput? result = null;
            var errors = new List<GraphQlError>();
            WorkingQueueId? queueId = null;

            while (!done)
            {
                var rr = await _baseClient.EnqueueWorkingMutation(endpoint, payload);

                errors.AddRange(rr.Errors);
                var response = rr.Data;
                result = response?.FirstOrDefault();

                if (result != null)
                    switch (result.StatusCode)
                    {
                        case "Success":
                            queueId = result?.Id;
                            done = true;
                            break;
                        case "Failure":
                            done = true;
                            break;
                        case "Pending":
                            await Task.Delay(1000);
                            break;
                    }
                else done = true;
            }

            if (queueId != null)
                await _restClient.Dequeue(queueId);

            return new GraphQlResponse<MutationOutput?>(result, errors.ToArray());
        }
    }
}