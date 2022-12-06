using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public class InFilter : MultiValueFieldComparison<object>
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public InFilter(string field, CollectionValueFilter<object> value) : base(field, value)
        {
        }
    }
}