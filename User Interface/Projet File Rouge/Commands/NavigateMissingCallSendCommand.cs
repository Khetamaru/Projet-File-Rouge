using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Commands
{
    public class NavigateMissingCallSendCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        public NavigateMissingCallSendCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        public override void Execute(object parameter)
        {
            if (RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser)).UserLevel >= User.AccessLevel.User)
            {
                navigationStore.CurrentViewModel = new MissingCallSendViewModel(navigationStore, cacheStore);
            }
        }
    }
}
