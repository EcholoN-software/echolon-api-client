using System.Collections.Generic;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public abstract class GroupGraphQlFilter : IGraphQlFilter
    {
        public IEnumerable<FieldComparisonGraphQlFilter> Filters { get; }
        public abstract T Accept<T>(IGraphQlFilterVisitor<T> visitor);

        protected GroupGraphQlFilter(IEnumerable<FieldComparisonGraphQlFilter> filters)
        {
            Filters = filters;
        }
    }

    public class AndGraphQlFilter : GroupGraphQlFilter
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public AndGraphQlFilter(IEnumerable<FieldComparisonGraphQlFilter> filters) : base(filters)
        {
        }
    }

    public class OrGraphQlFilter : GroupGraphQlFilter
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public OrGraphQlFilter(IEnumerable<FieldComparisonGraphQlFilter> filters) : base(filters)
        {
        }
    }
}
