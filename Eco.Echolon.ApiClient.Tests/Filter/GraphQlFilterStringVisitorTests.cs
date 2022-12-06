using System;
using System.Collections.Generic;
using Eco.Echolon.ApiClient.Filter;
using Eco.Echolon.ApiClient.Filter.Builder;
using Eco.Echolon.ApiClient.Filter.Values;
using Eco.Echolon.ApiClient.Filter.Visitor;
using Newtonsoft.Json;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Eco.Echolon.ApiClient.Tests.Filter;

public class GraphQlFilterStringVisitorTests
{
    private readonly ITestOutputHelper _outputHelper;

    public GraphQlFilterStringVisitorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    [Trait("Category", "fast")]
    public void And()
    {
        var sut = new GraphQlFilterStringVisitor();

        var actual = GraphQlFilterBuilder.CreateAnd(x => x
            .Equals.Constant("name", "TestName")
            .Equals.Constant("name", "TestName2")
        ).Build().Accept(sut);
        var expected = "{and: [{name_eq: \"TestName\"},{name_eq: \"TestName2\"}]}";
        (actual, expected) = CompareGqlStrings(actual, expected);
        actual.ShouldBe(expected);
        _outputHelper.WriteLine(expected);
    }    
    
    [Fact]
    [Trait("Category", "fast")]
    public void Or()
    {
        var sut = new GraphQlFilterStringVisitor();

        var actual = GraphQlFilterBuilder.CreateOr(x => x
            .Equals.Constant("name", "TestName")
            .Equals.Constant("name", "TestName2")
        ).Build().Accept(sut);
        var expected = "{or: [{name_eq: \"TestName\"},{name_eq: \"TestName2\"}]}";
        (actual, expected) = CompareGqlStrings(actual, expected);
        actual.ShouldBe(expected);
        _outputHelper.WriteLine(expected);
    }

    [Fact]
    [Trait("Category", "fast")]
    public void Variable_Filter()
    {
        var sut = new GraphQlFilterStringVisitor(new Dictionary<string, object>() { ["value"] = "TestVar" });

        var actual = GraphQlFilterBuilder.Equals.Variable("name", "value").Accept(sut);
        var expected = "{name_eq: \"TestVar\"}";
        (actual, expected) = CompareGqlStrings(actual, expected);
        actual.ShouldBe(expected);
        _outputHelper.WriteLine(expected);
    }

    [Fact]
    [Trait("Category", "fast")]
    public void Variable_Filter_IncorrectType()
    {
        var sut = new GraphQlFilterStringVisitor(new Dictionary<string, object>() { ["value"] = 5 });

        var actual = GraphQlFilterBuilder.Equals.Variable("name", "value").Accept(sut);
        var expected = "{name_eq: 5}";
        (actual, expected) = CompareGqlStrings(actual, expected);
        actual.ShouldBe(expected);
        _outputHelper.WriteLine(expected);
    }

    [Fact]
    [Trait("Category", "fast")]
    public void Variable_Filter_NotSupplied()
    {
        var sut = new GraphQlFilterStringVisitor();

        Assert.Throws<ParameterNotSuppliedException>(() =>
            GraphQlFilterBuilder.Equals.Variable("name", "value").Accept(sut));
    }

    [Theory]
    [InlineData(typeof(ContainsFilter), typeof(ConstantValue<string>), "Test", "_contains")]
    [InlineData(typeof(EndsWithFilter), typeof(ConstantValue<string>), "TestString", "_ends_with")]
    [InlineData(typeof(EqualsFilter), typeof(ConstantValue<object>), 5, "_eq")]
    [InlineData(typeof(EqualsFilter), typeof(NullValue), null, "_eq")]
    [InlineData(typeof(EqualsFilter), typeof(ConstantValue<object>), "string", "_eq")]
    [InlineData(typeof(EqualsFilter), typeof(ConstantValue<object>), 3.1, "_eq")]
    [InlineData(typeof(GreaterOrEqualFilter), typeof(ConstantValue<int>), 5, "_gte")]
    [InlineData(typeof(GreaterThanFilter), typeof(ConstantValue<int>), 5, "_gt")]
    [InlineData(typeof(LesserThanFilter), typeof(ConstantValue<int>), 5, "_lt")]
    [InlineData(typeof(LesserOrEqualFilter), typeof(ConstantValue<int>), 5, "_lte")]
    [InlineData(typeof(NotFilter), typeof(ConstantValue<object>), 5, "_not")]
    [InlineData(typeof(StartsWithFilter), typeof(ConstantValue<string>), "testString", "_starts_with")]
    public void Correct_Suffix(Type filterType, Type valueType, object value, string suffix)
    {
        var sut = new GraphQlFilterStringVisitor();
        const string fieldName = "TestField";
        var filter = GraphQlFilterBuilder.CreateField(filterType, valueType, fieldName, value);
        filter.ShouldBeOfType(filterType);
        var expected = $"{{{fieldName}{suffix}: {JsonConvert.SerializeObject(value)}}}";
        var actual = filter.Accept(sut);
        (actual, expected) = CompareGqlStrings(actual, expected);
        _outputHelper.WriteLine(expected);
        actual.ShouldBe(expected);
    }


    [Fact]
    public void In_WithConstantAndVariable()
    {
        var sut = new GraphQlFilterStringVisitor(new Dictionary<string, object>() { ["testVariable"] = "TestVar" });

        var filter = GraphQlFilterBuilder.CreateField<InFilter, CollectionValueFilter<object>, object>("Test",
            new SingleValueFilter<object>[]
            {
                new ConstantValue<object>("testValue"),
                new VariableValue<object>("testVariable")
            });
        var expected = $"{{Test_in: [\"testValue\", \"TestVar\"]}}";
        var actual = filter.Accept(sut);
        (actual, expected) = CompareGqlStrings(actual, expected);
        _outputHelper.WriteLine(expected);
        actual.ShouldBe(expected);
    }

    private (string actual, string expected) CompareGqlStrings(string actual, string expected)
    {
        string Normalize(string gql) => gql.Replace("\n", "").Replace(" ", "");
        return (Normalize(actual), Normalize(expected));
    }
}