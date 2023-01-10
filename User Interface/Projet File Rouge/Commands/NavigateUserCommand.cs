using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Commands
{
    public class NavigateUserCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        public NavigateUserCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        /// <summary>
        /// get user for cache
        /// </summary>
        /// <param name="user"></param>
        public void LoadUser(User user)
        {
            cacheStore.SetObjectCache(ObjectCacheStoreEnum.UserDetail, user.Id);
            Execute(new object());
        }

        public override void Execute(object parameter)
        {
            navigationStore.CurrentViewModel = new UserViewModel(navigationStore, cacheStore);
            navigationStore.NavBarViewModel = new NavBarViewModel(navigationStore, cacheStore);
        }
    }
}
