using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public class GreaterThanFilter : SingleValueFieldComparison
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public GreaterThanFilter(string field, SingleValueFilter value) : base(field, value)
        {
        }
    }
}