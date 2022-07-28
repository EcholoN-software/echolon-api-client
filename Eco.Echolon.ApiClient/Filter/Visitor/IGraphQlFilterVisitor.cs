using Eco.Echolon.ApiClient.Filter.Values;

namespace Eco.Echolon.ApiClient.Filter.Visitor
{
    public interface IGraphQlFilterVisitor<out T>
    {
        T Visit(VariableValue filter);
        T Visit(AndFilter filter);
        T Visit(OrFilter filter);
        T Visit(ConstantValue filter);
        T Visit(EqualsFilter filter);
        T Visit(NotFilter filter);
        T Visit(GreaterThanFilter filter);
        T Visit(GreaterOrEqualFilter filter);
        T Visit(LesserThanFilter filter);
        T Visit(LesserOrEqualFilter filter);
        T Visit(NullValue filter);
        T Visit(InFilter filter);
        T Visit(StartsWithFilter filter);
        T Visit(IsNullFilter filter);
        T Visit(EndsWithFilter filter);
        T Visit(ContainsFilter filter);
        T Visit(CollectionValueFilter filter);
    }
}
