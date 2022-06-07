using System.Collections.Generic;
using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public abstract class ValueGraphQlFilter : IGraphQlFilter
    {
        public abstract T Accept<T>(IGraphQlFilterVisitor<T> visitor);
    }

    public abstract class SingleValueGraphQlFilter : ValueGraphQlFilter
    {
    }

    public abstract class MultiValueGraphQlFilter : ValueGraphQlFilter
    {
    }

    public class ConstantValueGraphQlFilter : SingleValueGraphQlFilter
    {
        public object Value { get; set; }

        public ConstantValueGraphQlFilter(object value)
        {
            Value = value;
        }

        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class CollectionValueGraphQlFilter : MultiValueGraphQlFilter
    {
        public IEnumerable<ValueGraphQlFilter> Value { get; }

        public CollectionValueGraphQlFilter(IEnumerable<ValueGraphQlFilter> value)
        {
            Value = value;
        }

        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class VariableValueGraphQlFilter : SingleValueGraphQlFilter
    {
        public string VariableName { get; }

        public VariableValueGraphQlFilter(string variableName)
        {
            VariableName = variableName;
        }

        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class NullValueGraphQlFilter : SingleValueGraphQlFilter
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
