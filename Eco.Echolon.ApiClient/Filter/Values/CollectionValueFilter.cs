using System.Collections.Generic;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter.Values
{
    public class CollectionValueFilter : MultiValueFilter
    {
        public IEnumerable<ValueFilter> Value { get; }

        public CollectionValueFilter(IEnumerable<ValueFilter> value)
        {
            Value = value;
        }

        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}