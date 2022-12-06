using Eco.Echolon.ApiClient.Filter.Values;

namespace Eco.Echolon.ApiClient.Filter.Builder.FilterBuilder
{
    public class SingleComparisonBuilder<TComparison, TValue> where TComparison : SingleValueFieldComparison<TValue>
    {
        private readonly GroupBuilder _builder;

        public SingleComparisonBuilder(GroupBuilder builder)
        {
            _builder = builder;
        }

        public GroupBuilder Constant(string fieldName, TValue value)
        {
            return _builder.Field<TComparison, ConstantValue<TValue>, TValue>(fieldName, value);
        }

        public GroupBuilder Variable(string fieldName, string value)
        {
            return _builder.Field<TComparison, VariableValue<TValue>, TValue>(fieldName, value);
        }

        public GroupBuilder Custom<TFilter>(string fieldName, object value) where TFilter : SingleValueFilter<TValue>
        {
            return _builder.Field<TComparison, TFilter, TValue>(fieldName, value);
        }
    }
}