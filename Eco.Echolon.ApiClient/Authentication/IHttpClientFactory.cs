using System.Net.Http;

namespace Eco.Echolon.ApiClient.Authentication
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient(string name);
    }
}