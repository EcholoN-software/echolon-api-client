using System.Collections.Generic;

namespace Eco.Echolon.ApiClient.Model.CommonModels.Widget
{
    public sealed class Widget
    {
        
        public Widget()
        {
            Properties = new Dictionary<string, object?>();
            Children = new List<Widget>();
            Metadata = new Dictionary<string, object?>();
        }
        
        public string? Key { get; set; }
        public string? Type { get; set; }
        public Dictionary<string, object?> Properties { get; set; }
        
        public Dictionary<string, DataBinding> DataBindings { get; set; }
        
        public List<Widget> Children { get; set; }
        public Dictionary<string, object?> Metadata { get; set; }
    }
}