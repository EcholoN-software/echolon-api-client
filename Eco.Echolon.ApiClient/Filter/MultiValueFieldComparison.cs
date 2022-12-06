using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public abstract class MultiValueFieldComparison<TValue> : IFieldComparisonFilter<MultiValueFilter<TValue>, TValue>
    {
        public string Field { get; }
        public IValueFilter<TValue> Value { get; }

        protected MultiValueFieldComparison(string field, MultiValueFilter<TValue> value)
        {
            Field = field;
            Value = value;
        }

        public abstract T Accept<T>(IGraphQlFilterVisitor<T> visitor);
    }
}