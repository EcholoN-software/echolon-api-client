# .NET API Client for EcholoN Service Management Suite
[![Build](https://github.com/EcholoN-software/echolon-api-client/actions/workflows/dotnet.yml/badge.svg)](https://github.com/EcholoN-software/echolon-api-client/actions/workflows/dotnet.yml)

This Repository contains the code for the .NET API Client for [EcholoN](https://www.echolon.de) Service Management Suite

## Getting started

```csharp
using System;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient;

namespace MyApp
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var apiUri = new Uri("https://example.com/api");
            var identityHubUri = new Uri("https://example.com/identityhub");
            var config = new EcholonApiClientConfiguration(apiUri, identityHubUri, "Customer.MyApp", "My Secret");
            using var clientFactory = new ApiClientFactory(config);

            var client = clientFactory.Create();

            // use "client" to access API functions related to your EcholoN instance setup
        }
    }
}
```

For further questions please contact support with your specific use-case or visit www.echolon.de
