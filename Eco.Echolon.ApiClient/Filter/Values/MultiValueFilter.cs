using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter.Values
{
    public abstract class MultiValueFilter<TValue> : IValueFilter<TValue>
    {
        public abstract T Accept<T>(IGraphQlFilterVisitor<T> visitor);
    }
}