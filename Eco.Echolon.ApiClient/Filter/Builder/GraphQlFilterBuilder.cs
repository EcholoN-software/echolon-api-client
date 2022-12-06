using System;
using System.Linq;
using Eco.Echolon.ApiClient.Filter.Builder.FilterBuilder;
using Eco.Echolon.ApiClient.Filter.Values;

namespace Eco.Echolon.ApiClient.Filter.Builder
{
    public abstract class GraphQlFilterBuilder
    {
        public new static ObjectExpressionFilter<EqualsFilter> Equals => new();
        public static ObjectExpressionFilter<NotFilter> Not => new();
        public static SingleExpressionFilter<StartsWithFilter, string> StartsWith => new();
        public static SingleExpressionFilter<EndsWithFilter, string> EndsWith => new();
        public static SingleExpressionFilter<ContainsFilter, string> Contains => new();

        public static SingleExpressionFilter<LesserThanFilter, int> LesserThan => new();
        public static SingleExpressionFilter<LesserOrEqualFilter, int> LesserOrEqual => new();
        public static SingleExpressionFilter<GreaterThanFilter, int> GreaterThan => new();
        public static SingleExpressionFilter<GreaterOrEqualFilter, int> GreaterOrEqual => new();

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

        public static IAmEvaluateAble CreateField<TFilter, TValueType, TValue>(string fieldName, TValue value)
            where TFilter : IFieldComparisonFilter<TValueType, TValue>
            where TValueType : IValueFilter<TValue>
        {
            return new FieldBuilder<TFilter, TValueType, TValue>(fieldName, value).Build();
        }

        public static IAmEvaluateAble CreateField(Type filterType, Type valueType, string fieldName, object? value)
        {
            var compFilterInterface = filterType.GetInterfaces()
                .FirstOrDefault(
                    x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IFieldComparisonFilter<,>));

            if (compFilterInterface is null)
                throw new ArgumentException($"Type:{filterType} is not a valid Filter");

            if (!valueType.IsSubclassOf(compFilterInterface.GenericTypeArguments[0]))
                throw new ArgumentException($"{filterType} is not compatible with {valueType}");

            var constructorInfos = valueType.GetConstructors();
            if (constructorInfos.Any(x => x.GetParameters().Length > 0))
            {
                var valueCtor = constructorInfos
                    .FirstOrDefault(x => x.GetParameters()[0].ParameterType.IsInstanceOfType(value));

                if (valueCtor is null)
                    throw new ArgumentException(
                        $"value({value?.GetType()}) is not compatible with {compFilterInterface.GenericTypeArguments[1]}");
            }
            
            var fieldBuilderType = typeof(FieldBuilder<,,>)
                .MakeGenericType(filterType, valueType, compFilterInterface.GenericTypeArguments[1]);
            return ((ICreateFilter)Activator.CreateInstance(fieldBuilderType, fieldName, value)).Build();
        }
    }
}