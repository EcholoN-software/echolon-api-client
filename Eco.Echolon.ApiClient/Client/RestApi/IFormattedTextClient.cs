using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Client.RestApi
{
    public interface IFormattedTextClient
    {
        Task<ApiResult<FormattedTextId>> Upload(string formattedText, string[]? types);
        Task<ApiResult<FormattedTextId>> Upload(string formattedText);
        Task<ApiResult<EmbeddedResource>> UploadEmbedded(Stream stream, MediaTypeHeaderValue contentType);
        Task<ApiResult<EmbeddedResource>> UploadEmbedded(Stream stream, string fileName);
        Task<ApiResult<string>> Get(FormattedTextId formattedTextId);
    }

    public class EmbeddedResource
    {
        public FormattedTextId Key { get; set; }
        public string EmbedUrl { get; set; }

        public EmbeddedResource(FormattedTextId key, string embedUrl)
        {
            Key = key;
            EmbedUrl = embedUrl;
        }
    }
}