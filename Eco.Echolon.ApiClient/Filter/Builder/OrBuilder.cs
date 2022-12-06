using System;
using System.Linq;

namespace Eco.Echolon.ApiClient.Filter.Builder
{
    public class OrBuilder : GroupBuilder
    {
        public override GroupBuilder Or(Action<OrBuilder> action)
        {
            return this;
        }

        public override IAmEvaluateAble Build()
        {
            return new OrFilter(Filters.Select(x => x.Build()));
        }
    }
}