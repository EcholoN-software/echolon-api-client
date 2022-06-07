using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model
{
    public class MutationOutput
    {
        public WorkingQueueId Id { get; set; }
        public decimal Number { get; set; }
        public string Reference { get; set; }
        public string StatusCode { get; set; }

        public MutationOutput(WorkingQueueId id, decimal number, string reference, string statusCode)
        {
            Id = id;
            Number = number;
            Reference = reference;
            StatusCode = statusCode;
        }
    }
}