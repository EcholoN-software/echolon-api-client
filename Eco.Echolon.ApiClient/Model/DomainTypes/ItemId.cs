using System;

namespace Eco.Echolon.ApiClient.Model.DomainTypes
{
    public class ItemId : GuidDomainType
    {
        public ItemId(Guid val) : base(val)
        {
        }
    }
}