using System;

namespace Eco.Echolon.ApiClient.Model.DomainTypes
{
    public class CorrelationId<T> : GuidDomainType
    {
        public CorrelationId(Guid val) : base(val)
        {
        }
    }
}