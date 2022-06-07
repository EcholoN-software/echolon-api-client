using System;
using Eco.Echolon.ApiClient.Log;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Eco.Echolon.ApiClient.Authentication
{
    /// <summary>
    ///     Client access token cache using IMemoryCache.
    /// </summary>
    public class ClientAccessTokenCache : IClientAccessTokenCache
    {
        private static readonly ILog Log = LogService.Instance.GetLogger(typeof(ClientAccessTokenCache));

        private readonly IMemoryCache _cache;
        private readonly AccessTokenManagementOptions _options;

        /// <summary>
        ///     Creates an new Instance.
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="options"></param>
        public ClientAccessTokenCache(IMemoryCache cache,
            IOptions<AccessTokenManagementOptions> options)
        {
            _cache = cache;
            _options = options.Value;
        }

        /// <summary>
        ///     <see cref="IClientAccessTokenCache.GetToken" />
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ClientAccessToken GetToken(string clientName, ClientAccessTokenParameters parameters)
        {
            if (clientName is null) throw new ArgumentNullException(nameof(clientName));

            var cacheKey = GenerateCacheKey(_options, clientName);
            if (_cache.TryGetValue<string>(cacheKey, out var entry))
            {
                try
                {
                    Log.Debug($"Cache hit for access token for client: {clientName}");

                    var values = entry.Split(new[] { "___" }, StringSplitOptions.RemoveEmptyEntries);

                    return new ClientAccessToken
                    {
                        AccessToken = values[0],
                        Expiration = DateTimeOffset.FromUnixTimeSeconds(long.Parse(values[1]))
                    };
                }
                catch (Exception ex)
                {
                    Log.Error($"Error parsing cached access token for client {clientName}", ex);
                    return null;
                }
            }

            Log.Debug($"Cache miss for access token for client: {clientName}");
            return null;
        }

        /// <summary>
        ///     <see cref="IClientAccessTokenCache.SetToken" />
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="accessToken"></param>
        /// <param name="expiresIn"></param>
        /// <param name="parameters"></param>
        public void SetToken(string clientName, string accessToken, int expiresIn,
            ClientAccessTokenParameters parameters)
        {
            if (clientName is null) throw new ArgumentNullException(nameof(clientName));

            var expiration = DateTimeOffset.UtcNow.AddSeconds(expiresIn);
            var expirationEpoch = expiration.ToUnixTimeSeconds();
            var cacheExpiration = expiration.Subtract(_options.CacheLifetimeBuffer);

            var data = $"{accessToken}___{expirationEpoch}";

            Log.Debug($"Caching access token for client: {clientName}. Expiration: {expiration}");

            var cacheKey = GenerateCacheKey(_options, clientName);
            var entry = _cache.CreateEntry(cacheKey);
            entry.AbsoluteExpiration = cacheExpiration;
            entry.Value = data;
        }

        /// <summary>
        ///     <see cref="IClientAccessTokenCache.DeleteToken" />
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="parameters"></param>
        public void DeleteToken(string clientName, ClientAccessTokenParameters parameters)
        {
            if (clientName is null) throw new ArgumentNullException(nameof(clientName));

            var cacheKey = GenerateCacheKey(_options, clientName);
            _cache.Remove(cacheKey);
        }

        /// <summary>
        ///     Generates the cache key based on various inputs
        /// </summary>
        /// <param name="options"></param>
        /// <param name="clientName"></param>
        /// <returns></returns>
        protected virtual string GenerateCacheKey(AccessTokenManagementOptions options, string clientName)
        {
            return options.CacheKeyPrefix + clientName;
        }
    }
}