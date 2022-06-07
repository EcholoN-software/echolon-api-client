using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eco.Echolon.ApiClient.Model;

namespace Eco.Echolon.ApiClient.Query
{
    public class QueryProvider
    {
        private readonly QueryConfigurator _configurator;
        private Dictionary<Type, int> TypeDepth = new Dictionary<Type, int>();
        private StringBuilder Builder = new StringBuilder();

        public QueryProvider(QueryConfigurator configurator)
        {
            _configurator = configurator;
        }

        public string GetMutationQuery<T>(string[] endpoint, WorkingEnqueueInput<T> input)
        {
            return GetGraphQL(RequestType.Mutation, endpoint,
                new Dictionary<string, object>() { { "input", input } },
                typeof(MutationOutput));
        }

        public string GetViewQuery<T>(string endpoint, IDictionary<string, object>? input)
        {
            return GetViewQuery(endpoint, input, typeof(T));
        }

        public string GetViewQuery(string endpoint, IDictionary<string, object>? input, Type returnType)
        {
            return GetGraphQlQuery(new[] { "views", endpoint }, input, returnType);
        }

        public string GetGraphQlQuery(string[] endpointPath, IDictionary<string, object>? input, Type returnType)
        {
            return GetGraphQL(RequestType.Query, endpointPath, input, returnType);
        }

        private string GetGraphQL(RequestType type, string[] endpointPath, IDictionary<string, object>? input,
            Type returnType)
        {
            Builder.AppendLine($"{type.ToQueryKeyWord()} {{");

            for (int i = 0; i < endpointPath.Length - 1; i++)
            {
                Builder.AppendLine($"{endpointPath[i]} {{");
            }

            Builder.AppendLine($"{endpointPath[endpointPath.Length - 1]}");
            AppendInputString(input);
            Builder.AppendLine(" {");
            GetFieldsFromType(returnType);

            foreach (var path in endpointPath)
            {
                Builder.AppendLine("}");
            }

            Builder.AppendLine("}");
            var r = Builder.ToString();
            Builder.Clear();
            return r;
        }

        private void AppendInputString(IDictionary<string, object>? input)
        {
            if (input == null || !input.Any())
                return;
            Builder.Append("(");

            foreach (var kv in input)
                Builder.Append($"{kv.Key}: {GraphQLConvert.Serialize(kv.Value)}, ");

            Builder.Remove(Builder.Length - 2, 2);
            Builder.Append(")");
        }

        private void GetFieldsFromType(Type type)
        {
            if (type.IsArray)
                type = type.GetElementType()!;

            foreach (var property in type.GetProperties())
            {
                var cAtt = property.CustomAttributes.ToArray();
                var propName = property.Name;

                if (cAtt.Length > 0)
                {
                    //TODO: CustomAttribute erstellen für Custom name
                }


                Builder.AppendLine(string.Concat(propName[0].ToString().ToLower(),
                    propName.Substring(1, propName.Length - 1)));

                if (!property.PropertyType.IsPrimitive &&
                    property.PropertyType != typeof(string) &&
                    property.PropertyType != typeof(object) &&
                    !_configurator.IsSingleValueType(property.PropertyType))
                {
                    if (TypeDepth.TryGetValue(property.PropertyType, out var depth) &&
                        _configurator.MaxRecursiveDepth < depth)
                    {
                        continue;
                    }

                    TypeDepth[property.PropertyType] = depth + 1;
                    Builder.AppendLine("{");
                    GetFieldsFromType(property.PropertyType);
                    Builder.AppendLine("}");
                    TypeDepth[property.PropertyType]--;
                }
            }
        }
    }
}