using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model.Results
{
    public class SystemViews
    {
        public ItemId<SystemViews> Id { get; }
        public CorrelationId<SystemViews> CorrelationId { get; }
        public string Key { get; }
        public uint Version { get; }
        public string Name { get; }
        public string ViewFieldName { get; }
        public EntityId[] EntityId { get; }

        public SystemViews(ItemId<SystemViews> id,
            CorrelationId<SystemViews> correlationId,
            string key,
            uint version,
            string name,
            string viewFieldName,
            EntityId[] entityId)
        {
            Id = id;
            CorrelationId = correlationId;
            Key = key;
            Version = version;
            Name = name;
            ViewFieldName = viewFieldName;
            EntityId = entityId;
        }
    }
}