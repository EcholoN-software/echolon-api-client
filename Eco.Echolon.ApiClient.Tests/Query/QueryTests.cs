using System;
using System.Collections.Generic;
using System.IO;
using Eco.Echolon.ApiClient.Model.DomainTypes;
using Eco.Echolon.ApiClient.Model.Results;
using Eco.Echolon.ApiClient.Query;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Eco.Echolon.ApiClient.Tests.Query;

public class QueryTests
{
    [Fact]
    public void BuildQuery_R26()
    {
        var b = QueryBuilder.Query()
            .AddField("views", views => views
                .AddField("incidents", incidents => incidents
                    .AddField("id", id => id
                        .AddField("itemId")
                        .AddField("entityId"))
                    .AddField("name")))
            .Build();
        var str = b.ToString();
    }

    [Fact]
    public void GraphField_R26()
    {
        var q = new GraphQLField("query", [
            new GraphQLField("views", [
                new GraphQLField("incidents", [
                    new GraphQLField("id", [
                        new GraphQLField("itemId"),
                        new GraphQLField("entityId")
                    ]),
                    new GraphQLField("name")
                ])
            ])
        ]);
        var str = q.ToString();
    }

    [Fact]
    [Trait("Category", "fast")]
    public void Serialize_DictionaryWithTypedValue()
    {
        var input = new Dictionary<string, object?>()
        {
            ["input"] = new TestDictionaryProperty()
            {
                Dictionary = new Dictionary<string, TestClass>()
                {
                    ["testKey"] = new TestClass() { NullableInt = 0, PewPew = "TestPewpew" },
                    ["testKey2"] = new TestClass() { NullableInt = 0, PewPew = "TestValue" },
                }
            }
        };
        var serialized = QueryBuilder.Mutation(null).AddField("test", x => x.AddField("test"), input).Build()
            .ToString();
    }

    [Fact]
    [Trait("Category", "fast")]
    public void QueryBuilder_Dictionary_TypedValue()
    {
        var qp = new QueryProvider(new QueryConfigurator());
        var req = qp.GetGraphQlQuery(["test"], null, typeof(TestDictionaryProperty));
        req.ShouldBe("query{test{dictionary{key value{pewPew nullableInt }}}}");
    }

    [Fact]
    public void GraphField_WithArgs()
    {
        var args = new Dictionary<string, object?>() { ["key"] = "pewpew" };
        var provider = new QueryProvider(new QueryConfigurator());
        var query = provider.GetGraphQlQuery(["views", "incidents"], args, typeof(TestClass));
        query.ShouldBe("query{views{incidents(key: \"pewpew\"){pewPew nullableInt }}}");
    }
}

public class TestClass
{
    public string? PewPew { get; init; }
    public int? NullableInt { get; init; }
}

public class TestDictionaryProperty
{
    public Dictionary<string, TestClass>? Dictionary { get; init; }
}