using System;

namespace Eco.Echolon.ApiClient.Authentication
{
    public static class AccessTokenManagementDefaults
    {
        /// <summary>
        ///     Name of the back-channel HTTP client.
        /// </summary>
        public const string BackChannelHttpClientName = "AccessTokenManagement.BackChannelHttpClient";

        /// <summary>
        ///     Name used to propagate access token parameters to HttpRequestMessage.
        /// </summary>
        public const string AccessTokenParametersOptionsName = "AccessTokenManagement.AccessTokenParameters";

        /// <summary>
        ///     Name used to prefix cache keys.
        /// </summary>
        public const string CacheKeyPrefix = "AccessTokenManagement.AccessTokens:";

        /// <summary>
        ///     Default value to subtract from token lifetime for the cache entry lifetime.
        /// </summary>
        public static readonly TimeSpan CacheLifetimeBuffer = TimeSpan.FromMinutes(1);
    }
}