using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model.Modules.WorkPattern
{
    public class WorkPatternAvailabilityConfiguration
    {
        public ItemId DataSourceId { get; set; } //TODO: ItemId<DataSource>
        public string Condition { get; set; }
    }
}