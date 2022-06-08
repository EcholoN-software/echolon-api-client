// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using Microsoft.Extensions.DependencyInjection;

namespace Eco.Echolon.ApiClient.Authentication
{
    public class TokenManagementBuilder
    {
        /// <summary>
        ///     Creates an new Instance.
        /// </summary>
        /// <param name="services"></param>
        public TokenManagementBuilder(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        ///     The underlying service collection.
        /// </summary>
        public IServiceCollection Services { get; }
    }
}