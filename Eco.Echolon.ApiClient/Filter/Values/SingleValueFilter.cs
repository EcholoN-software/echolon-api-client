using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter.Values
{
    public abstract class SingleValueFilter<TValue> : IValueFilter<TValue>
    {
        public abstract T Accept<T>(IGraphQlFilterVisitor<T> visitor);
    }    
    public abstract class SingleValueFilter : SingleValueFilter<object?>
    {
    }

    public abstract class LazyValueFilter<TValue> : SingleValueFilter<TValue>
    {
    }
}