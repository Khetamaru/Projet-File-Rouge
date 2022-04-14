using Projet_File_Rouge.Stores;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Commands
{
    public class NavigateDetailCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        public NavigateDetailCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        public override void Execute(object parameter)
        {
            if (cacheStore.DoesCacheExist(CacheStoreEnum.UserDetail))
            {
                navigationStore.CurrentViewModel = new DetailViewModel(navigationStore, cacheStore);
            }
            else
            {
                // ERROR MESSAGE
            }
        }
    }
}
