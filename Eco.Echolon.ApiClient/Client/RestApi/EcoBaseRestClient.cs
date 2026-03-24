using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
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

        public async Task<ApiResult<FileKey>> CreateNewFile(FileInput input,
            CancellationToken cancellationToken = default)
        {
            var url = _config.ApiUri + "/api/files/upload";
            var content = new StringContent(Serialize(input));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync(url, content, cancellationToken);

            if (response.IsSuccessStatusCode && response.Content is not null)
            {
                var fileKey = Deserialize<Dictionary<string, FileKey>>(await response.Content.ReadAsStringAsync());
                if (fileKey is not null)
                    return ApiResult.Success(fileKey["filekey"]);
                return ApiResult.Faulted<FileKey>(new[] { Fault.InvalidResponse(),  });
            }

            return ApiResult.Faulted<FileKey>(await ExtractFaults(response));
        }

        public async Task<ApiResult> UploadFileData(FileKey key, Stream stream,
            CancellationToken cancellationToken = default)
        {
            var url = _config.ApiUri + $"/api/files/upload/{key}";
            var response = await _client.PostAsync(url, new StreamContent(stream), cancellationToken);

            if (response.IsSuccessStatusCode)
                return ApiResult.Success();

            return ApiResult.Faulted(await ExtractFaults(response));
        }

        public async Task<ApiResult<FormattedTextId>> StoreFormattedText(string formattedText,
            CancellationToken cancellationToken = default)
        {
            var url = _config.ApiUri + "/api/formattedtexts/";
            var response = await _client.PostAsync(url, new StringContent(formattedText), cancellationToken);

            if (response.IsSuccessStatusCode && response.Content is not null)
            {
                var fText = Deserialize<Dictionary<string, FormattedTextId>>(await response.Content.ReadAsStringAsync());
                if (fText is not null)
                    return ApiResult.Success(fText["key"]);
                return ApiResult.Faulted<FormattedTextId>(new[] { Fault.InvalidResponse(),  });
            }

            return ApiResult.Faulted<FormattedTextId>(await ExtractFaults(response));
        }

        public async Task<ApiResult<string>> GetFormattedText(FormattedTextId id,
            CancellationToken cancellationToken = default)
        {
            var url = _config.ApiUri + "/api/formattedtexts/" + id;
            var response = await _client.GetAsync(url, cancellationToken);

            if (response.IsSuccessStatusCode)
                return ApiResult.Success(await response.Content.ReadAsStringAsync());

            return ApiResult.Faulted<string>(await ExtractFaults(response));
        }

        public async Task<ApiResult<EmbeddedResource>> UploadEmbedded(Stream stream, MediaTypeHeaderValue contentType,
            CancellationToken cancellationToken = default)
        {
            var url = _config.ApiUri + "/api/formattedtexts/embedded";
            var content = new StreamContent(stream);
            content.Headers.ContentType = contentType;

            var response = await _client.PostAsync(url, content, cancellationToken);
            
            if (response.IsSuccessStatusCode && response.Content is not null)
            {
                var resource = Deserialize<EmbeddedResource>(await response.Content.ReadAsStringAsync());
                if (resource is not null)
                    return ApiResult.Success(resource);
                return ApiResult.Faulted<EmbeddedResource>(new[] { Fault.InvalidResponse(),  });
            }
            
            return ApiResult.Faulted<EmbeddedResource>(await ExtractFaults(response));
        }

        public async Task<ApiResult<WorkQueuePointer[]>> Get(CancellationToken cancellationToken = default)
        {
            var url = _config.ApiUri + "/api/working/queue";

            var response = await _client.GetAsync(url, cancellationToken);

            if (response.IsSuccessStatusCode && response.Content is not null)
            {
                var pointer = Deserialize<WorkQueuePointer[]>(await response.Content.ReadAsStringAsync());
                if (pointer is not null)
                    return ApiResult.Success(pointer);
                return ApiResult.Faulted<WorkQueuePointer[]>(new[] { Fault.InvalidResponse(),  });
            }
            
            return ApiResult.Faulted<WorkQueuePointer[]>(await ExtractFaults(response));
        }

        public async Task<ApiResult> Dequeue(WorkingQueueId id, CancellationToken cancellationToken = default)
        {
            var url = _config.ApiUri + "/api/working/queue/" + id;
            var r = await _client.DeleteAsync(url, cancellationToken);

            if (r.IsSuccessStatusCode)
                return ApiResult.Success();

            return ApiResult.Faulted(await ExtractFaults(r));
        }

        public async Task<ApiResult<Version>> EcholonVersion(CancellationToken cancellationToken = default)
        {
            var url = _config.ApiUri + "/api/version/echolon";

            var response = await _client.GetAsync(url, cancellationToken);
            
            if (response.IsSuccessStatusCode && response.Content is not null)
            {
                var version = Deserialize<Version>(await response.Content.ReadAsStringAsync());
                if (version is not null)
                    return ApiResult.Success(version);
                return ApiResult.Faulted<Version>(new[] { Fault.InvalidResponse(),  });
            }
            
            return ApiResult.Faulted<Version>(await ExtractFaults(response));
        }

        public async Task<ApiResult<EmbeddedResource>> UploadEmbedded(Stream stream, string fileName,
            CancellationToken cancellationToken = default)
        {
            return await UploadEmbedded(stream, GuessMimeTypeByName(fileName), cancellationToken);
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
                var f = Deserialize<Fault[]>(respAsString);
                if (f is not null)
                    faultList.AddRange(f);
                else
                    faultList.Add(new Fault("Unknown_Fault",
                        "Could not deserialize Fault. Please search in the Webapi log for further information."));
            }
            else
            {
                faultList.Add(new Fault("No_Api_Response", "WebApi didn't return Errors but a HTTP Error Code"));
                faultList.Add(new Fault("HTTP_ERROR", $"WebApi returned HTTP StatusCode: {response.StatusCode}"));
            }

            return faultList.ToArray();
        }

        private T? Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        private string Serialize<T>(T input)
        {
            return JsonConvert.SerializeObject(input);
        }
    }
}
