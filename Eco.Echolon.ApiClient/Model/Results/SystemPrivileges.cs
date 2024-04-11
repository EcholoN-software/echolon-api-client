using Eco.Echolon.ApiClient.Model.CommonModels;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model.Results
{
    public class SystemPrivileges
    {
        public string? Name { get; }
        public ItemId? Notes { get; }
        public bool HasRelatedPrivileged { get; }
        public bool HasRelatedAccounts { get; }
        public string Key { get; }
        public UserTimeStamp? Created { get; }
        public UserTimeStamp? Modified { get; }
        public bool IsActive { get; }
        public ItemId<SystemPrivileges> Id { get; }

        public SystemPrivileges(string? name,
            ItemId? notes, bool hasRelatedPrivileged, bool hasRelatedAccounts,
            string key, UserTimeStamp? created, UserTimeStamp? modified, bool isActive, ItemId<SystemPrivileges> id)
        {
            Name = name;
            Notes = notes;
            HasRelatedPrivileged = hasRelatedPrivileged;
            HasRelatedAccounts = hasRelatedAccounts;
            Key = key;
            Created = created;
            Modified = modified;
            IsActive = isActive;
            Id = id;
        }
    }
}