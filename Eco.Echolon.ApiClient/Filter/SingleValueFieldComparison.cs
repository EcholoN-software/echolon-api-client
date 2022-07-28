using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public abstract class SingleValueFieldComparison : IFieldComparisonFilter<SingleValueFilter>
    {
        public string Field { get; }
        public ValueFilter Value { get; }

        protected SingleValueFieldComparison(string field, SingleValueFilter value)
        {
            Field = field;
            Value = value;
        }

        public abstract T Accept<T>(IGraphQlFilterVisitor<T> visitor);
    }
}