using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public interface IConfigClient
    {
        Task<GraphQlResponse<string?>> Get(string section, string module, string key);
        // Task<GraphQlResponse<object>> ConfigurationSet(string section, string module, string key, object value);
    }
}