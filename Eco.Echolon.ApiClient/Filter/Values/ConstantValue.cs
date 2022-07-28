using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter.Values
{
    public class ConstantValue : SingleValueFilter
    {
        public object Value { get; }

        public ConstantValue(object value)
        {
            Value = value;
        }

        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}