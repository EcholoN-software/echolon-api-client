using System;

namespace Eco.Echolon.ApiClient
{
    public class EcholonApiClientConfiguration
    {
        public Uri ApiUri { get; }
        public Uri IdentityHubUri { get; }
        public string IdentityHubClientId { get; }
        public string IdentityHubClientSecret { get; }
        public bool AcceptAnyCertificate { get; }

        public EcholonApiClientConfiguration(Uri apiUri, Uri identityHubUri, string clientId, string secret,
            bool acceptAnyCertificate = false)
        {
            ApiUri = apiUri;
            IdentityHubUri = identityHubUri;
            IdentityHubClientId = clientId;
            IdentityHubClientSecret = secret;
            AcceptAnyCertificate = acceptAnyCertificate;
        }
    }
}