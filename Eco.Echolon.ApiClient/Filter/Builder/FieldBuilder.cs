using System;
using Eco.Echolon.ApiClient.Filter.Values;

namespace Eco.Echolon.ApiClient.Filter.Builder
{
    public class FieldBuilder<TFilter, TValueType, TValue> : ICreateFilter
        where TFilter : IFieldComparisonFilter<TValueType, TValue>
        where TValueType : IValueFilter<TValue>
    {
        private readonly string _fieldName;
        private readonly object? _value;

        public FieldBuilder(string fieldName, object? value)
        {
            _fieldName = fieldName;
            _value = value;
        }

        public IAmEvaluateAble Build()
        {
            return (TFilter)Activator.CreateInstance(typeof(TFilter), _fieldName,
                typeof(TValueType) == typeof(NullValue)
                    ? Activator.CreateInstance(typeof(TValueType))
                    : Activator.CreateInstance(typeof(TValueType), _value));
        }
    }
}