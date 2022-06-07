using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.Results;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public class SystemClient : ISystemClient
    {
        private readonly IBaseClient _baseClient;

        public SystemClient(IBaseClient baseClient)
        {
            _baseClient = baseClient;
        }
        public async Task<GraphQlResponse<SystemDataSources[]?>> SystemDataSources(string[] ids)
        {
            return await _baseClient.QueryCustom<SystemDataSources[]>(new[] { "system", "dataSources" },
                new Dictionary<string, object>() { { "id", ids } });
        }

        public async Task<GraphQlResponse<SystemViews[]?>> SystemViews(SystemViewInput? input)
        {
            var dicInput = new Dictionary<string, object>();

            if (input != null)
            {
                if (input.Id != null && input.Id.Any())
                    dicInput["id"] = input.Id;
                if (input.HasMetadataKeys != null && input.HasMetadataKeys.Any())
                    dicInput["has_metadataKeys"] = input.HasMetadataKeys;
                if (input.ContainsEntityIds != null && input.ContainsEntityIds.Any())
                    dicInput["contains_entityIds"] = input.ContainsEntityIds;
            }

            return await _baseClient.QueryCustom<SystemViews[]>(new[] { "system", "views" }, dicInput);
        }

        public async Task<GraphQlResponse<SystemPrivileges[]?>> SystemPrivileges()
        {
            return await _baseClient.QueryCustom<SystemPrivileges[]>(new[] { "system", "privileges" });
        }

        public async Task<GraphQlResponse<SystemPropertySets[]?>> SystemProperties()
        {
            return await _baseClient.QueryCustom<SystemPropertySets[]>(new[] { "system", "propertySets" });
        }

        public async Task<GraphQlResponse<SystemIndividuals[]?>> SystemIndividuals(string[]? subjects)
        {
            return await _baseClient.QueryCustom<SystemIndividuals[]>(new[] { "system", "individuals" });
        }

    }
}