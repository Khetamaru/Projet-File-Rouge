using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Commands
{
    class NavigateCommandCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        public NavigateCommandCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        /// <summary>
        /// Get command for cache
        /// </summary>
        /// <param name="commandList"></param>
        public void LoadCommand(CommandList commandList)
        {
            cacheStore.SetObjectCache(ObjectCacheStoreEnum.CommandListDetail, commandList.Id);
            Execute(new object());
        }

        public override void Execute(object parameter)
        {
            if (RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser)).UserLevel >= User.AccessLevel.SuperUser)
            {
                navigationStore.CurrentViewModel = new CommandViewModel(navigationStore, cacheStore);
                navigationStore.NavBarViewModel = new NavBarViewModel(navigationStore, cacheStore);
            }
        }
    }
}
