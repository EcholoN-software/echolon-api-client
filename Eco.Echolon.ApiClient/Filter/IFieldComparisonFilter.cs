using Eco.Echolon.ApiClient.Filter.Values;

namespace Eco.Echolon.ApiClient.Filter
{
    public interface IFieldComparisonFilter<in TU> : IAmEvaluateAble where TU : ValueFilter
    {
        public string Field { get; }
        public ValueFilter Value { get; }
    }
}