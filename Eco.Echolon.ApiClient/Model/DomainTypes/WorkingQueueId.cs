using System;

namespace Eco.Echolon.ApiClient.Model.DomainTypes
{
    public class WorkingQueueId : GuidDomainType
    {
        public WorkingQueueId(Guid val) : base(val)
        {
        }
    }
}