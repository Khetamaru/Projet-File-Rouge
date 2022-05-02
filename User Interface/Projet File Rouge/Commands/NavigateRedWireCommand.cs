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
    class NavigateRedWireCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        public NavigateRedWireCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        public void LoadRedWire(RedWire redWire)
        {
            cacheStore.SetCache(CacheStoreEnum.RedWireDetail, redWire);
            Execute(new object());
        }

        public override void Execute(object parameter)
        {
            navigationStore.CurrentViewModel = new RedWireViewModel(navigationStore, cacheStore);
        }
    }
}
