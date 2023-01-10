using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Commands
{
    public class NavigateDocumentCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        public NavigateDocumentCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        /// <summary>
        /// Get sale document number for cache
        /// </summary>
        /// <param name="saleDocumentNumber"></param>
        public void SetSaleDocumentCacheInfo(string saleDocumentNumber, int DocumentListId)
        {
            cacheStore.SetInfoCache(InfoCacheStoreEnum.SaleDocumentDetail, saleDocumentNumber);
            cacheStore.SetObjectCache(ObjectCacheStoreEnum.DocumentListDetail, DocumentListId);
            navigationStore.NavBarViewModel = new NavBarViewModel(navigationStore, cacheStore);
        }

        public override void Execute(object parameter)
        {
            navigationStore.CurrentViewModel = new DocumentViewModel(navigationStore, cacheStore);
        }
    }
}
