using System;
using System.Linq;
using Eco.Echolon.ApiClient.Filter.Values;

namespace Eco.Echolon.ApiClient.Filter.Builder
{
    public class OrBuilder : GroupBuilder<OrBuilder>
    {
        internal OrBuilder() : base()
        {
        }

        public OrBuilder And(Action<AndBuilder> action)
        {
            var orFilter = new AndBuilder();
            Filters.Add(orFilter);
            action(orFilter);
            return this;
        }

        public OrBuilder Field<TFilter, TValue>(string fieldName, object value)
            where TFilter : IFieldComparisonFilter<TValue>
            where TValue : ValueFilter
        {
            Filters.Add(new FieldBuilder<TFilter, TValue>(fieldName, value));
            return this;
        }
        
        public override IAmEvaluateAble Build()
        {
            return new OrFilter(Filters.Select(x => x.Build()));
        }
    }
}