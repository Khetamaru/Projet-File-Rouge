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
    public class NavigateOutRedWireCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        public NavigateOutRedWireCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        /// <summary>
        /// Get Redwire for cache
        /// </summary>
        /// <param name="redWire"></param>
        /// <param name="pageName"></param>
        public void LoadRedWire(RedWire redWire, PageNameEnum pageName)
        {
            cacheStore.SetObjectCache(ObjectCacheStoreEnum.RedWireDetail, redWire.Id);
            cacheStore.SetInfoCache(InfoCacheStoreEnum.PreviousPageRedWire, pageName.ToString());
            Execute(new object());
        }

        public override void Execute(object parameter)
        {
            string value = cacheStore.GetInfoCache(InfoCacheStoreEnum.PreviousPageRedWire);

            if (value == PageNameEnum.GlobalCenter.ToString()) { navigationStore.CurrentViewModel = new GlobalCenterViewModel(navigationStore, cacheStore); }

            else if (value == PageNameEnum.FreeFolder.ToString()) { navigationStore.CurrentViewModel = new FreeFolderViewModel(navigationStore, cacheStore); }

            else if (value == PageNameEnum.PersoSpace.ToString()) { navigationStore.CurrentViewModel = new PersoSpaceViewModel(navigationStore, cacheStore); }

            else if (value == PageNameEnum.OldFolder.ToString()) { navigationStore.CurrentViewModel = new OldFolderViewModel(navigationStore, cacheStore); }

            else if (value == PageNameEnum.CommandView.ToString()) { navigationStore.CurrentViewModel = new CommandViewModel(navigationStore, cacheStore); }

            else { PopUpCenter.MessagePopup("Le codeur sait pas coder"); }
        }
    }
}
