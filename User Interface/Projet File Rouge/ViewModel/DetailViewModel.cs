using System.Windows.Input;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;

namespace Projet_File_Rouge.ViewModel
{
    public class DetailViewModel : ViewModelBase
    {
        private User actualUser;

        public DetailViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateDetailCommand = new NavigateDetailCommand(navigationStore, cacheStore);
            actualUser = cacheStore.GetCache(CacheStoreEnum.UserDetail) as User;
        }

        public User ActualUser
        {
            get => actualUser;
            set
            {
                actualUser = value;
            }
        }

        public ICommand NavigateDetailCommand { get; }
    }
}
