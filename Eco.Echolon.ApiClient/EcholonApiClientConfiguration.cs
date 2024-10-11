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
        public bool UseAccessTokenManagement { get; }

        public EcholonApiClientConfiguration(Uri apiUri, Uri identityHubUri, string clientId, string secret,
            bool acceptAnyCertificate = false, bool useAccessTokenManagement = true)
        {
            UseAccessTokenManagement = useAccessTokenManagement;
            ApiUri = apiUri;
            IdentityHubUri = identityHubUri;
            IdentityHubClientId = clientId;
            IdentityHubClientSecret = secret;
            AcceptAnyCertificate = acceptAnyCertificate;
        }
    }
}