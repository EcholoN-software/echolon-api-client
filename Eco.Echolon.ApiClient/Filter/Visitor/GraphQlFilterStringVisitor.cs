using System;
using System.Collections.Generic;
using System.Linq;
using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Query;

namespace Eco.Echolon.ApiClient.Filter.Visitor
{
    public class GraphQlFilterStringVisitor : IGraphQlFilterVisitor<string>
    {
        private readonly IDictionary<string, object> _params;

        public GraphQlFilterStringVisitor(IDictionary<string, object>? @params = null)
        {
            _params = @params ?? new Dictionary<string, object>();
        }

        public string Visit<TValue>(VariableValue<TValue> filter)
        {
            if (_params.TryGetValue(filter.VariableName, out var value))
                return new ConstantValue<TValue>((TValue)value).Accept(this);
            throw new ParameterNotSuppliedException(filter.VariableName);
        }

        public string Visit(AndFilter filter)
        {
            return $"{{and: [{string.Join(", ", filter.Filters.Select(x => x.Accept(this)))}]}}";
        }

        public string Visit(OrFilter filter)
        {
            return $"{{or: [{string.Join(", ", filter.Filters.Select(x => x.Accept(this)))}]}}";
        }

        public string Visit<TValue>(ConstantValue<TValue> filter)
        {
            return GraphQLConvert.Serialize(filter.Value);
        }

        public string Visit(EqualsFilter filter)
        {
            return $"{{{filter.Field}_eq: {filter.Value.Accept(this)}}}";
        }

        public string Visit(NotFilter filter)
        {
            return $"{{{filter.Field}_not: {filter.Value.Accept(this)}}}";
        }

        public string Visit(GreaterThanFilter filter)
        {
            return $"{{{filter.Field}_gt: {filter.Value.Accept(this)}}}";
        }

        public string Visit(GreaterOrEqualFilter filter)
        {
            return $"{{{filter.Field}_gte: {filter.Value.Accept(this)}}}";
        }

        public string Visit(LesserThanFilter filter)
        {
            return $"{{{filter.Field}_lt: {filter.Value.Accept(this)}}}";
        }

        public string Visit(LesserOrEqualFilter filter)
        {
            return $"{{{filter.Field}_lte: {filter.Value.Accept(this)}}}";
        }

        public string Visit(NullValue filter)
        {
            return "null";
        }

        public string Visit(InFilter filter)
        {
            return $"{{{filter.Field}_in: {filter.Value.Accept(this)}}}";
        }

        public string Visit(StartsWithFilter filter)
        {
            return $"{{{filter.Field}_starts_with: {filter.Value.Accept(this)}}}";
        }

        public string Visit(EndsWithFilter filter)
        {
            return $"{{{filter.Field}_ends_with: {filter.Value.Accept(this)}}}";
        }

        public string Visit(ContainsFilter filter)
        {
            return $"{{{filter.Field}_contains: {filter.Value.Accept(this)}}}";
        }

        public string Visit<TValue>(CollectionValueFilter<TValue> filter)
        {
            return $"[{string.Join(", ", filter.Value.Select(x => x.Accept(this)))}]";
        }
    }
}
