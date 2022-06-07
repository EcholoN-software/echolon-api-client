using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Eco.Echolon.ApiClient.Authentication
{
    public static class ServiceCollectionExtensions
    {
        public static TokenManagementBuilder AddAccessTokenManagement(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddHttpClientFactory();

            services.AddOptions<AccessTokenManagementOptions>();
            services.TryAddTransient<ITokenClientConfigurationService, DefaultTokenClientConfigurationService>();
            services.TryAddTransient<ITokenEndpointService, TokenEndpointService>();

            services.AddResilientHttpClient(AccessTokenManagementDefaults.BackChannelHttpClientName);

            return new TokenManagementBuilder(services);
        }
        
        public static IServiceCollection AddHttpClientFactory(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpClientFactory, DefaultHttpClientFactory>();
            return services;
        }

        public static IServiceCollection AddHttpClient(this IServiceCollection services,
            string name, Func<IServiceProvider, HttpClient> callback)
        {
            return services.Configure<HttpClientFactoryOptions>(factory => factory.RegisterClient(name, callback));
        }

        public static IServiceCollection ReplaceHttpClient(this IServiceCollection services,
            string name, Func<IServiceProvider, HttpClient> callback)
        {
            return services.PostConfigure<HttpClientFactoryOptions>(factory =>
            {
                factory.UnregisterClient(name);
                factory.RegisterClient(name, callback);
            });
        }

        public static IServiceCollection AddHttpClient(this IServiceCollection services,
            string name, Func<IServiceProvider, DelegatingHandler> callback)
        {
            return services.Configure<HttpClientFactoryOptions>(factory => factory.RegisterClient(name, callback));
        }

        public static IServiceCollection ReplaceHttpClient(this IServiceCollection services,
            string name, Func<IServiceProvider, DelegatingHandler> callback)
        {
            return services.PostConfigure<HttpClientFactoryOptions>(factory =>
            {
                factory.UnregisterClient(name);
                factory.RegisterClient(name, callback);
            });
        }

        public static IServiceCollection AddResilientHttpClient(this IServiceCollection services,
            string name)
        {
            return services.Configure<HttpClientFactoryOptions>(factory => factory.RegisterClient(name, _ =>
            {
                var resilientHandler = ResilientHttpClientHandler.Create();
                return new HttpClient(resilientHandler);
            }));
        }
        
        
        /// <summary>
        ///     Adds a named HTTP client to the factory that automatically manages the client access token.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <param name="authority"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="scopes"></param>
        /// <returns></returns>
        public static TokenManagementBuilder AddClientCredentials(this TokenManagementBuilder builder, string name,
            string authority, string clientId, string clientSecret,
            params string[] scopes)
        {
            builder.Services.TryAddTransient<IClientAccessTokenManagementService, ClientAccessTokenManagementService>();
            builder.Services.TryAddTransient<IClientAccessTokenCache, ClientAccessTokenCache>();

            builder.Services.Configure<AccessTokenManagementOptions>(options =>
            {
                options.Clients.Add(name, new ClientAccessTokenOptions(authority)
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    Scopes = scopes
                });
            });

            builder.Services.AddHttpClient(name, provider =>
            {
                var tokenService = provider.GetRequiredService<IClientAccessTokenManagementService>();
                return new ClientAccessTokenHandler(tokenService, name)
                {
                    InnerHandler = ResilientHttpClientHandler.Create()
                };
            });

            return builder;
        }
        
    }
}