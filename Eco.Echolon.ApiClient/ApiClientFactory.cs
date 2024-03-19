using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Eco.Echolon.ApiClient.Authentication;
using Eco.Echolon.ApiClient.Client;
using Eco.Echolon.ApiClient.Client.GraphQl;
using Eco.Echolon.ApiClient.Client.RestApi;
using Eco.Echolon.ApiClient.Model.WorkingQueue;
using Eco.Echolon.ApiClient.Query;
using Microsoft.Extensions.DependencyInjection;

namespace Eco.Echolon.ApiClient
{
    public class ApiClientFactory : IDisposable
    {
        private ServiceProvider Provider { get; }
        private IServiceScope ProviderScope { get; }
        private IServiceCollection ServiceCollection { get; }

        public ApiClientFactory(EcholonApiClientConfiguration config, Action<IServiceCollection, QueryConfigurator>? confAction = null)
        {
            const string scopes = "Echolon.Api.WebApi";
            CheckConfiguration(config);

            ServiceCollection = new ServiceCollection();
            var queryConf = new QueryConfigurator(); 
            ServiceCollection.AddSingleton<EcholonApiClientConfiguration>(config);

            confAction?.Invoke(ServiceCollection, queryConf);

            ServiceCollection.AddAccessTokenManagement()
                .AddClientCredentials(Variables.HttpClientForApi,
                    config.IdentityHubUri.ToString(),
                    config.IdentityHubClientId,
                    config.IdentityHubClientSecret,
                    scopes);
            ServiceCollection.AddSingleton<ClientAccessTokenManagementService>();
            ServiceCollection.AddSingleton<QueryConfigurator>(queryConf);
            ServiceCollection.AddTransient<QueryProvider>();
            ServiceCollection.AddSingleton<IApiClient, EcoApiClient>();
            ServiceCollection.AddSingleton<IBaseClient, EcoBaseClient>();
            ServiceCollection.AddSingleton<IWorkingClient, WorkingClient>();
            ServiceCollection.AddSingleton<IEntitiesClient, EntitiesClient>();
            ServiceCollection.AddSingleton<ITextTemplatesClient, TextTemplatesClient>();
            ServiceCollection.AddSingleton<IProcessClient, ProcessClient>();
            ServiceCollection.AddSingleton<IConfigClient, ConfigClient>();
            ServiceCollection.AddSingleton<ISystemClient, SystemClient>();
            ServiceCollection.AddSingleton<IViewClient, ViewClient>();
            ServiceCollection.AddSingleton<IFileClient, FileClient>();
            ServiceCollection.AddSingleton<IBaseRestClient, EcoBaseRestClient>();
            ServiceCollection.AddSingleton<IFormattedTextClient, FormattedTextClient>();
            ServiceCollection.AddSingleton<IWorkingQueueClient, WorkingQueueClient>();
            
            if (config.AcceptAnyCertificate)
            {
                ServiceCollection.ReplaceHttpClient(AccessTokenManagementDefaults.BackChannelHttpClientName,
                    _ => new HttpClient(new UnsafeResilientHttpClientHandler()));
                ServiceCollection.ReplaceHttpClient(Variables.HttpClientForApi, provider =>
                {
                    var tokenService = provider.GetRequiredService<IClientAccessTokenManagementService>();
                    return new ClientAccessTokenHandler(tokenService, Variables.HttpClientForApi)
                    {
                        InnerHandler = new UnsafeResilientHttpClientHandler(),
                    };
                });
            }

            Provider = ServiceCollection.BuildServiceProvider();
            ProviderScope = Provider.CreateScope();
        }

        private void CheckConfiguration(EcholonApiClientConfiguration config)
        {
            if (string.IsNullOrEmpty(config.IdentityHubClientId) ||
                string.IsNullOrEmpty(config.IdentityHubClientSecret))
                throw new InvalidOperationException("The client configuration is faulty.");
        }

        public IApiClient Create()
        {
            return ProviderScope.ServiceProvider.GetService<IApiClient>();
        }

        private class UnsafeResilientHttpClientHandler : ResilientHttpClientHandler
        {
            public UnsafeResilientHttpClientHandler()
            {
                ServerCertificateCustomValidationCallback =
                    (Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>)
                    ((_param1, _param2, _param3, _param4) => true);
            }
        }

        public void Dispose()
        {
            ProviderScope.Dispose();
            Provider.Dispose();
        }
    }
}