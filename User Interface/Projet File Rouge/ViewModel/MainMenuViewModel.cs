using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    public class MainMenuViewModel : ViewModelBase
    {
        private string userName;

        public MainMenuViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            LogOutCommand = new LogOutCommand(navigationStore, cacheStore);
            NavigateGlobalCenterCommand = new NavigateGlobalCenterCommand(navigationStore, cacheStore);
            NavigateNewFileCommand = new NavigateNewFileCommand(navigationStore, cacheStore);

            userName = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser)).Name;
        }

        public string UserName
        {
            get { return userName; }
        }

        public LogOutCommand LogOutCommand { get; }
        public NavigateGlobalCenterCommand NavigateGlobalCenterCommand { get; }
        public NavigateNewFileCommand NavigateNewFileCommand { get; }
    }
}
