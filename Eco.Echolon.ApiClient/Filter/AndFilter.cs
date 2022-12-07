using System.Collections.Generic;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public class AndFilter : GroupFilter
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public AndFilter(IEnumerable<IAmEvaluateAble> filters) : base(filters)
        {
        }
    }
}