using System;
using System.Collections.Generic;
using System.Linq;
using Eco.Echolon.ApiClient.Query;

namespace Eco.Echolon.ApiClient.Filter.Visitor
{
    public class GraphQlFilterStringVisitor : IGraphQlFilterVisitor<string>
    {
        private readonly IDictionary<string, object> _params;

        public GraphQlFilterStringVisitor(IDictionary<string, object> @params)
        {
            _params = @params ?? throw new ArgumentNullException(nameof(@params));
        }

        public string Visit(VariableValueGraphQlFilter filter)
        {
            if (_params.TryGetValue(filter.VariableName, out var value))
                return new ConstantValueGraphQlFilter(value).Accept(this);
            throw new ParameterNotSuppliedException(filter.VariableName);
        }

        public string Visit(AndGraphQlFilter filter)
        {
            return $"{{and: [{string.Join(", ", filter.Filters.Select(x => x.Accept(this)))}]}}";
        }

        public string Visit(OrGraphQlFilter filter)
        {
            return $"{{or: [{string.Join(", ", filter.Filters.Select(x => x.Accept(this)))}]}}";
        }

        public string Visit(ConstantValueGraphQlFilter filter)
        {
            return GraphQLConvert.Serialize(filter.Value);
        }

        public string Visit(EqComparisonGraphQlFilter filter)
        {
            //TODO: Workaround GQL-Api Bug
            return new InComparisonGraphQlFilter(filter.Field, new CollectionValueGraphQlFilter(new[] {filter.Value}))
                .Accept(this);
            return $"{{{filter.Field}_eq: {filter.Value.Accept(this)}}}";
        }

        public string Visit(NotComparisonGraphQlFilter filter)
        {
            return $"{{{filter.Field}_not: {filter.Value.Accept(this)}}}";
        }

        public string Visit(GreaterThanComparisonGraphQlFilter filter)
        {
            return $"{{{filter.Field}_gt: {filter.Value.Accept(this)}}}";
        }

        public string Visit(GreaterOrEqualComparisonGraphQlFilter filter)
        {
            return $"{{{filter.Field}_gte: {filter.Value.Accept(this)}}}";
        }

        public string Visit(LesserThanComparisonGraphQlFilter filter)
        {
            return $"{{{filter.Field}_lt: {filter.Value.Accept(this)}}}";
        }

        public string Visit(LesserOrEqualComparisonGraphQlFilter filter)
        {
            return $"{{{filter.Field}_lte: {filter.Value.Accept(this)}}}";
        }

        public string Visit(NullValueGraphQlFilter filter)
        {
            return "null";
        }

        public string Visit(InComparisonGraphQlFilter filter)
        {
            return $"{{{filter.Field}_in: {filter.Value.Accept(this)}}}";
        }

        public string Visit(StartsWithComparisonGraphQlFilter filter)
        {
            return $"{{{filter.Field}_starts_with: {filter.Value.Accept(this)}}}";
        }

        public string Visit(NullComparisonGraphQlFilter filter)
        {
            return new EqComparisonGraphQlFilter(filter.Field, new NullValueGraphQlFilter()).Accept(this);
        }

        public string Visit(EndsWithComparisonGraphQlFilter filter)
        {
            return $"{{{filter.Field}_ends_with: {filter.Value.Accept(this)}}}";
        }

        public string Visit(ContainsComparisonGraphQlFilter filter)
        {
            return $"{{{filter.Field}_contains: {filter.Value.Accept(this)}}}";
        }

        public string Visit(CollectionValueGraphQlFilter filter)
        {
            return $"[{string.Join(", ", filter.Value.Select(x => x.Accept(this)))}]";
        }
    }
}
