using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Client.RestApi
{
    public class FileClient : IFileClient
    {
        private readonly IBaseRestClient _restClient;

        public FileClient(IBaseRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<ApiResult<FileKey>> Upload(FileInput fileName, Stream stream,
            CancellationToken cancellationToken = default)
        {
            var result = await _restClient.CreateNewFile(fileName, cancellationToken);

            if (result.IsFaulted)
                return result;

            var uploadResult = await _restClient.UploadFileData(result.GetData(), stream, cancellationToken);

            if (uploadResult.IsFaulted)
                return ApiResult.Faulted<FileKey>(uploadResult.Faults);

            return result;
        }

        public async Task<ApiResult<FileInfoResult>> Info(FileKey fileKey,
            CancellationToken cancellationToken = default)
        {
            return await _restClient.GetFileInfo(fileKey, cancellationToken);
        }

        public async Task<ApiResult<Stream>> Download(FileKey fileKey,
            CancellationToken cancellationToken = default)
        {
            return await _restClient.DownloadFile(fileKey, cancellationToken);
        }
    }
}
