using System;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter.Values
{
    public class VariableValue<TValue> : LazyValueFilter<TValue>
    {
        public string VariableName { get; }

        public VariableValue(string variableName)
        {
            VariableName = variableName;
        }

        public bool HasCorrectType(object obj)
        {
            return obj is TValue;
        }

        public bool IsCorrectType(Type type)
        {
            return type == typeof(TValue);
        }

        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}