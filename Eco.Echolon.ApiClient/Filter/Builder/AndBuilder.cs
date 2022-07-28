using System;
using System.Linq;
using Eco.Echolon.ApiClient.Filter.Values;

namespace Eco.Echolon.ApiClient.Filter.Builder
{
    public class AndBuilder : GroupBuilder<AndBuilder>
    {
        internal AndBuilder() : base()
        {
        }

        public AndBuilder Or(Action<OrBuilder> action)
        {
            var orFilter = new OrBuilder();
            Filters.Add(orFilter);
            action(orFilter);
            return this;
        }

        public AndBuilder Field<TFilter, TValue>(string fieldName, object value)
            where TFilter : IFieldComparisonFilter<TValue>
            where TValue : ValueFilter
        {
            Filters.Add(new FieldBuilder<TFilter, TValue>(fieldName, value));
            return this;
        }
        
        public override IAmEvaluateAble Build()
        {
            return new AndFilter(Filters.Select(x => x.Build()));
        }
    }
}