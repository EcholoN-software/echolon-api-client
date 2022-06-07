using System.Threading;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Log;
using IdentityModel.Client;

namespace Eco.Echolon.ApiClient.Authentication
{
    /// <summary>
    ///     Implements token endpoint operations using IdentityModel
    /// </summary>
    public class TokenEndpointService : ITokenEndpointService
    {
        private static readonly ILog Log = LogService.Instance.GetLogger(typeof(TokenEndpointService));
        private readonly ITokenClientConfigurationService _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        ///     Creates an new Instance.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="httpClientFactory"></param>
        public TokenEndpointService(ITokenClientConfigurationService configuration,
            IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        ///     <see cref="ITokenEndpointService.RequestClientAccessToken" />
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TokenResponse> RequestClientAccessToken(string clientName,
            ClientAccessTokenParameters parameters = default,
            CancellationToken cancellationToken = default)
        {
            Log.Debug($"Requesting client access token for client: {clientName}");

            parameters ??= new ClientAccessTokenParameters();
            var tokenRequest = await _configuration.GetClientCredentialsRequestAsync(clientName, parameters);
            var httpClient = _httpClientFactory.CreateClient(AccessTokenManagementDefaults.BackChannelHttpClientName);
            return await httpClient.RequestClientCredentialsTokenAsync(tokenRequest, cancellationToken);
        }
    }
}