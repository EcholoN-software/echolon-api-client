using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Client.RestApi
{
    public interface IFileClient
    {
        Task<ApiResult<FileKey>> Upload(FileInput fileName, Stream stream,
            CancellationToken cancellationToken = default);
        Task<ApiResult<FileInfoResult>> Info(FileKey fileKey,
            CancellationToken cancellationToken = default);
        Task<ApiResult<Stream>> Download(FileKey fileKey,
            CancellationToken cancellationToken = default);
    }
}
