using Eco.Echolon.ApiClient.Client.GraphQl;
using Eco.Echolon.ApiClient.Client.RestApi;

namespace Eco.Echolon.ApiClient.Client
{
    public class EcoApiClient : IApiClient
    {
        public IWorkingClient Working { get; }
        public IFormattedTextClient FormattedText { get; }
        public IFileClient File { get; }
        public IEntitiesClient Entities { get; }
        public ITextTemplatesClient TextTemplates { get; set; }
        public IProcessClient Process { get; }
        public IConfigClient Configuration { get; }
        public ISystemClient System { get; }
        public IViewClient Views { get; }
        public IWorkingQueueClient WorkingQueue { get; }

        public EcoApiClient(IWorkingClient workingClient, IFormattedTextClient formattedText, IFileClient file,
            IEntitiesClient entities, ITextTemplatesClient textTemplates, IProcessClient process,
            IConfigClient configuration, ISystemClient client, IViewClient views, IWorkingQueueClient workingQueue)
        {
            Working = workingClient;
            FormattedText = formattedText;
            File = file;
            Entities = entities;
            TextTemplates = textTemplates;
            Process = process;
            Configuration = configuration;
            System = client;
            Views = views;
            WorkingQueue = workingQueue;
        }
    }
}