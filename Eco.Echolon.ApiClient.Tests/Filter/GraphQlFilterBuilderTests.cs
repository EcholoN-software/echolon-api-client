using System;
using System.Linq;
using Eco.Echolon.ApiClient.Filter;
using Eco.Echolon.ApiClient.Filter.Builder;
using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;
using Shouldly;
using Xunit;

namespace Eco.Echolon.ApiClient.Tests.Filter;

public class GraphQlFilterBuilderTests
{
    [Fact]
    public void GroupFilter()
    {
        var filter = GraphQlFilterBuilder.CreateAnd(and => and
            .StartsWith.Constant("TestField", "Pew")
            .Or(or => or
                .Equals.Constant("TestField1", new Identity(new ItemId(Guid.NewGuid()), new EntityId(Guid.NewGuid())))
                .Equals.Variable<int>("TestField2", "variable"))
            .LesserThan.Variable("test", "test")
            .GreaterThan.Constant("TestField2", 8)
        ).Build();

        filter.ShouldBeOfType<AndFilter>();
        var and = (AndFilter)filter;
        and.Filters.Count().ShouldBe(4);
        and.Filters.ShouldContain(x => x.GetType() == typeof(OrFilter));
    }


    [Theory]
    [InlineData("Test")]
    [InlineData(6)]
    [InlineData(6.6)]
    public void Equals_CustomConstant(object value)
    {
        var filter = GraphQlFilterBuilder.Equals.CustomConstant("testField", value);
        filter.ShouldBeOfType(typeof(EqualsFilter));
        var equals = (EqualsFilter)filter;
        equals.Field.ShouldBe("testField");
        equals.Value.ShouldBeOfType<ConstantValue<object>>();
        ((ConstantValue<object>)equals.Value).Value.ShouldBe(value);
    }

    [Fact]
    [Trait("Category", "fast")]
    public void Equals_Constant_Number()
    {
        var filter = GraphQlFilterBuilder.Equals.Constant("testField", 5.7);
        filter.ShouldBeOfType(typeof(EqualsFilter));
        var equals = (EqualsFilter)filter;
        equals.Field.ShouldBe("testField");
        equals.Value.ShouldBeOfType<ConstantValue<object>>();
        ((ConstantValue<object>)equals.Value).Value.ShouldBe(5.7);
    }

    [Fact]
    [Trait("Category", "fast")]
    public void Equals_Constant_String()
    {
        var filter = GraphQlFilterBuilder.Equals.Constant("testField", "Test");
        filter.ShouldBeOfType(typeof(EqualsFilter));
        var equals = (EqualsFilter)filter;
        equals.Field.ShouldBe("testField");
        equals.Value.ShouldBeOfType<ConstantValue<object>>();
        ((ConstantValue<object>)equals.Value).Value.ShouldBe("Test");
    }

    [Fact]
    [Trait("Category", "fast")]
    public void Equals_Constant_Identity()
    {
        var id = new Identity(new ItemId(Guid.NewGuid()), new EntityId(Guid.NewGuid()));
        var filter = GraphQlFilterBuilder.Equals.Constant("testField", id);
        filter.ShouldBeOfType(typeof(EqualsFilter));
        var equals = (EqualsFilter)filter;
        equals.Field.ShouldBe("testField");
        equals.Value.ShouldBeOfType<ConstantValue<object>>();
        ((ConstantValue<object>)equals.Value).Value.ShouldBe(id);
    }

    [Fact]
    public void IsNull()
    {
        var filter = GraphQlFilterBuilder.Equals.Null("testField");
        filter.ShouldBeOfType(typeof(EqualsFilter));
        var equals = (EqualsFilter)filter;
        equals.Field.ShouldBe("testField");
        equals.Value.ShouldBeOfType<NullValue>();
    }

    [Fact]
    public void SingleExpressionBuilderTest()
    {
        var filter = GraphQlFilterBuilder.StartsWith.Constant("name", "testField");
        filter.ShouldBeOfType(typeof(StartsWithFilter));
        var equals = (StartsWithFilter)filter;
        equals.Field.ShouldBe("name");
        equals.Value.ShouldBeOfType<ConstantValue<string>>();
    }

    [Fact]
    public void In_Creation()
    {
        var filter =
            GraphQlFilterBuilder.CreateField<InFilter, CollectionValueFilter<object>, object>("Test",
                new[] { new ConstantValue<object>("testValue") });
        filter.ShouldBeOfType(typeof(InFilter));
        var equals = (InFilter)filter;
        equals.Field.ShouldBe("Test");
    }

    [Theory]
    [InlineData(typeof(ContainsFilter), typeof(ConstantValue<string>), "Test")]
    [InlineData(typeof(EndsWithFilter), typeof(ConstantValue<string>), "TestString")]
    [InlineData(typeof(EqualsFilter), typeof(ConstantValue<object>), 5)]
    [InlineData(typeof(EqualsFilter), typeof(NullValue), null)]
    [InlineData(typeof(EqualsFilter), typeof(ConstantValue<object>), "string")]
    // [InlineData(typeof(EqualsFilter), typeof(ConstantValue<string>), "string")]
    [InlineData(typeof(EqualsFilter), typeof(ConstantValue<object>), 3.1)]
    [InlineData(typeof(GreaterOrEqualFilter), typeof(ConstantValue<int>), 5)]
    [InlineData(typeof(GreaterThanFilter), typeof(ConstantValue<int>), 5)]
    [InlineData(typeof(LesserThanFilter), typeof(VariableValue<int>), "Test")]
    [InlineData(typeof(LesserOrEqualFilter), typeof(ConstantValue<int>), 5)]
    [InlineData(typeof(NotFilter), typeof(ConstantValue<object>), 5)]
    [InlineData(typeof(StartsWithFilter), typeof(VariableValue<string>), "testString")]
    public void SingleValue_Runtime_Valid(Type filterType, Type valueType, object value)
    {
        const string fieldName = "TestField";
        var filter = GraphQlFilterBuilder.CreateField(filterType, valueType, fieldName, value);
        filter.ShouldBeOfType(filterType);
        var dynamicFilter = (dynamic)filter;
        ((string)dynamicFilter.Field).ShouldBe(fieldName);
        ((IFilter)dynamicFilter.Value).ShouldBeOfType(valueType);
        if (valueType == typeof(ConstantValue<>))
            ((ConstantValue<object>)dynamicFilter.Value).Value.ShouldBe(value);
        if (valueType == typeof(VariableValue<>))
            ((VariableValue<object>)dynamicFilter.Value).VariableName.ShouldBe(value);
    }


    [Theory]
    [InlineData(typeof(ContainsFilter), typeof(ConstantValue<string>), 6)]
    [InlineData(typeof(ContainsFilter), typeof(ConstantValue<int>), 6)]
    public void SingleValue_Runtime_InValid(Type filterType, Type valueType, object value)
    {
        Assert.ThrowsAny<Exception>((Action)(() =>
        {
            const string fieldName = "TestField";
            GraphQlFilterBuilder.CreateField(filterType, valueType, fieldName, value);
        }));
    }
}