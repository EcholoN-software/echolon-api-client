using System;
using System.Collections.Generic;
using System.Linq;

namespace Eco.Echolon.ApiClient.Query
{
    public class QueryBuilder
    {
        public string Name { get; }
        private List<QueryBuilder> _subFields;
        private readonly QueryConfigurator _configurator;
        private readonly IDictionary<string, object?>? _args;

        private QueryBuilder(string name, QueryConfigurator? configurator = null,
            IDictionary<string, object?>? args = null)
        {
            Name = name;
            _args = args;
            _subFields = new List<QueryBuilder>();
            _configurator = configurator ?? new QueryConfigurator();
        }


        public QueryBuilder AddField(string name)
        {
            _subFields.Add(new QueryBuilder(name, _configurator));
            return this;
        }

        public QueryBuilder AddField(string name, Action<QueryBuilder> subQuery, IDictionary<string, object?>? args = null)
        {
            var field = new QueryBuilder(name, _configurator, args);
            _subFields.Add(field);
            subQuery(field);
            return this;
        }
        

        public QueryBuilder AddField<T>(string name)
        {
            return AddField(name, typeof(T));
        }

        public QueryBuilder AddField(string name,
            Type type,
            IDictionary<string, object>? args = null)
        {
            return AddFieldInternal(name, type, args, null);
        }
        
        private QueryBuilder AddFieldInternal(string name,
            Type type,
            IDictionary<string, object>? args,
            IDictionary<Type, int>? typeDepth)
        {
            typeDepth ??= new Dictionary<Type, int>();
            var newField = new QueryBuilder(name, _configurator, args);
            _subFields.Add(newField);
            if (type.IsArray)
                type = type.GetElementType()!;

            foreach (var property in type.GetProperties())
            {
                var cAtt = property.CustomAttributes.ToArray();
                var propName = Uncapitalize(property.Name);

                if (cAtt.Length > 0)
                {
                    //TODO: CustomAttribute erstellen für Custom name
                }

                var propType = property.PropertyType;
                if (propType.IsArray)
                    propType = propType.GetElementType()!;

                if (!propType.IsPrimitive &&
                    propType != typeof(string) &&
                    propType != typeof(object) &&
                    !_configurator.IsSingleValueType(propType))
                {
                    if (propType.GetInterfaces().FirstOrDefault(x => x.Name == "IDictionary") != null)
                    {
                        newField.AddField(propName, x => x.AddField("key").AddField("value"));
                        continue;
                    }
                    if (typeDepth.TryGetValue(propType, out var depth) &&
                        _configurator.MaxRecursiveDepth < depth)
                    {
                        continue;
                    }

                    typeDepth[propType] = depth + 1;
                    newField.AddFieldInternal(propName, propType, null, typeDepth);
                    typeDepth[propType]--;
                }
                else newField.AddField(propName);
            }

            return this;
        }

        private string Uncapitalize(string str)
        {
            return string.Concat(str[0].ToString().ToLower(),
                str.Substring(1, str.Length - 1));
        }

        public override string ToString()
        {
            return Build().ToString();
        }

        public GraphQLField Build()
        {
            return _subFields.Count == 0
                ? new GraphQLField(Name, null, _args)
                : new GraphQLField(Name, _subFields.Select(x => x.Build()).ToArray(), _args);
        }

        public static QueryBuilder Query(QueryConfigurator? config = null)
        {
            return new QueryBuilder("query", config);
        }

        public static QueryBuilder Mutation(QueryConfigurator? config = null)
        {
            return new QueryBuilder("mutation", config);
        }
    }
}