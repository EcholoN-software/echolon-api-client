using System;
using System.Linq;

namespace Eco.Echolon.ApiClient.Filter.Builder
{
    public class AndBuilder : GroupBuilder
    {
        public override GroupBuilder And(Action<AndBuilder> action)
        {
            return this;
        }

        public override IAmEvaluateAble Build()
        {
            return new AndFilter(Filters.Select(x => x.Build()));
        }
    }
}