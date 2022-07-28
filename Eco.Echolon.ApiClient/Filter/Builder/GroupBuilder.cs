using System.Collections.Generic;
using Eco.Echolon.ApiClient.Filter.Values;

namespace Eco.Echolon.ApiClient.Filter.Builder
{
    public abstract class GroupBuilder<TGroupBuilder> : ICreateFilter where TGroupBuilder : ICreateFilter
    {
        protected readonly List<ICreateFilter> Filters;

        protected GroupBuilder()
        {
            Filters = new List<ICreateFilter>();
        }

        public abstract IAmEvaluateAble Build();
    }
}