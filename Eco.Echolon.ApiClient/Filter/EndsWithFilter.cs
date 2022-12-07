using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public class EndsWithFilter : SingleValueFieldComparison<string>
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public EndsWithFilter(string field, SingleValueFilter<string> value) : base(field, value)
        {
        }
    }
}