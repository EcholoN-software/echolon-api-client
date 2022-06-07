namespace Eco.Echolon.ApiClient.Model.Results
{
    #nullable disable
    public class EntityDefinitionResult
    {
        public string Id { get; }
        public string Key { get; }
        public string Name { get; }
        public string Number { get; }
        public string Notes { get; }
        public SchemaDefinitionResult Schema { get; }
        public bool IsActive { get; }
    }

    public class SchemaDefinitionResult
    {
        public FieldDefinitionResult[] Fields { get; }
        public AssignmentDefinitionResult[] Assignments { get; }
        public Metadata[] Metadata { get; }
    }

    public class Metadata
    {
        public string Key { get; }
        public object Value { get; }
    }

    public class AssignmentDefinitionResult
    {
        public string Target { get; }
        public object Value { get; }
    }

    public class FieldDefinitionResult
    {
        public string Key { get; }
        public string Type { get; }
        public Metadata[] Metadata { get; }
        public FieldValidatorDefinitionResult[] Validators { get; }
        public FieldSchemaDefinitionResult[] Schemas { get; }
    }

    public class FieldSchemaDefinitionResult
    {
        public FieldDefinitionResult[] Fields { get; }
        public Metadata[] Metadata { get; }
    }

    public class FieldValidatorDefinitionResult
    {
        public string Key { get; }
        public Metadata[] Settings { get; }
    }
    #nullable restore
}