using System.Collections.Generic;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public class OrFilter : GroupFilter
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public OrFilter(IEnumerable<IAmEvaluateAble> filters) : base(filters)
        {
        }
    }
}