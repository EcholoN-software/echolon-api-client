using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Authentication;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;
using Eco.Echolon.ApiClient.Model.WorkingQueue;
using Newtonsoft.Json;

namespace Eco.Echolon.ApiClient.Client.RestApi
{
    public class EcoBaseRestClient : IBaseRestClient
    {
        private readonly EcholonApiClientConfiguration _config;
        private readonly HttpClient _client;

        public EcoBaseRestClient(EcholonApiClientConfiguration config, IHttpClientFactory factory)
        {
            _config = config;
            _client = factory.CreateClient(Variables.HttpClientForApi);
        }

        public async Task<ApiResult<FileKey>> CreateNewFile(FileInput input)
        {
            var url = _config.ApiUri + "/api/files/upload";
            var content = new StringContent(Serialize(input));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
                return ApiResult.Success(
                    Deserialize<Dictionary<string, FileKey>>(await response.Content.ReadAsStringAsync())["filekey"]);

            return ApiResult.Faulted<FileKey>(await ExtractFaults(response));
        }

        public async Task<ApiResult> UploadFileData(FileKey key, Stream stream)
        {
            var url = _config.ApiUri + $"/api/files/upload/{key}";
            var response = await _client.PostAsync(url, new StreamContent(stream));

            if (response.IsSuccessStatusCode)
                return ApiResult.Success();

            return ApiResult.Faulted(await ExtractFaults(response));
        }

        public async Task<ApiResult<FormattedTextId>> StoreFormattedText(string formattedText)
        {
            var url = _config.ApiUri + "/api/formattedtexts/";
            var response = await _client.PostAsync(url, new StringContent(formattedText));

            if (response.IsSuccessStatusCode)
                return ApiResult.Success(
                    Deserialize<Dictionary<string, FormattedTextId>>(
                        await response.Content.ReadAsStringAsync())["key"]);

            return ApiResult.Faulted<FormattedTextId>(await ExtractFaults(response));
        }

        public async Task<ApiResult<string>> GetFormattedText(FormattedTextId id)
        {
            var url = _config.ApiUri + "/api/formattedtexts/" + id;
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
                return ApiResult.Success(await response.Content.ReadAsStringAsync());

            return ApiResult.Faulted<string>(await ExtractFaults(response));
        }

        public async Task<ApiResult<EmbeddedResource>> UploadEmbedded(Stream stream, MediaTypeHeaderValue contentType)
        {
            var url = _config.ApiUri + "/api/formattedtexts/embedded";
            var content = new StreamContent(stream);
            content.Headers.ContentType = contentType;

            var response = await _client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
                return ApiResult.Success(Deserialize<EmbeddedResource>(await response.Content.ReadAsStringAsync()));

            return ApiResult.Faulted<EmbeddedResource>(await ExtractFaults(response));
        }

        public async Task<ApiResult<WorkQueuePointer[]>> Get()
        {
            var url = _config.ApiUri + "/api/working/queue";

            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
                return ApiResult.Success(Deserialize<WorkQueuePointer[]>(await response.Content.ReadAsStringAsync()));
            
            return ApiResult.Faulted<WorkQueuePointer[]>(await ExtractFaults(response));
        }

        public async Task<ApiResult> Dequeue(WorkingQueueId id)
        {
            var url = _config.ApiUri + "/api/working/queue/" + id;
            var r = await _client.DeleteAsync(url);
            
            if(r.IsSuccessStatusCode)
                return ApiResult.Success();
            
            return ApiResult.Faulted(await ExtractFaults(r));
        }

        public async Task<ApiResult<Version>> EcholonVersion()
        {
            var url = _config.ApiUri + "/api/version/echolon";

            var response = await _client.GetAsync(url);
            if(response.IsSuccessStatusCode)
                return ApiResult.Success<Version>(Deserialize<Version>(await response.Content.ReadAsStringAsync()));
            
            return ApiResult.Faulted<Version>(await ExtractFaults(response));
        }

        public async Task<ApiResult<EmbeddedResource>> UploadEmbedded(Stream stream, string fileName)
        {
            return await UploadEmbedded(stream, GuessMimeTypeByName(fileName));
        }

        private MediaTypeHeaderValue GuessMimeTypeByName(string fileName)
        {
            var helper = new MimeTypeHelper();
            var r = helper.GuessMimeTypeByFileName(fileName);
            
            return new MediaTypeHeaderValue(r);
        }

        private async Task<Fault[]> ExtractFaults(HttpResponseMessage response)
        {
            var faultList = new List<Fault>();
            var respAsString = await response.Content.ReadAsStringAsync();
            if (respAsString.Length > 0)
            {
                faultList.AddRange(Deserialize<Fault[]>(respAsString));
            }
            else
            {
                faultList.Add(new Fault("No_Api_Response", "WebApi didn't return Errors but a HTTP Error Code"));
                faultList.Add(new Fault("HTTP_ERROR", $"WebApi returned HTTP StatusCode: {response.StatusCode}"));
            }

            return faultList.ToArray();
        }

        private T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        private string Serialize<T>(T input)
        {
            return JsonConvert.SerializeObject(input);
        }
    }
}