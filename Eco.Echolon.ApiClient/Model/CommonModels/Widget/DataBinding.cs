using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model.CommonModels.Widget
{
    public sealed class DataBinding
    {
        public string? Type { get; set; }
        public BindingBehaviour Behaviour { get; set; }
        public string[]? Attributes { get; set; }
        public string[]? TargetPath { get; set; }
        // view id for lookup binding
        public ItemId? ViewId { get; set; }
        public DataBindingParameters[]? Parameters { get; set; }
        public object? Value { get; set; }
        // search definition id for search binding
        public ItemId? DefinitionId { get; set; }
        public string? Expression { get; set; }
        public string[]? ViewIdPath { get; set; }
    }
}