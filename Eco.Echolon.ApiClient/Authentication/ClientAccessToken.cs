using System;

namespace Eco.Echolon.ApiClient.Authentication
{
    /// <summary>
    ///     Represents a client access token
    /// </summary>
    public class ClientAccessToken
    {
        /// <summary>
        ///     The access token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        ///     The access token expiration
        /// </summary>
        public DateTimeOffset Expiration { get; set; }
    }
}