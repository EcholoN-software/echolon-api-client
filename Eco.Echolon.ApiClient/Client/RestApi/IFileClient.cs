using System.IO;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Client.RestApi
{
    public interface IFileClient
    {
        Task<ApiResult<FileKey>> Upload(FileInput fileName, Stream stream);
    }
}