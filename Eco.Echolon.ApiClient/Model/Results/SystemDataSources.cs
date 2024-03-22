using System.Collections.Generic;
using Eco.Echolon.ApiClient.Model.CommonModels;
using Eco.Echolon.ApiClient.Model.CommonModels.Schema;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model.Results
{
    public class SystemDataSources
    {
        public ItemId<SystemDataSources> Id { get; }
        public Dictionary<string, object?> Metadata { get; }
        public string Name { get; }
        public SchemaDefinitionList? Schema { get; }
        public string[] Features { get; }

        public SystemDataSources(ItemId<SystemDataSources> id, Dictionary<string, object?> metadata, string name, SchemaDefinitionList? schema, string[] features)
        {
            Id = id;
            Metadata = metadata;
            Name = name;
            Schema = schema;
            Features = features;
        }
    }
}