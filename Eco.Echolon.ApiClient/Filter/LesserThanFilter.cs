using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public class LesserThanFilter : SingleValueFieldComparison
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public LesserThanFilter(string field, SingleValueFilter value) : base(field, value)
        {
        }
    }
}