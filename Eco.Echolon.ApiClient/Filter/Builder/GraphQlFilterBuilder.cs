using System;
using System.Linq;
using Eco.Echolon.ApiClient.Filter.Values;

namespace Eco.Echolon.ApiClient.Filter.Builder
{
    public abstract class GraphQlFilterBuilder
    {
        public static AndBuilder CreateAnd(Action<AndBuilder> action)
        {
            var andFilter = new AndBuilder();
            action(andFilter);
            return andFilter;
        }

        public static OrBuilder CreateOr(Action<OrBuilder> action)
        {
            var orFilter = new OrBuilder();
            action(orFilter);
            return orFilter;
        }

        public static FieldBuilder<TFilter, TValue> CreateField<TFilter, TValue>(string fieldName, object value)
            where TFilter : IFieldComparisonFilter<TValue>
            where TValue : ValueFilter
        {
            return new FieldBuilder<TFilter, TValue>(fieldName, value);
        }
        
        public static FieldBuilder<IsNullFilter, NullValue> CreateFieldIsNull(string fieldName)
        {
            return new FieldBuilder<IsNullFilter, NullValue>(fieldName, null);
        }

        public static ICreateFilter CreateField(Type filterType, Type valueType, string fieldName, object value)
        {
            var compFilterInterface = filterType.GetInterfaces()
                .FirstOrDefault(
                    x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IFieldComparisonFilter<>));
            
            if (compFilterInterface is null)
                throw new ArgumentException($"Type:{filterType} is not a valid Filter");

            if (!valueType.IsSubclassOf(compFilterInterface.GenericTypeArguments[0]))
                throw new ArgumentException($"{filterType} is not compatible with {valueType}");

            var fieldBuilderType = typeof(FieldBuilder<,>).MakeGenericType(filterType, valueType);
            return (ICreateFilter)Activator.CreateInstance(fieldBuilderType, fieldName, value);
        }
    }
}