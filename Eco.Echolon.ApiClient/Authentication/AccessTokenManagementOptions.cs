using System;
using System.Collections.Generic;

namespace Eco.Echolon.ApiClient.Authentication
{
    public class AccessTokenManagementOptions
    {
        /// <summary>
        ///     Used to prefix the cache key.
        /// </summary>
        public string CacheKeyPrefix { get; set; }
            = AccessTokenManagementDefaults.CacheKeyPrefix;

        /// <summary>
        ///     Value to subtract from token lifetime for the cache entry lifetime (defaults to 60 seconds).
        /// </summary>
        public TimeSpan CacheLifetimeBuffer { get; set; }
            = AccessTokenManagementDefaults.CacheLifetimeBuffer;

        /// <summary>
        ///     Configures named client configurations for requesting client tokens.
        /// </summary>
        public IDictionary<string, ClientAccessTokenOptions> Clients { get; set; }
            = new Dictionary<string, ClientAccessTokenOptions>();
    }
}