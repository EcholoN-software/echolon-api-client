using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model.WorkingQueue
{
    public class WorkQueuePointer
    {
        public WorkingQueueId Id { get; set; }
        public int Number { get; set; }
        public string Reference { get; set; }
        public PointerStatus Status { get; set; }

        public WorkQueuePointer(WorkingQueueId id, int number, string reference, PointerStatus status)
        {
            Id = id;
            Number = number;
            Reference = reference;
            Status = status;
        }
    }
}