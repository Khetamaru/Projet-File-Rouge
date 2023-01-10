using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Commands
{
    public class NavigateDocumentFTPCenterCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        public NavigateDocumentFTPCenterCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        public override void Execute(object parameter)
        {
            navigationStore.CurrentViewModel = new DocumentFTPCenterViewModel(navigationStore, cacheStore);
            navigationStore.NavBarViewModel = new NavBarViewModel(navigationStore, cacheStore);
        }
    }
}
