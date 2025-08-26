using Eco.Echolon.ApiClient.Model.CommonModels;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model.Modules.WorkPattern
{
    public class WorkPatternListItem : VersionedListItem<WorkPatternDefinition>
    {
        public string Number { get; }
        public string ImageKey { get; }
    }

    public class VersionedListItem<T> : ListItem<T>
    {
        public CorrelationId<T> CorrelationId { get; }
        public uint LatestDeployedVersions { get; }
        public uint LatestVersion { get; }
        public ItemId<T> LatestRevisionId { get; }
    }

    public class ListItem<T>
    {
        public string Name { get; set; }
        public ItemKey Key { get; set; }
        public UserTimeStamp Created { get; set; }
        public UserTimeStamp Modified { get; set; }
        public bool IsActive { get; set; }
    }
}