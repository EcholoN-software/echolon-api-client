using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public class GreaterOrEqualFilter : SingleValueFieldComparison
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public GreaterOrEqualFilter(string field, SingleValueFilter value) : base(field, value)
        {
        }
    }
}