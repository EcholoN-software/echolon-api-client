using System.Collections.Generic;
using System.Text;

namespace Eco.Echolon.ApiClient.Query
{
    public class InterfaceGraphQLField : GraphQLField
    {
        public Dictionary<string, GraphQLField[]> Variants { get; } = new Dictionary<string, GraphQLField[]>();

        public InterfaceGraphQLField(string name,
            GraphQLField[]? baseChildren = null,
            IDictionary<string, object?>? arguments = null)
            : base(name, baseChildren, arguments)
        {
        }

        public void AddVariant(string name, GraphQLField[] conditionalChildren)
        {
            Variants.Add(name, conditionalChildren);
        }

        public override StringBuilder ToString(StringBuilder builder)
        {
            return ToString(builder,
                x =>
                {
                    foreach (var kv in Variants)
                    {
                        x.Append($"... on {kv.Key} {{");
                        foreach (var graphQLField in kv.Value)
                        {
                            graphQLField.ToString(x);
                        }

                        x.Append("}");
                    }
                });
        }
    }
}