using System;
using System.Net;
using System.Net.Http;

namespace Eco.Echolon.ApiClient.Client.GraphQl
{
    public class GraphQlRequestException : Exception
    {
        public string GraphQlQuery { get; set; }

        public HttpResponseMessage HttpResponse { get; set; }
        
        public GraphQlRequestException(HttpStatusCode httpRespStatusCode, string query, HttpResponseMessage httpResp) 
            : base($"GraphQl-Api responded with statuscode {httpRespStatusCode} and no further Information")
        {
            GraphQlQuery = query;
            HttpResponse = httpResp;
        }
        
    }
}