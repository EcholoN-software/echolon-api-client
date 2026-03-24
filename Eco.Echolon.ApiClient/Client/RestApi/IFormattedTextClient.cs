using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Client.RestApi
{
    public interface IFormattedTextClient
    {
        Task<ApiResult<FormattedTextId>> Upload(string formattedText, string[]? types,
            CancellationToken cancellationToken = default);
        Task<ApiResult<FormattedTextId>> Upload(string formattedText,
            CancellationToken cancellationToken = default);
        Task<ApiResult<EmbeddedResource>> UploadEmbedded(Stream stream, MediaTypeHeaderValue contentType,
            CancellationToken cancellationToken = default);
        Task<ApiResult<EmbeddedResource>> UploadEmbedded(Stream stream, string fileName,
            CancellationToken cancellationToken = default);
        Task<ApiResult<string>> Get(FormattedTextId formattedTextId,
            CancellationToken cancellationToken = default);
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
