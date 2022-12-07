using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public abstract class SingleValueFieldComparison<TValue> : IFieldComparisonFilter<SingleValueFilter<TValue>, TValue>
    {
        public string Field { get; }
        public IValueFilter<TValue> Value { get; }

        protected SingleValueFieldComparison(string field, SingleValueFilter<TValue> value)
        {
            Field = field;
            Value = value;
        }

        public abstract T Accept<T>(IGraphQlFilterVisitor<T> visitor);
    }
    
}