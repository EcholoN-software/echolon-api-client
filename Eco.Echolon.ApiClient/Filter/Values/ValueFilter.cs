using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter.Values
{
    public abstract class ValueFilter : IFilter
    {
        public abstract T Accept<T>(IGraphQlFilterVisitor<T> visitor);
    }
}
