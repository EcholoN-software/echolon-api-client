using System.Collections.Generic;

namespace Eco.Echolon.ApiClient.Model.CommonModels.Widget
{
    public sealed class Widget
    {
        public string? Key { get; set; }
        public string? Type { get; set; }
        public Dictionary<string, object?> Properties { get; set; } = new();

        public Dictionary<string, DataBinding> DataBindings { get; set; } = new();
        
        public List<Widget> Children { get; set; } = new();
        public Dictionary<string, object?> Metadata { get; set; } = new();
    }
}