using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Model;

namespace Eco.Echolon.ApiClient.Filter.Builder.FilterBuilder
{
    public class ObjectComparisonBuilder<TComparison> where TComparison : SingleValueFieldComparison<object?>
    {
        private readonly GroupBuilder _builder;

        public ObjectComparisonBuilder(GroupBuilder builder)
        {
            _builder = builder;
        }

        public GroupBuilder Custom<T>(string fieldName, object? value) where T : SingleValueFilter<object?>
        {
            return _builder.Field<TComparison, T, object>(fieldName, value);
        }
        
        public GroupBuilder CustomConstant(string fieldName, object? value)
        {
            return _builder.Field<TComparison, ConstantValue<object?>, object?>(fieldName, value);
        }

        public GroupBuilder Constant(string fieldName, string value)
        {
            return EqualsConstant(fieldName, value);
        }

        public GroupBuilder Constant(string fieldName, double value)
        {
            return EqualsConstant(fieldName, value);
        }

        public GroupBuilder Constant(string fieldName, Identity value)
        {
            return EqualsConstant(fieldName, value);
        }

        public GroupBuilder Variable<T>(string fieldName, string value)
        {
            return _builder.Field<TComparison, VariableValue<object?>, object?>(fieldName, value);
        }

        private GroupBuilder EqualsConstant<T>(string fieldName, T value)
        {
            return _builder.Field<TComparison, ConstantValue<object?>, object?>(fieldName, value);
        }

        public GroupBuilder Null(string fieldName)
        {
            return _builder.Field<TComparison, NullValue, object?>(fieldName, null);
        }
    }
}