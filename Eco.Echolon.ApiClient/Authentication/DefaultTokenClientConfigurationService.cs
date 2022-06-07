using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Log;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Eco.Echolon.ApiClient.Authentication
{
    /// <summary>
    ///     Options-based configuration service for token clients
    /// </summary>
    public class DefaultTokenClientConfigurationService : ITokenClientConfigurationService
    {
        private static readonly ILog Log = LogService.Instance
            .GetLogger(typeof(DefaultTokenClientConfigurationService));

        private readonly ConcurrentDictionary<string, DiscoveryCache> _discoveryCache;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AccessTokenManagementOptions _options;

        /// <summary>
        ///     Creates an new Instance.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="httpClientFactory"></param>
        public DefaultTokenClientConfigurationService(IOptions<AccessTokenManagementOptions> options,
            IHttpClientFactory httpClientFactory)
        {
            _options = options.Value;
            _httpClientFactory = httpClientFactory;

            _discoveryCache = new ConcurrentDictionary<string, DiscoveryCache>();
        }

        /// <summary>
        ///     <see cref="ITokenClientConfigurationService.GetClientCredentialsRequestAsync" />
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual async Task<ClientCredentialsTokenRequest> GetClientCredentialsRequestAsync(string clientName,
            ClientAccessTokenParameters parameters)
        {
            if (!TryGetClientOptions(clientName, out var options))
            {
                var error = $"No access token management configuration found for client: {clientName}";
                throw new ArgumentException(error, nameof(clientName));
            }

            var tokenEndpoint = await ResolveTokenEndpointUri(options.Authority);

            var tokenRequest = new ClientCredentialsTokenRequest
            {
                Address = tokenEndpoint,
                ClientId = options.ClientId,
                ClientSecret = options.ClientSecret
            };

            if (options.Scopes != null && options.Scopes.Any())
            {
                tokenRequest.Scope = string.Join(" ", options.Scopes);
            }

            tokenRequest.Properties[AccessTokenManagementDefaults.AccessTokenParametersOptionsName] = parameters;

            Log.Debug($"Constructed client credentials token request for client: {clientName}");

            return tokenRequest;
        }

        private bool TryGetClientOptions(string name, out ClientAccessTokenOptions options)
        {
            return _options.Clients.TryGetValue(name, out options);
        }

        private async Task<string> ResolveTokenEndpointUri(string authority)
        {
            var discoveryCache = _discoveryCache.GetOrAdd(authority, CreateDiscoveryCache);
            var discoveryDocument = await discoveryCache.GetAsync();
            if (discoveryDocument.Exception is not null)
            {
                Log.Error($"Failed to fetch discovery document from authority: {authority}");
                throw discoveryDocument.Exception;
            }

            return discoveryDocument.TokenEndpoint;
        }

        private DiscoveryCache CreateDiscoveryCache(string authority)
        {
            const StringComparison comparison = StringComparison.OrdinalIgnoreCase;
            var validation = new StringComparisonAuthorityValidationStrategy(comparison);

            var policy = new DiscoveryPolicy
            {
                AuthorityValidationStrategy = validation,
                RequireHttps = false
            };

            HttpClient CreateBackChannelHttpClient()
            {
                const string name = AccessTokenManagementDefaults.BackChannelHttpClientName;
                return _httpClientFactory.CreateClient(name);
            }

            return new DiscoveryCache(authority, CreateBackChannelHttpClient, policy);
        }
    }
}