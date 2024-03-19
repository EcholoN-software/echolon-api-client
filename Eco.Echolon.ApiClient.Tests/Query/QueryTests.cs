using System.Collections.Generic;
using Eco.Echolon.ApiClient.Query;
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
    public void GraphField_WithArgs()
    {
        var args = new Dictionary<string, object>(){["key"] = "pewpew"};
        var provider = new QueryProvider(new QueryConfigurator());
        var query = provider.GetGraphQlQuery(["views", "incidents"], args, typeof(TestClass));
        query.ShouldBe("query{views{incidents(key: \"pewpew\"){pewPew }}}");
    }
}

public class TestClass{
    public string PewPew { get; init; }
}