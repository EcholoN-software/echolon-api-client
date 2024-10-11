using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eco.Echolon.ApiClient.Query
{
    public class GraphQLField
    {
        public GraphQLField(string name, GraphQLField[]? children = null, IDictionary<string, object?>? arguments = null)
        {
            Name = name;
            Arguments = arguments;
            Children = children;
        }

        public string Name { get; }
        public IDictionary<string, object?>? Arguments { get; }
        public GraphQLField[]? Children { get; }

        public override string ToString()
        {
            return ToString(new StringBuilder()).ToString();
        }

        public StringBuilder ToString(StringBuilder stringBuilder)
        {
            stringBuilder.Append(Name);
            AppendInputString(stringBuilder, Arguments);

            if (Children is not null)
            {
                stringBuilder.Append("{");
                foreach (var child in Children)
                {
                    child.ToString(stringBuilder);
                    if (child.Children is null)
                        stringBuilder.Append(" ");
                }

                stringBuilder.Append("}");
            }

            return stringBuilder;
        }

        private void AppendInputString(StringBuilder stringBuilder, IDictionary<string, object>? input)
        {
            if (input == null || !input.Any())
                return;
            stringBuilder.Append("(");

            foreach (var kv in input)
                stringBuilder.Append($"{kv.Key}: {GraphQLConvert.Serialize(kv.Value)}, ");

            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            stringBuilder.Append(")");
        }

        public static implicit operator GraphQLField(string s)
        {
            return new GraphQLField(s, null);
        }
    }
}