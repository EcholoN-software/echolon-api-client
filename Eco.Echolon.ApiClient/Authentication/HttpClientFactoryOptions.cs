using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace Eco.Echolon.ApiClient.Authentication
{
    public class HttpClientFactoryOptions
    {
        public ConcurrentDictionary<string, Func<IServiceProvider, DelegatingHandler>> HandlerFactories { get; }
            = new();

        public ConcurrentDictionary<string, Func<IServiceProvider, HttpClient>> ClientFactories { get; }
            = new();

        public void RegisterClient(string name, Func<IServiceProvider, HttpClient> factory)
        {
            if (!ClientFactories.TryAdd(name, factory))
            {
                var message = $"Client '{name}' is already registered!";
                throw new InvalidOperationException(message);
            }
        }

        public void RegisterClient(string name, Func<IServiceProvider, DelegatingHandler> factory)
        {
            if (!HandlerFactories.TryAdd(name, factory))
            {
                var message = $"Client '{name}' is already registered!";
                throw new InvalidOperationException(message);
            }
        }

        public void UnregisterClient(string name)
        {
            ClientFactories.TryRemove(name, out _);
            HandlerFactories.TryRemove(name, out _);
        }
    }
}