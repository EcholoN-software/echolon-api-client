using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public class ConfigClient : IConfigClient
    {
        private readonly IBaseClient _baseClient;

        public ConfigClient(IBaseClient baseClient)
        {
            _baseClient = baseClient;
        }

        public async Task<GraphQlResponse<string>> Get(string section, string module, string key)
        {
            if (section == null || module == null || key == null)
                throw new ArgumentException();
            var input = new Dictionary<string, object?>()
            {
                { "section", section }, { "module", module }, { "key", key }
            };
            return await _baseClient.Query<string>(new[] { "configurations", "get" }, input);
        }
    }
}