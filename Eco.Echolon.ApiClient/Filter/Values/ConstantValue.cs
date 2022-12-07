using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter.Values
{
    public class ConstantValue<TValue> : SingleValueFilter<TValue>
    {
        public TValue Value { get; }

        public ConstantValue(TValue value)
        {
            Value = value;
        }

        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}