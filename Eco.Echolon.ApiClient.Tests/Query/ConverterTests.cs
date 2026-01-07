using System;
using System.IO;
using Eco.Echolon.ApiClient.Model.Results;
using Eco.Echolon.ApiClient.Query;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Eco.Echolon.ApiClient.Tests.Query;

public class ConverterTests
{
    
    [Fact]
    [Trait("Category", "fast")]
    public void JsonSerializerNullable()
    {
        var json = @"{
      ""name"" : ""Administration"",
      ""notes"" : null,
      ""hasRelatedPrivileged"" : false,
      ""hasRelatedAccounts"" : true,
      ""key"" : ""Administration"",
      ""created"" : null,
      ""modified"" : null,
      ""isActive"" : true,
      ""id"" : ""d683ca67-1b90-405f-a93b-e8aca4b252a9""
    }";
        var b = typeof(Guid?) == typeof(Guid);
        var serializer = new JsonSerializer();
        serializer.Converters.Add(new KeyValueDictionaryJsonConverter());
        var reader = new JsonTextReader(new StringReader(json));
        var priv = serializer.Deserialize<SystemPrivileges>(reader);
    }

    [Fact]
    [Trait("Category", "fast")]
    public void Deserialize_DictionaryWithTypedValue()
    {
        var json = @"{""dictionary"":[{""key"": ""testKey"", ""value"": {""pewpew"": ""Test""}}]}";
        var serializer = new JsonSerializer();
        serializer.Converters.Add(new DictionaryJsonConverter());
        serializer.Converters.Add(new KeyValueDictionaryJsonConverter());
        var reader = new JsonTextReader(new StringReader(json));
        var priv = serializer.Deserialize<TestDictionaryProperty>(reader);
        priv.ShouldNotBeNull().Dictionary.ShouldNotBeNull()["testKey"].ShouldBeOfType<TestClass>();
    }

}