using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Client.RestApi
{
    public class FormattedTextClient : IFormattedTextClient
    {
        private readonly IBaseRestClient _baseRestClient;

        public FormattedTextClient(IBaseRestClient baseRestClient)
        {
            _baseRestClient = baseRestClient;
        }

        public async Task<ApiResult<FormattedTextId>> Store(string formattedText)
        {
            return await _baseRestClient.StoreFormattedText(formattedText);
        }

        public async Task<ApiResult<FormattedTextId>> Upload(string formattedText)
        {
            return await Upload(formattedText, null);
        }

        public async Task<ApiResult<FormattedTextId>> Upload(string formattedText, string[]? types)
        {
            var regex = new Regex(@"!\[(.*?)]\((.*?)\)", RegexOptions.ECMAScript);
            var fault = new List<Fault>();
            var matches = regex.Matches(formattedText);
            var currentText = formattedText;
            var embedCount = 0;

            foreach (Match match in matches)
            {
                var path = match.Groups[2].Value;
                if (!File.Exists(path))
                    continue;

                var f = File.OpenRead(path);
                ApiResult<EmbeddedResource> uploadResult;
                if (types == null || !types.Any())
                {
                    var li = f.Name.LastIndexOf('\\') + 1;
                    var name = f.Name.Substring(li);
                    uploadResult = await UploadEmbedded(f, name);
                }
                else
                {
                    if (embedCount >= types?.Length)
                    {
                        fault.Add(new Fault("Missing_Types",
                            $"Uploading embedding {embedCount} failed due to a missing type"));
                        break;
                    }
                    uploadResult = await UploadEmbedded(f, new MediaTypeHeaderValue(types![embedCount++]));
                }

                if (uploadResult.IsFaulted)
                    fault.AddRange(uploadResult.Faults);

                var embeddedResource = uploadResult.GetData();
                currentText = currentText.Replace(match.Groups[0].Value,
                    $"![{match.Groups[1]}]({embeddedResource.EmbedUrl})");
            }

            if (fault.Count != 0)
                return ApiResult.Faulted<FormattedTextId>(fault.ToArray());

            var upload = await Store(currentText);

            return upload;
        }

        public async Task<ApiResult<EmbeddedResource>> UploadEmbedded(Stream stream, MediaTypeHeaderValue contentType)
        {
            return await _baseRestClient.UploadEmbedded(stream, contentType);
        }

        public async Task<ApiResult<EmbeddedResource>> UploadEmbedded(Stream stream, string fileName)
        {
            return await _baseRestClient.UploadEmbedded(stream, fileName);
        }


        public Task<ApiResult<string>> Get(FormattedTextId formattedTextId)
        {
            return _baseRestClient.GetFormattedText(formattedTextId);
        }
    }
}