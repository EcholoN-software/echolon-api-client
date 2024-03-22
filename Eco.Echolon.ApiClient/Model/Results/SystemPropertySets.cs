using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model.Results
{
    public class SystemPropertySets
    {
        public string Id { get; }
        public string Key { get; }
        public string Name { get; }
        public bool IsActive { get; }

        public SystemPropertySets(string id, string key, string name, bool isActive)
        {
            Id = id;
            Key = key;
            Name = name;
            IsActive = isActive;
        }
    }
}