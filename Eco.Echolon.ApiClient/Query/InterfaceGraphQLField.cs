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

        public override StringBuilder ToString(StringBuilder builder, int depth = 0)
        {
            return ToString(builder,
                (x, scopeDepth) =>
                {
                    foreach (var kv in Variants)
                    {
                        x.Append($"... on {kv.Key} {{");
                        AddNewLineWithSpacing(builder, scopeDepth + 2);
                        foreach (var graphQLField in kv.Value)
                        {
                            graphQLField.ToString(x,scopeDepth + 2);
                        }
                        
                        builder.Remove(builder.Length - 2, 2);
                        x.AppendLine("}");
                        Spacing(builder, scopeDepth +1);
                    }
                }, depth);
        }
    }
}