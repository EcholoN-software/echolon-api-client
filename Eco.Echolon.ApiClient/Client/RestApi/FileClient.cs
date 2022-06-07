using System.IO;
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

        public async Task<ApiResult<FileKey>> Upload(FileInput fileName, Stream stream)
        {
            var result = await _restClient.CreateNewFile(fileName);

            if (result.IsFaulted)
                return result;

            var uploadResult = await _restClient.UploadFileData(result.GetData(), stream);

            if (uploadResult.IsFaulted)
                return ApiResult.Faulted<FileKey>(uploadResult.Faults);

            return result;
        }
    }
}