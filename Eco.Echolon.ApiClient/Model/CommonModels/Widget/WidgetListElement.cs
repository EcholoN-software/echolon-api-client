using System.Collections.Generic;

namespace Eco.Echolon.ApiClient.Model.CommonModels.Widget
{
    public class WidgetListElement
    {
        public WidgetListElement(string? key,
            string? type,
            Dictionary<string, object?> properties,
            Dictionary<string, DataBinding> dataBindings,
            int[] childrenIndexes,
            Dictionary<string, object?> metadata)
        {
            Key = key;
            Type = type;
            Properties = properties;
            DataBindings = dataBindings;
            ChildrenIndexes = childrenIndexes;
            Metadata = metadata;
        }
        
        public string? Key { get; set; }
        public string? Type { get; set; }
        public Dictionary<string, object?> Properties { get; set; }
        public Dictionary<string, DataBinding> DataBindings { get; set; }
        public int[] ChildrenIndexes { get; set; }
        public Dictionary<string, object?> Metadata { get; set; }
    }
}