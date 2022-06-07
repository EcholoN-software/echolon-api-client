using System;

namespace Eco.Echolon.ApiClient.Model.DomainTypes
{
    public class EntityId : GuidDomainType
    {
        public EntityId(Guid val) : base(val)
        {
        }
    }
}