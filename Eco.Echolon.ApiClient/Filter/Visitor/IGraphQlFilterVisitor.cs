using Eco.Echolon.ApiClient.Filter.Values;

namespace Eco.Echolon.ApiClient.Filter.Visitor
{
    public interface IGraphQlFilterVisitor<out T>
    {
        T Visit<TValue>(VariableValue<TValue> filter);
        T Visit(AndFilter filter);
        T Visit(OrFilter filter);
        T Visit<TValue>(ConstantValue<TValue> filter);
        T Visit(EqualsFilter filter);
        T Visit(NotFilter filter);
        T Visit(GreaterThanFilter filter);
        T Visit(GreaterOrEqualFilter filter);
        T Visit(LesserThanFilter filter);
        T Visit(LesserOrEqualFilter filter);
        T Visit(NullValue filter);
        T Visit(InFilter filter);
        T Visit(StartsWithFilter filter);
        T Visit(EndsWithFilter filter);
        T Visit(ContainsFilter filter);
        T Visit<TValue>(CollectionValueFilter<TValue> filter);
    }
}
