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
    class NavigateCommandCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        public NavigateCommandCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        public void LoadCommand(CommandList commandList)
        {
            cacheStore.SetObjectCache(ObjectCacheStoreEnum.CommandListDetail, commandList.Id);
            Execute(new object());
        }

        public override void Execute(object parameter)
        {
            navigationStore.CurrentViewModel = new CommandViewModel(navigationStore, cacheStore);
        }
    }
}
