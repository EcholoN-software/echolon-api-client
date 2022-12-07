using System.Collections.Generic;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public abstract class GroupFilter : IAmEvaluateAble
    {
        public IEnumerable<IAmEvaluateAble> Filters { get; }
        public abstract T Accept<T>(IGraphQlFilterVisitor<T> visitor);

        protected GroupFilter(IEnumerable<IAmEvaluateAble> filters)
        {
            Filters = filters;
        }
    }
}
