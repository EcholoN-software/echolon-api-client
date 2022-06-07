using System;

namespace Eco.Echolon.ApiClient.Model.Results
{
    public class FormattedTextTemplateAdminResult
    {
        public string Id { get; }
        public string Key { get; }
        public string CreatedBy { get; }
        public DateTimeOffset CreatedOn { get; }
        public string ModifiedBy { get; }
        public DateTimeOffset ModifiedOn { get; }
        public string Name { get; set; }
        public string FormattedTextId { get; }
        public string Language { get; }
        public bool IsActive { get; }

        public FormattedTextTemplateAdminResult(string id, string key, string createdBy, DateTimeOffset createdOn,
            string modifiedBy, DateTimeOffset modifiedOn, string name, string formattedTextId, string language,
            bool isActive)
        {
            Id = id;
            Key = key;
            CreatedBy = createdBy;
            CreatedOn = createdOn;
            ModifiedBy = modifiedBy;
            ModifiedOn = modifiedOn;
            Name = name;
            FormattedTextId = formattedTextId;
            Language = language;
            IsActive = isActive;
        }
    }
}