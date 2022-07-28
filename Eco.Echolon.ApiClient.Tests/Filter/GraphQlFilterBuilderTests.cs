using System;
using System.Linq;
using Eco.Echolon.ApiClient.Filter;
using Eco.Echolon.ApiClient.Filter.Builder;
using Eco.Echolon.ApiClient.Filter.Values;
using Shouldly;
using Xunit;

namespace Eco.Echolon.ApiClient.Tests.Filter;

public class GraphQlFilterBuilderTests
{
    [Fact]
    public void GroupFilter()
    {
        var filter = GraphQlFilterBuilder.CreateAnd(and => and
            .Field<EqualsFilter, ConstantValue>("TestField", 5)
            .Or(or => or
                .Field<EqualsFilter, ConstantValue>("TestField1", 6)
                .Field<EqualsFilter, ConstantValue>("TestField1", 7))
            .Field<GreaterThanFilter, ConstantValue>("TestField2", 8)
        ).Build();
        
        filter.ShouldBeOfType<AndFilter>();
        var and = (AndFilter)filter;
        and.Filters.Count().ShouldBe(3);
        and.Filters.ShouldContain(x => x.GetType() == typeof(OrFilter));
    }
    
    [Fact]
    public void Equals_Constant()
    {
        var filter = GraphQlFilterBuilder.CreateField<EqualsFilter, ConstantValue>("testField", 5).Build();
        filter.ShouldBeOfType(typeof(EqualsFilter));
        var equals = (EqualsFilter)filter;
        equals.Field.ShouldBe("testField");
        equals.Value.ShouldBeOfType<ConstantValue>();
        ((ConstantValue)equals.Value).Value.ShouldBe(5);
    }

    [Fact]
    public void IsNull()
    {
        var filter = GraphQlFilterBuilder.CreateFieldIsNull("testField").Build();
        filter.ShouldBeOfType(typeof(IsNullFilter));
        var equals = (IsNullFilter)filter;
        equals.Field.ShouldBe("testField");
        equals.Value.ShouldBeOfType<NullValue>();
    }

    [Theory]
    [InlineData(typeof(EqualsFilter), typeof(ConstantValue), 5)]
    [InlineData(typeof(EqualsFilter), typeof(ConstantValue), "string")]
    [InlineData(typeof(EqualsFilter), typeof(ConstantValue), 3.1)]
    [InlineData(typeof(ContainsFilter), typeof(ConstantValue), 5)]
    [InlineData(typeof(EndsWithFilter), typeof(ConstantValue), 5)]
    [InlineData(typeof(GreaterOrEqualFilter), typeof(ConstantValue), 5)]
    [InlineData(typeof(GreaterThanFilter), typeof(ConstantValue), 5)]
    [InlineData(typeof(IsNullFilter), typeof(NullValue), null)]
    [InlineData(typeof(LesserThanFilter), typeof(ConstantValue), 5)]
    [InlineData(typeof(LesserOrEqualFilter), typeof(ConstantValue), 5)]
    [InlineData(typeof(NotFilter), typeof(ConstantValue), 5)]
    [InlineData(typeof(StartsWithFilter), typeof(ConstantValue), "testString")]
    public void SingleValue_Runtime_Valid(Type filterType, Type valueType, object value)
    {
        const string fieldName = "TestField";
        var filter = GraphQlFilterBuilder.CreateField(filterType, valueType, fieldName, value).Build();
        filter.ShouldBeOfType(filterType);
        var equals = (SingleValueFieldComparison)filter;
        equals.Field.ShouldBe(fieldName);
        equals.Value.ShouldBeOfType(valueType);
        if (valueType == typeof(ConstantValue))
            ((ConstantValue)equals.Value).Value.ShouldBe(value);
        if (valueType == typeof(VariableValue))
            ((VariableValue)equals.Value).VariableName.ShouldBe(value);
    }
}