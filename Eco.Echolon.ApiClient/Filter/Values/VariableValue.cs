using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter.Values
{
    public class VariableValue : SingleValueFilter
    {
        public string VariableName { get; }

        public VariableValue(string variableName)
        {
            VariableName = variableName;
        }

        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}