using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Eco.Echolon.ApiClient.Authentication
{
    /// <summary>
    ///     Delegating handler that injects a client access token into an outgoing request
    /// </summary>
    public class ClientAccessTokenHandler : DelegatingHandler
    {
        private readonly IClientAccessTokenManagementService _accessTokenManagementService;
        private readonly string _clientName;

        /// <summary>
        ///     Creates an new Instance.
        /// </summary>
        /// <param name="accessTokenManagementService">The Access Token Management Service</param>
        /// <param name="clientName">The name of the token client configuration</param>
        public ClientAccessTokenHandler(IClientAccessTokenManagementService accessTokenManagementService,
            string clientName)
        {
            _accessTokenManagementService = accessTokenManagementService;
            _clientName = clientName;
        }

        /// <summary>
        ///     <see cref="DelegatingHandler.SendAsync" />
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            await SetTokenAsync(request, false, cancellationToken);
            var response = await base.SendAsync(request, cancellationToken);

            // retry if 401
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                response.Dispose();

                await SetTokenAsync(request, true, cancellationToken);
                return await base.SendAsync(request, cancellationToken);
            }

            return response;
        }

        /// <summary>
        ///     Sets the Token.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="forceRenewal"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual async Task SetTokenAsync(HttpRequestMessage request, bool forceRenewal,
            CancellationToken cancellationToken)
        {
            var parameters = new ClientAccessTokenParameters
            {
                ForceRenewal = forceRenewal
            };

            var token = await _accessTokenManagementService.GetClientAccessTokenAsync(
                _clientName, parameters,
                cancellationToken);

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}