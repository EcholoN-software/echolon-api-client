﻿using Eco.Echolon.ApiClient.Filter.Visitor;

namespace Eco.Echolon.ApiClient.Filter
{
    public interface IGraphQlFilter
    {
        T Accept<T>(IGraphQlFilterVisitor<T> visitor);
    }
}
