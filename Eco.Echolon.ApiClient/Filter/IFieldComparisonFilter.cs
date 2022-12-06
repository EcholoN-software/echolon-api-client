using Eco.Echolon.ApiClient.Filter.Values;

namespace Eco.Echolon.ApiClient.Filter
{
    public interface IFieldComparisonFilter<in TU, in TV> : IAmEvaluateAble where TU : IValueFilter<TV>
    {
        public string Field { get; }
        public IValueFilter<TV> Value { get; }
    }
}