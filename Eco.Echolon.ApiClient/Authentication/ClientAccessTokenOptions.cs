using System;

namespace Eco.Echolon.ApiClient.Authentication
{
    public class ClientAccessTokenOptions
    {
        /// <summary>
        ///     Creates an new Instance.
        /// </summary>
        /// <param name="authority"></param>
        public ClientAccessTokenOptions(string authority)
        {
            Authority = authority;
        }

        /// <summary>
        ///     Options for authority url.
        /// </summary>
        public string Authority { get; }

        /// <summary>
        ///     Gets or sets the client identifier.
        /// </summary>
        /// <value>
        ///     The client identifier.
        /// </value>
        public string? ClientId { get; set; }

        /// <summary>
        ///     Gets or sets the client secret.
        /// </summary>
        /// <value>
        ///     The client secret.
        /// </value>
        public string? ClientSecret { get; set; }

        /// <summary>
        ///     List of the requested scopes.
        /// </summary>
        /// <value>
        ///     The scopes.
        /// </value>
        public string[] Scopes { get; set; }
            = Array.Empty<string>();
    }
}