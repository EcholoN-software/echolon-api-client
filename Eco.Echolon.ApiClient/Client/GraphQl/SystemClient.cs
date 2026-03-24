using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public async Task<GraphQlResponse<SystemDataSources[]>> SystemDataSources(string[] ids,
            CancellationToken cancellationToken = default)
        {
            return await _baseClient.Query<SystemDataSources[]>(new[] { "system", "dataSources" },
                new Dictionary<string, object?>() { { "id", ids } }, cancellationToken);
        }

        public async Task<GraphQlResponse<SystemViews[]>> SystemViews(SystemViewInput? input,
            CancellationToken cancellationToken = default)
        {
            var dicInput = new Dictionary<string, object?>();

            if (input != null)
            {
                if (input.Id != null && input.Id.Any())
                    dicInput["id"] = input.Id;
                if (input.HasMetadataKeys != null && input.HasMetadataKeys.Any())
                    dicInput["has_metadataKeys"] = input.HasMetadataKeys;
                if (input.ContainsEntityIds != null && input.ContainsEntityIds.Any())
                    dicInput["contains_entityIds"] = input.ContainsEntityIds;
            }

            return await _baseClient.Query<SystemViews[]>(new[] { "system", "views" }, dicInput, cancellationToken);
        }

        public async Task<GraphQlResponse<SystemPrivileges[]>> SystemPrivileges(
            CancellationToken cancellationToken = default)
        {
            return await _baseClient.Query<SystemPrivileges[]>(new[] { "system", "privileges" },
                cancellationToken: cancellationToken);
        }

        public async Task<GraphQlResponse<SystemPropertySets[]>> SystemProperties(
            CancellationToken cancellationToken = default)
        {
            return await _baseClient.Query<SystemPropertySets[]>(new[] { "system", "propertySets" },
                cancellationToken: cancellationToken);
        }

        public async Task<GraphQlResponse<SystemIndividuals[]>> SystemIndividuals(string[]? subjects,
            CancellationToken cancellationToken = default)
        {
            return await _baseClient.Query<SystemIndividuals[]>(new[] { "system", "individuals" },
                cancellationToken: cancellationToken);
        }
    }
}
