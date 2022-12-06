using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Model;

namespace Eco.Echolon.ApiClient.Filter.Builder.FilterBuilder
{
    public class ObjectExpressionFilter<TComparison> where TComparison : SingleValueFieldComparison<object?>
    {
        public IAmEvaluateAble Custom<T>(string fieldName, object? value) where T : SingleValueFilter<object?>
        {
            return new FieldBuilder<TComparison, T, object>(fieldName, value).Build();
        }
        
        public IAmEvaluateAble CustomConstant(string fieldName, object? value)
        {
            return new FieldBuilder<TComparison, ConstantValue<object?>, object?>(fieldName, value).Build();
        }

        public IAmEvaluateAble Constant(string fieldName, string value)
        {
            return EqualsConstant(fieldName, value);
        }

        public IAmEvaluateAble Constant(string fieldName, double value)
        {
            return EqualsConstant(fieldName, value);
        }

        public IAmEvaluateAble Constant(string fieldName, Identity value)
        {
            return EqualsConstant(fieldName, value);
        }

        public IAmEvaluateAble Variable(string fieldName, string value)
        {
            return new FieldBuilder<TComparison, VariableValue<object?>, object?>(fieldName, value).Build();
        }

        private IAmEvaluateAble EqualsConstant<T>(string fieldName, T value)
        {
            return new FieldBuilder<TComparison, ConstantValue<object?>, object?>(fieldName, value).Build();
        }

        public IAmEvaluateAble Null(string fieldName)
        {
            return new FieldBuilder<TComparison, NullValue, object?>(fieldName, null).Build();
        }
    }
}