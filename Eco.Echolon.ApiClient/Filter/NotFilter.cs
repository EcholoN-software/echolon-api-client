using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public class NotFilter : SingleValueFieldComparison<object?>
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public NotFilter(string field, SingleValueFilter<object?> value) : base(field, value)
        {
        }
    }
}