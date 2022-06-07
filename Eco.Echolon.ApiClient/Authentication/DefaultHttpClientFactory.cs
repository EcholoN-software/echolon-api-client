using System;
using System.Collections.Concurrent;
using System.Net.Http;
using Microsoft.Extensions.Options;

namespace Eco.Echolon.ApiClient.Authentication
{
    public class DefaultHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClientFactoryOptions _factoryOptions;
        private readonly ConcurrentDictionary<string, HttpClient> _httpClients;
        private readonly IServiceProvider _serviceProvider;

        public DefaultHttpClientFactory(IServiceProvider serviceProvider,
            IOptions<HttpClientFactoryOptions> factoryOptions)
        {
            _factoryOptions = factoryOptions.Value;
            _serviceProvider = serviceProvider;

            _httpClients = new ConcurrentDictionary<string, HttpClient>();
        }

        public HttpClient CreateClient(string name)
        {
            return _httpClients.GetOrAdd(name, CreateClientInternal);
        }

        private HttpClient CreateClientInternal(string name)
        {
            if (_factoryOptions.ClientFactories.TryGetValue(name, out var httpClientFactory))
            {
                return httpClientFactory(_serviceProvider);
            }

            if (_factoryOptions.HandlerFactories.TryGetValue(name, out var delegatingHandlerFactory))
            {
                var delegatingHandler = delegatingHandlerFactory(_serviceProvider);
                return new HttpClient(delegatingHandler);
            }

            return new HttpClient();
        }
    }
}