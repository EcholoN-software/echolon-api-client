using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public abstract class MultiValueFieldComparison : IFieldComparisonFilter<MultiValueFilter>
    {
        public string Field { get; }
        public ValueFilter Value { get; }

        protected MultiValueFieldComparison(string field, MultiValueFilter value)
        {
            Field = field;
            Value = value;
        }

        public abstract T Accept<T>(IGraphQlFilterVisitor<T> visitor);
    }
}