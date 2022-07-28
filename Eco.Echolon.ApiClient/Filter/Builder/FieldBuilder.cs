using System;
using Eco.Echolon.ApiClient.Filter.Values;

namespace Eco.Echolon.ApiClient.Filter.Builder
{
    public class FieldBuilder<TFilter, TValue> : ICreateFilter
        where TFilter : IFieldComparisonFilter<TValue>
        where TValue : ValueFilter
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
                typeof(TValue) == typeof(NullValue)
                    ? Activator.CreateInstance(typeof(TValue))
                    : Activator.CreateInstance(typeof(TValue), _value));
        }
    }
}