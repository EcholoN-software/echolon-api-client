namespace Eco.Echolon.ApiClient.Filter.Visitor
{
    public interface IGraphQlFilterVisitor<out T>
    {
        T Visit(VariableValueGraphQlFilter filter);
        T Visit(AndGraphQlFilter filter);
        T Visit(OrGraphQlFilter filter);
        T Visit(ConstantValueGraphQlFilter filter);
        T Visit(EqComparisonGraphQlFilter filter);
        T Visit(NotComparisonGraphQlFilter filter);
        T Visit(GreaterThanComparisonGraphQlFilter filter);
        T Visit(GreaterOrEqualComparisonGraphQlFilter filter);
        T Visit(LesserThanComparisonGraphQlFilter filter);
        T Visit(LesserOrEqualComparisonGraphQlFilter filter);
        T Visit(NullValueGraphQlFilter filter);
        T Visit(InComparisonGraphQlFilter filter);
        T Visit(StartsWithComparisonGraphQlFilter filter);
        T Visit(NullComparisonGraphQlFilter filter);
        T Visit(EndsWithComparisonGraphQlFilter filter);
        T Visit(ContainsComparisonGraphQlFilter filter);
        T Visit(CollectionValueGraphQlFilter filter);
    }
}
