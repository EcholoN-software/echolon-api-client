using Eco.Echolon.ApiClient.Filter.Values;

namespace Eco.Echolon.ApiClient.Filter.Builder.FilterBuilder
{
    public class SingleExpressionFilter<TComparison, TValue> where TComparison : SingleValueFieldComparison<TValue>
    {
        public IAmEvaluateAble Constant(string fieldName, TValue value)
        {
            return new FieldBuilder<TComparison, ConstantValue<TValue>, TValue>(fieldName, value).Build();
        }

        public IAmEvaluateAble Variable(string fieldName, string value)
        {
            return new FieldBuilder<TComparison, VariableValue<TValue>, TValue>(fieldName, value).Build();
        }

        public IAmEvaluateAble Custom<TFilter>(string fieldName, object value) where TFilter : SingleValueFilter<TValue>
        {
            return new FieldBuilder<TComparison, TFilter, TValue>(fieldName, value).Build();
        }
    }
}