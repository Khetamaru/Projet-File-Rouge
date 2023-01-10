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
    public class NavigateRedWireCommand : CommandBase
    {
        public NavigationStore navigationStore;
        public CacheStore cacheStore;

        public NavigateRedWireCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        public void LoadRedWire(RedWire redWire, PageNameEnum pageName)
        {
            cacheStore.SetObjectCache(ObjectCacheStoreEnum.RedWireDetail, redWire.Id);
            cacheStore.SetInfoCache(InfoCacheStoreEnum.PreviousPageRedWire, pageName.ToString());
        }

        public override void Execute(object parameter)
        {
            navigationStore.CurrentViewModel = new RedWireViewModel(navigationStore, cacheStore);
            navigationStore.NavBarViewModel = new NavBarViewModel(navigationStore, cacheStore);
        }
    }
}
