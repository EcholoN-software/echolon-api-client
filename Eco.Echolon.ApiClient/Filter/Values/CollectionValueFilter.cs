 using System.Collections.Generic;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter.Values
{
    public class CollectionValueFilter<TValue> : MultiValueFilter<TValue>
    {
        public IEnumerable<IValueFilter<TValue>> Value { get; }

        public CollectionValueFilter(IEnumerable<IValueFilter<TValue>> value)
        {
            Value = value;
        }

        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}