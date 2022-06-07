using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public abstract class FieldComparisonGraphQlFilter : IGraphQlFilter
    {
        public abstract T Accept<T>(IGraphQlFilterVisitor<T> visitor);
    }


    public abstract class FieldComparisonGraphQlFilter<TU> : FieldComparisonGraphQlFilter where TU : ValueGraphQlFilter
    {
        public string Field { get; }
        public TU Value { get; }

        public FieldComparisonGraphQlFilter(string field, TU value)
        {
            Field = field;
            Value = value;
        }
    }

    public class EqComparisonGraphQlFilter : FieldComparisonGraphQlFilter<SingleValueGraphQlFilter>
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public EqComparisonGraphQlFilter(string field, SingleValueGraphQlFilter value) : base(field,
            value)
        {
        }
    }

    public class NotComparisonGraphQlFilter : FieldComparisonGraphQlFilter<SingleValueGraphQlFilter>
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public NotComparisonGraphQlFilter(string field, SingleValueGraphQlFilter value) : base(field,
            value)
        {
        }
    }

    public class GreaterThanComparisonGraphQlFilter : FieldComparisonGraphQlFilter<SingleValueGraphQlFilter>
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public GreaterThanComparisonGraphQlFilter(string field, SingleValueGraphQlFilter value) : base(
            field, value)
        {
        }
    }

    public class GreaterOrEqualComparisonGraphQlFilter : FieldComparisonGraphQlFilter<SingleValueGraphQlFilter>
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public GreaterOrEqualComparisonGraphQlFilter(string field, SingleValueGraphQlFilter value) : base(
            field, value)
        {
        }
    }

    public class LesserThanComparisonGraphQlFilter : FieldComparisonGraphQlFilter<SingleValueGraphQlFilter>
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public LesserThanComparisonGraphQlFilter(string field, SingleValueGraphQlFilter value) : base(
            field, value)
        {
        }
    }

    public class LesserOrEqualComparisonGraphQlFilter : FieldComparisonGraphQlFilter<SingleValueGraphQlFilter>
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public LesserOrEqualComparisonGraphQlFilter(string field, SingleValueGraphQlFilter value) : base(
            field, value)
        {
        }
    }

    public class InComparisonGraphQlFilter : FieldComparisonGraphQlFilter<CollectionValueGraphQlFilter>
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public InComparisonGraphQlFilter(string field, CollectionValueGraphQlFilter value) : base(
            field, value)
        {
        }
    }

    public class StartsWithComparisonGraphQlFilter : FieldComparisonGraphQlFilter<SingleValueGraphQlFilter>
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public StartsWithComparisonGraphQlFilter(string field, SingleValueGraphQlFilter value) : base(
            field, value)
        {
        }
    }

    public class EndsWithComparisonGraphQlFilter : FieldComparisonGraphQlFilter<SingleValueGraphQlFilter>
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public EndsWithComparisonGraphQlFilter(string field, SingleValueGraphQlFilter value) : base(
            field, value)
        {
        }
    }

    public class ContainsComparisonGraphQlFilter : FieldComparisonGraphQlFilter<SingleValueGraphQlFilter>
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public ContainsComparisonGraphQlFilter(string field, SingleValueGraphQlFilter value) : base(
            field, value)
        {
        }
    }

    public class NullComparisonGraphQlFilter : FieldComparisonGraphQlFilter<NullValueGraphQlFilter>
    {
        public override T Accept<T>(IGraphQlFilterVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public NullComparisonGraphQlFilter(string field) : base(
            field, new NullValueGraphQlFilter())
        {
        }
    }
}
