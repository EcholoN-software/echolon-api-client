using System;

namespace Eco.Echolon.ApiClient.Model.DomainTypes
{
    public class ItemId : GuidDomainType
    {
        public ItemId(Guid val) : base(val)
        {
        }
    }    
    public class ItemId<T> : GuidDomainType
    {
        public ItemId(Guid val) : base(val)
        {
        }
    }
}