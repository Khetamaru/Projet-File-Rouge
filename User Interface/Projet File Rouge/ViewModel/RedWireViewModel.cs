using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;

namespace Projet_File_Rouge.ViewModel
{
    class RedWireViewModel : ViewModelBase
    {
        private RedWire redWire;

        public RedWireViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);
            redWire = cacheStore.GetCache(CacheStoreEnum.RedWireDetail) as RedWire;
        }

        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
    }
}
