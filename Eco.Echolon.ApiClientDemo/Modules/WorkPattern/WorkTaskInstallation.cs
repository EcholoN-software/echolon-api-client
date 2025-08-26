using System.Collections.Generic;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model.Modules.WorkPattern
{
    public class WorkTaskInstallation
    {
        public ItemId Id { get; set; }
        public ItemId ParentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Dictionary<string, object> Settings { get; set; }
        public WorkTaskParameterBinding[] ParameterBindings { get; set; }
    }

    public class WorkTaskParameterBinding
    {
        public string Key { get; set; }
        public string Path { get; set; }
        
    }
}