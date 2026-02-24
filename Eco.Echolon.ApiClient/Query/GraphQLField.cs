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

        public virtual StringBuilder ToString(StringBuilder stringBuilder, int depth = 0)
        {
            return ToString(stringBuilder, null, depth);
        }

        protected StringBuilder ToString(StringBuilder stringBuilder,
            Action<StringBuilder, int>? inScopeAppendAction,
            int depth)
        {
            stringBuilder.Append(Name);
            AppendInputString(stringBuilder, Arguments);

            if (BaseChildren is not null)
            {
                stringBuilder.Append(" {");
                AddNewLineWithSpacing(stringBuilder, depth + 1);
                for (int i = 0; i < BaseChildren.Length; i++)
                {
                    BaseChildren[i].ToString(stringBuilder, depth + 1);
                }

                if (inScopeAppendAction is not null)
                    inScopeAppendAction.Invoke(stringBuilder, depth);
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
                stringBuilder.AppendLine("}");
            }
            else
            {
              stringBuilder.AppendLine();
            }

            Spacing(stringBuilder, depth);

            return stringBuilder;
        }

        protected void AddNewLineWithSpacing(StringBuilder str, int depth)
        {
            str.AppendLine();
            Spacing(str, depth);
        }

        protected void Spacing(StringBuilder str, int depth)
        {
            str.Append(' ', 2 * (depth));
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