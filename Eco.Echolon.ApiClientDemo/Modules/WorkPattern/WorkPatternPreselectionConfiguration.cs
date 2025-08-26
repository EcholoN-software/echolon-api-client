using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model.Modules.WorkPattern
{
    public class WorkPatternPreselectionConfiguration
    {
        public ItemId DataSourceId { get; set; } //TODO: ItemId<DataSource>
        public WorkPatternPreselectionBinding[] Bindings { get; set; }
    }
}