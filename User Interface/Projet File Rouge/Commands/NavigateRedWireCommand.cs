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
        private readonly NavigationStore navigationStore;
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
            Execute(new object());
        }

        public override void Execute(object parameter)
        {
            navigationStore.CurrentViewModel = new RedWireViewModel(navigationStore, cacheStore);
        }
    }
}
