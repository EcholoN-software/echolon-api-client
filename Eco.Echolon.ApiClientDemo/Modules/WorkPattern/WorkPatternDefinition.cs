using System.Collections.Generic;
using Eco.Echolon.ApiClient.Model.CommonModels.Schema;
using Eco.Echolon.ApiClient.Model.CommonModels.Widget;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model.Modules.WorkPattern
{
    public class WorkPatternDefinition 
    {
        public CorrelationId<WorkPatternDefinition> CorrelationId { get; set; }
        public bool IsDeployed { get; set; }
        public bool IsLatestDeployed { get; set; }
        public string DeployedOn { get; set; }
        public int DeployedVersion { get; set; }
        public int Version { get; set; }
        public string VersionComment { get; set; }
        public string Name { get; set; }
        public ItemId<WorkPatternDefinition> Id { get; set; }
        public string Key { get; set; }
        public string Number { get; set; }
        public FormattedTextId Notes { get; set; }
        public string ImageKey { get; set; } = "rocket";
        public WidgetListElement[] Form { get; set; }
        public WorkTaskInstallation[] Installations { get; set; }
        public WorkPatternConfiguration Configuration { get; set; }

        public bool IsOfflineAvailable { get; set; }
        public SchemaDefinitionList Schema { get; set; }
        public bool IsContextSpecific { get; set; }
        public bool IsHidden { get; set; }
        public bool RenderAsFormSnapshot { get; set; }
        public string RequiredPrivilegeKey { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
    }
}