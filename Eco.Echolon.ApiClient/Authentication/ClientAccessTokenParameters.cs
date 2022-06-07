namespace Eco.Echolon.ApiClient.Authentication
{
    /// <summary>
    ///     Additional optional parameters for a client access token request
    /// </summary>
    public class ClientAccessTokenParameters
    {
        /// <summary>
        ///     Force renewal of token.
        /// </summary>
        public bool ForceRenewal { get; set; }
    }
}