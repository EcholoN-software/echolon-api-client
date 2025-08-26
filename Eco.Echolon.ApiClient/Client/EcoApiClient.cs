using Eco.Echolon.ApiClient.Client.GraphQl;
using Eco.Echolon.ApiClient.Client.GraphQl.Administration;
using Eco.Echolon.ApiClient.Client.RestApi;

namespace Eco.Echolon.ApiClient.Client
{
    public class EcoApiClient : IApiClient
    {
        public IBaseClient BaseClient { get; }
        public IWorkingClient Working { get; }
        public IFormattedTextClient FormattedText { get; }
        public IFileClient File { get; }
        public ITextTemplatesClient TextTemplates { get; set; }
        public IConfigClient Configuration { get; }
        public ISystemClient System { get; }
        public IViewClient Views { get; }
        public IWorkingQueueClient WorkingQueue { get; }

        public EcoApiClient(IWorkingClient workingClient,
            IFormattedTextClient formattedText,
            IFileClient file,
            ITextTemplatesClient textTemplates,
            IConfigClient configuration,
            ISystemClient client,
            IViewClient views,
            IWorkingQueueClient workingQueue,
            IBaseClient baseClient)
        {
            Working = workingClient;
            FormattedText = formattedText;
            File = file;
            TextTemplates = textTemplates;
            Configuration = configuration;
            System = client;
            Views = views;
            WorkingQueue = workingQueue;
            BaseClient = baseClient;
        }
        
        
        public IVersionedAdministrationFor<TItem, TItemInput, TListItem> VersionedAdministration<TItem, TItemInput, TListItem>(string module) where TItem : class
        {
            return new BaseVersionedAdministration<TItem, TItemInput, TListItem>(module, BaseClient);
        }

        public IAdministration<TItem, TItemInput> Administration<TItem, TItemInput>(string module) where TItem : class
        {
            return new BaseAdministration<TItem, TItemInput>(module, BaseClient);
        }
    }
}