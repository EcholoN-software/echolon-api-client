using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;
using Eco.Echolon.ApiClient.Model.WorkingQueue;

namespace Eco.Echolon.ApiClient.Client.RestApi
{
    public interface IBaseRestClient
    {
        Task<ApiResult<FileKey>> CreateNewFile(FileInput input, CancellationToken cancellationToken = default);
        Task<ApiResult> UploadFileData(FileKey key, Stream stream, string? contentType = null, CancellationToken cancellationToken = default);
        Task<ApiResult<FileInfoResult>> GetFileInfo(FileKey key, CancellationToken cancellationToken = default);
        Task<ApiResult<Stream>> DownloadFile(FileKey key, CancellationToken cancellationToken = default);
        Task<ApiResult<FormattedTextId>> StoreFormattedText(string formattedText, CancellationToken cancellationToken = default);
        Task<ApiResult<string>> GetFormattedText(FormattedTextId id, CancellationToken cancellationToken = default);
        Task<ApiResult<EmbeddedResource>> UploadEmbedded(Stream stream, string fileName, CancellationToken cancellationToken = default);
        Task<ApiResult<EmbeddedResource>> UploadEmbedded(Stream stream, MediaTypeHeaderValue contentType, CancellationToken cancellationToken = default);
        Task<ApiResult<WorkQueuePointer[]>> Get(CancellationToken cancellationToken = default);
        Task<ApiResult> Dequeue(WorkingQueueId id, CancellationToken cancellationToken = default);
        Task<ApiResult<Version>> EcholonVersion(CancellationToken cancellationToken = default);
    }
}
