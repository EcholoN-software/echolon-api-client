using System;
using Eco.Echolon.ApiClient.Model.CommonModels;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Model.Modules.Privileges
{
    public class PrivilegeInput
    {
        public ItemId<Privilege> Id { get; set; }
        public ItemKey Key { get; set; }
        public string Name { get; set; }
        public FormattedTextId Notes { get; set; }
        public PrivilegeRelation[] Privileges { get; set; } = Array.Empty<PrivilegeRelation>();
        public AccountRelation[] Accounts { get; set; } = Array.Empty<AccountRelation>();

        public PrivilegeInput(ItemId<Privilege> id, ItemKey key)
        {
            Id = id;
            Key = key;
        }
    }
    

    public class Privilege
    {
        public ItemKey Key { get; set; }
        public ItemId<Privilege> Id { get; set; }

        public UserTimeStamp Created { get; set; }
        public UserTimeStamp Modified { get; set; }
        public bool IsActive { get; set; }


        public string Name { get; set; }
        public bool IsBuiltIn { get; set; }
        public FormattedTextId Notes { get; set; }
        public PrivilegeRelation[] Privileges { get; set; } = Array.Empty<PrivilegeRelation>();
        public AccountRelation[] Accounts { get; set; } = Array.Empty<AccountRelation>();

        public PrivilegeInput ToInput()
        {
            return new PrivilegeInput(Id, Key)
            {
                Accounts = Accounts,
                Name = Name,
                Notes = Notes,
                Privileges = Privileges
            };
        }
    }

    public class AccountRelation
    {
        public ItemId AccountId { get; set; }
        public string Name { get; set; }
    }

    public class PrivilegeRelation
    {
        public ItemId<Privilege> PrivilegeId { get; set; }
        public string Name { get; set; }
    }
}