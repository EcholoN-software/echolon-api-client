using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Log;

namespace Eco.Echolon.ApiClient.Authentication
{
      /// <summary>
    ///     Implements basic token management logic
    /// </summary>
    public class ClientAccessTokenManagementService : IClientAccessTokenManagementService
    {
        private static readonly ConcurrentDictionary<string, Lazy<Task<string>>> ClientTokenRequestDictionary = new();
        private static readonly ILog Log = LogService.Instance.GetLogger(typeof(ClientAccessTokenManagementService));
        
        private readonly IClientAccessTokenCache _clientAccessTokenCache;
        private readonly ITokenEndpointService _tokenEndpointService;

        /// <summary>
        ///     Creates an new Instance.
        /// </summary>
        /// <param name="tokenEndpointService"></param>
        /// <param name="clientAccessTokenCache"></param>
        public ClientAccessTokenManagementService(
            ITokenEndpointService tokenEndpointService,
            IClientAccessTokenCache clientAccessTokenCache)
        {
            _tokenEndpointService = tokenEndpointService;
            _clientAccessTokenCache = clientAccessTokenCache;
        }

        /// <summary>
        ///     <see cref="IClientAccessTokenManagementService.GetClientAccessTokenAsync" />
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> GetClientAccessTokenAsync(string clientName,
            ClientAccessTokenParameters parameters = null,
            CancellationToken cancellationToken = default)
        {
            parameters ??= new ClientAccessTokenParameters();

            if (parameters.ForceRenewal == false)
            {
                var item = _clientAccessTokenCache.GetToken(clientName, parameters);
                if (item != null)
                {
                    return item.AccessToken;
                }
            }

            try
            {
                var task = ClientTokenRequestDictionary.GetOrAdd(clientName, _ =>
                {
                    return new Lazy<Task<string>>(async () =>
                    {
                        var response = await _tokenEndpointService.RequestClientAccessToken(clientName,
                            parameters, cancellationToken);

                        if (response.IsError)
                        {
                            var message = $"Error requesting access token for client {clientName}: {response.Error}";
                            if (!string.IsNullOrWhiteSpace(response.ErrorDescription))
                            {
                                message += Environment.NewLine;
                                message += response.ErrorDescription;
                            }

                            Log.Error(message, response.Exception);

                            return null;
                        }

                        Log.Debug($"Successfully requested access token for client {clientName}");

                        _clientAccessTokenCache.SetToken(clientName, 
                            response.AccessToken, 
                            response.ExpiresIn,
                            parameters);

                        return response.AccessToken;
                    });
                });

                return await task.Value;
            }
            finally
            {
                ClientTokenRequestDictionary.TryRemove(clientName, out _);
            }
        }

        /// <summary>
        ///     <see cref="IClientAccessTokenManagementService.DeleteClientAccessTokenAsync" />
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task DeleteClientAccessTokenAsync(string clientName,
            ClientAccessTokenParameters parameters = null,
            CancellationToken cancellationToken = default)
        {
            _clientAccessTokenCache.DeleteToken(clientName, parameters);
            return Task.CompletedTask;
        }
    }
}