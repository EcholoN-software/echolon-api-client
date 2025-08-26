using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model.Modules.WorkPattern
{
    public class WorkPatternPreselectionBinding
    {
        public ItemId DataSourceId { get; set; } //TODO: ItemId<DataSource>
        public string Source { get; set; }
        public string Target { get; set; }
    }
}