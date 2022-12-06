using System;
using System.Collections.Generic;
using Eco.Echolon.ApiClient.Filter.Builder.FilterBuilder;
using Eco.Echolon.ApiClient.Filter.Values;


namespace Eco.Echolon.ApiClient.Filter.Builder
{
    public abstract class GroupBuilder : ICreateFilter
    {
        protected readonly List<ICreateFilter> Filters;

        protected GroupBuilder()
        {
            Filters = new List<ICreateFilter>();
            Equals = new ObjectComparisonBuilder<EqualsFilter>(this);
            Not = new ObjectComparisonBuilder<NotFilter>(this);
            StartsWith = new SingleComparisonBuilder<StartsWithFilter, string>(this);
            EndsWith = new SingleComparisonBuilder<EndsWithFilter, string>(this);
            LesserThan = new SingleComparisonBuilder<LesserThanFilter, int>(this);
            LesserOrEqual = new SingleComparisonBuilder<LesserOrEqualFilter, int>(this);
            GreaterThan = new SingleComparisonBuilder<GreaterThanFilter, int>(this);
            GreaterOrEqual = new SingleComparisonBuilder<GreaterOrEqualFilter, int>(this);
            Contains = new SingleComparisonBuilder<ContainsFilter, string>(this);
        }

        public virtual GroupBuilder Or(Action<OrBuilder> action)
        {
            var orFilter = new OrBuilder();
            Filters.Add(orFilter);
            action(orFilter);
            return this;
        }

        public virtual GroupBuilder And(Action<AndBuilder> action)
        {
            var orFilter = new AndBuilder();
            Filters.Add(orFilter);
            action(orFilter);
            return this;
        }

        public new ObjectComparisonBuilder<EqualsFilter> Equals { get; }
        public ObjectComparisonBuilder<NotFilter> Not { get; }
        public SingleComparisonBuilder<StartsWithFilter, string> StartsWith { get; }
        public SingleComparisonBuilder<EndsWithFilter, string> EndsWith { get; }

        public SingleComparisonBuilder<LesserThanFilter, int> LesserThan { get; }
        public SingleComparisonBuilder<LesserOrEqualFilter, int> LesserOrEqual { get; }
        public SingleComparisonBuilder<GreaterThanFilter, int> GreaterThan { get; }
        public SingleComparisonBuilder<GreaterOrEqualFilter, int> GreaterOrEqual { get; }
        public SingleComparisonBuilder<ContainsFilter, string> Contains { get; }


        internal GroupBuilder Field<TFilter, TValueType, TValue>(string fieldName, TValue value)
            where TFilter : IFieldComparisonFilter<TValueType, TValue>
            where TValueType : IValueFilter<TValue>
        {
            Filters.Add(new FieldBuilder<TFilter, TValueType, TValue>(fieldName, value));
            return this;
        }

        internal GroupBuilder Field<TFilter, TValueType, TValue>(string fieldName, object? value)
            where TFilter : IFieldComparisonFilter<TValueType, TValue>
            where TValueType : IValueFilter<TValue>
        {
            Filters.Add(new FieldBuilder<TFilter, TValueType, TValue>(fieldName, value));
            return this;
        }

        public abstract IAmEvaluateAble Build();
    }
}