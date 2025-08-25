using Eco.Echolon.ApiClient.Client.GraphQl;
using Eco.Echolon.ApiClient.Client.GraphQl.Administration;
using Eco.Echolon.ApiClient.Client.RestApi;

namespace Eco.Echolon.ApiClient.Client
{
    public interface IApiClient
    {
        public IBaseClient BaseClient { get; }
        public IWorkingClient Working { get; }
        public IFormattedTextClient FormattedText { get; }
        public IFileClient File { get; }
        public ITextTemplatesClient TextTemplates { set; }
        public IConfigClient Configuration { get; }
        public ISystemClient System { get; }
        public IViewClient Views { get; }
        public IWorkingQueueClient WorkingQueue { get; }

        public IVersionedAdministrationFor<TItem, TItemInput, TListItem>
            VersionedAdministration<TItem, TItemInput, TListItem>(string module) where TItem : class;
        
        public IAdministration<TItem, TItemInput> Administration<TItem, TItemInput>(string module) where TItem : class;
    }
}