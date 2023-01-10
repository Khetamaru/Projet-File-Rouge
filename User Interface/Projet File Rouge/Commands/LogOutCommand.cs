using Projet_File_Rouge.Stores;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Commands
{
    public class LogOutCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        public LogOutCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        public override void Execute(object parameter)
        {
            cacheStore = new CacheStore(cacheStore.CloseEvent);
            navigationStore.CurrentViewModel = new LoginViewModel(navigationStore, cacheStore);
            navigationStore.NavBarViewModel = null;
        }
    }
}
