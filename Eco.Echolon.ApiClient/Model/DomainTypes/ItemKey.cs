using Newtonsoft.Json;

namespace Eco.Echolon.ApiClient.Model.DomainTypes
{
    [JsonConverter(typeof(DomainTypeConverter<string>))]
    public class ItemKey : DomainType<string>
    {
        public ItemKey(string val) : base(val)
        {
        }
    }
}