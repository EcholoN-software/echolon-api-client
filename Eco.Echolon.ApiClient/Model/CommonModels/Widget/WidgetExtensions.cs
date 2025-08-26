using System.Collections.Generic;
using System.Linq;

namespace Eco.Echolon.ApiClient.Model.CommonModels.Widget
{
    public static class WidgetExtensions
    {
        public static WidgetListElement[] ToList(this Widget widget)
        {
            var widgetList = new List<WidgetListElement>();
            
            int RecursiveCall(Widget child)
            {
                widgetList.Add(new WidgetListElement(child.Key, child.Type, child.Properties, child.DataBindings,
                    child.Children.Select(RecursiveCall).ToArray(), child.Metadata));
                return widgetList.Count;
            }
            
            var root = new WidgetListElement(widget.Key, widget.Type, widget.Properties, widget.DataBindings,
                widget.Children.Select(RecursiveCall).ToArray(), widget.Metadata);
            widgetList.Insert(0, root);
            return widgetList.ToArray();
        }
        
        public static Widget? ToWidget(this WidgetListElement[] widgetList)
        {
            if (widgetList.Length == 0)
                return null;
            
            Widget RecursiveCall(int i)
            {
                var widget = widgetList[i];
                var w = new Widget()
                {
                    Type = widget.Type,
                    Key = widget.Key,
                    Metadata = widget.Metadata,
                    Properties = widget.Properties,
                    Children = widget.ChildrenIndexes.Select(RecursiveCall).ToList()
                };
                w.DataBindings = new Dictionary<string, DataBinding>();
                foreach (var wDataBinding in widget.DataBindings)
                {
                    w.DataBindings.Add(wDataBinding.Key, wDataBinding.Value);
                }
                return w;
            }
            
            return RecursiveCall(0);
        }
    }
}