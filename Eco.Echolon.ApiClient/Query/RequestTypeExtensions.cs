using System;

namespace Eco.Echolon.ApiClient.Query
{
    public static class RequestTypeExtensions
    {
        public static string ToQueryKeyWord(this RequestType t) => 
            t switch
            {
                RequestType.Query => "query",
                RequestType.Mutation => "mutation",
                RequestType.Subscription => "subscription",
                _ => throw new ArgumentOutOfRangeException(nameof(t), t, null)
            };
        
    }
}