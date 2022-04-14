using Projet_File_Rouge.Stores;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Commands
{
    public class ConnectionCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        public ConnectionCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        public override void Execute(object parameter)
        {
            navigationStore.CurrentViewModel = new MainMenuViewModel(navigationStore, cacheStore);
        }
    }
}
