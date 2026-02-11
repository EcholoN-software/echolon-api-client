using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eco.Echolon.ApiClient.Query
{
    public class GraphQLField
    {
        public GraphQLField(string name,
            GraphQLField[]? baseChildren = null,
            IDictionary<string, object?>? arguments = null)
        {
            Name = name;
            Arguments = arguments;
            BaseChildren = baseChildren;
        }

        public string Name { get; }
        public IDictionary<string, object?>? Arguments { get; }
        public GraphQLField[]? BaseChildren { get; }

        public override string ToString()
        {
            return ToString(new StringBuilder()).ToString();
        }

        public virtual StringBuilder ToString(StringBuilder stringBuilder)
        {
            return ToString(stringBuilder, null);
        }

        protected StringBuilder ToString(StringBuilder stringBuilder, Action<StringBuilder>? inScopeAppendAction)
        {
            stringBuilder.Append(Name);
            AppendInputString(stringBuilder, Arguments);

            if (BaseChildren is not null)
            {
                stringBuilder.Append("{");
                foreach (var child in BaseChildren)
                {
                    child.ToString(stringBuilder);
                    if (child.BaseChildren is null)
                        stringBuilder.Append(" ");
                }

                if (inScopeAppendAction is not null)
                    inScopeAppendAction.Invoke(stringBuilder);

                stringBuilder.Append("}");
            }

            return stringBuilder;
        }

        private void AppendInputString(StringBuilder stringBuilder, IDictionary<string, object?>? input)
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