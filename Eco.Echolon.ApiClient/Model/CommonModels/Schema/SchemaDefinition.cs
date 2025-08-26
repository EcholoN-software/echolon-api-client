using System.Collections.Generic;

namespace Eco.Echolon.ApiClient.Model.CommonModels.Schema
{
    public class SchemaDefinition
    {
        public SchemaDefinition(FieldDefinition[] fields,
            AssignmentDefinition[] assignments,
            IDictionary<string, object?> metadata)
        {
            Fields = fields;
            Assignments = assignments;
            Metadata = metadata;
        }

        public FieldDefinition[] Fields { get; set; }
        public AssignmentDefinition[] Assignments { get; set; }
        public IDictionary<string, object?> Metadata { get; set; }
    }

    public class FieldDefinition
    {
        public FieldDefinition(string key,
            string type,
            Dictionary<string, object?> metadata,
            FieldValidatorDefinition[] validators,
            FieldSchemaDefinition[] schemas)
        {
            Key = key;
            Type = type;
            Metadata = metadata;
            Validators = validators;
            Schemas = schemas;
        }

        public string Key { get; set; }
        public string Type { get; set; }
        public Dictionary<string, object?> Metadata { get; set; }
        public FieldValidatorDefinition[] Validators { get; set; }
        public FieldSchemaDefinition[] Schemas { get; set; }
    }

    public class FieldValidatorDefinition
    {
        public FieldValidatorDefinition(string key, Dictionary<string, object?> settings)
        {
            Key = key;
            Settings = settings;
        }

        public string Key { get; set; }
        public Dictionary<string, object?> Settings { get; set; }
    }

    public class FieldSchemaDefinition
    {
        public FieldSchemaDefinition(FieldDefinition[] fields, Dictionary<string, object?> metadata)
        {
            Fields = fields;
            Metadata = metadata;
        }

        public FieldDefinition[] Fields { get; set; }
        public IDictionary<string, object?> Metadata { get; set; }
    }

    public sealed class AssignmentDefinition
    {
        public AssignmentDefinition(string target, object? value)
        {
            Target = target;
            Value = value;
        }

        public string Target { get; set; }
        public object? Value { get; set; }
    }
}