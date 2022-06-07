using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model
{
    public class Identity
    {
        public ItemId ItemId { get; set; }
        public EntityId EntityId { get; set; }

        public Identity(ItemId itemId, EntityId entityId)
        {
            ItemId = itemId;
            EntityId = entityId;
        }
    }
}