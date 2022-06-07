using System;
using Newtonsoft.Json;

namespace Eco.Echolon.ApiClient.Model.DomainTypes
{
    [JsonConverter(typeof(DomainTypeConverter<Guid>))]
    public abstract class GuidDomainType : DomainType<Guid>
    {
        protected GuidDomainType(Guid val) : base(val)
        {
        }
    }
}