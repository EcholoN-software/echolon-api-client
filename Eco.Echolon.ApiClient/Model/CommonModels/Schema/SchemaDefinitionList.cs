using System.Collections.Generic;
using System.Linq;

namespace Eco.Echolon.ApiClient.Model.CommonModels.Schema
{
    public class SchemaDefinitionList
    {
        public TypeDefinition[] Types { get; }
        public AssignmentDefinition[] Assignments { get; }

        public SchemaDefinitionList(TypeDefinition[] types, AssignmentDefinition[] assignments)
        {
            Types = types;
            Assignments = assignments;
        }

        public SchemaDefinition? ToSchemaDefinition()
        {
            if (Types.Length == 0)
                return null;

            FieldSchemaDefinition RecursiveCall(int i)
            {
                var schema = Types[i];
                return new FieldSchemaDefinition(schema.PropertyDefinitions.Select(x =>
                        new FieldDefinition(x.Name,
                            x.Type,
                            x.Metadata,
                            x.Validators,
                            x.SchemaIndexes.Select(RecursiveCall).ToArray())).ToArray(),
                    schema.Metadata);
            }

            var fs = RecursiveCall(0);
            return new SchemaDefinition(fs.Fields, Assignments, fs.Metadata);
        }
    }

    public class TypeDefinition
    {
        public PropertyPair[] PropertyDefinitions { get; }
        public Dictionary<string, object?> Metadata { get; }

        public TypeDefinition(PropertyPair[] propertyDefinitions, Dictionary<string, object?> metadata)
        {
            this.PropertyDefinitions = propertyDefinitions;
            this.Metadata = metadata;
        }
    }

    public class PropertyPair
    {
        public string Name { get; }
        public string Type { get; }
        public FieldValidatorDefinition[] Validators { get; }
        public int[] SchemaIndexes { get; }
        public Dictionary<string, object?> Metadata { get; }

        public PropertyPair(string name,
            string type,
            FieldValidatorDefinition[] validators,
            int[] schemaIndexes,
            Dictionary<string, object?> metadata)
        {
            Name = name;
            Type = type;
            Validators = validators;
            SchemaIndexes = schemaIndexes;
            Metadata = metadata;
        }
    }
}