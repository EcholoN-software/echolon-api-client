using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;
using Eco.Echolon.ApiClient.Model.WorkingQueue;

namespace Eco.Echolon.ApiClient.Client.RestApi
{
    public interface IBaseRestClient
    {
        Task<ApiResult<FileKey>> CreateNewFile(FileInput input);
        Task<ApiResult> UploadFileData(FileKey key, Stream stream);
        Task<ApiResult<FormattedTextId>> StoreFormattedText(string formattedText);
        Task<ApiResult<string>> GetFormattedText(FormattedTextId id);
        Task<ApiResult<EmbeddedResource>> UploadEmbedded(Stream stream, string fileName);
        Task<ApiResult<EmbeddedResource>> UploadEmbedded(Stream stream, MediaTypeHeaderValue contentType);
        Task<ApiResult<WorkQueuePointer[]>> Get();
        Task<ApiResult> Dequeue(WorkingQueueId id);
        Task<ApiResult<Version>> EcholonVersion();
    }
}