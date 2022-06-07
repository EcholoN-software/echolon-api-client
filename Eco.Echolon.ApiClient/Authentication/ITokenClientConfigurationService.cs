using System.Threading.Tasks;
using IdentityModel.Client;

namespace Eco.Echolon.ApiClient.Authentication
{
    public interface ITokenClientConfigurationService
    {
        /// <summary>
        ///     Returns the request details for a client credentials token request.
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<ClientCredentialsTokenRequest> GetClientCredentialsRequestAsync(string clientName,
            ClientAccessTokenParameters parameters);
    }
}