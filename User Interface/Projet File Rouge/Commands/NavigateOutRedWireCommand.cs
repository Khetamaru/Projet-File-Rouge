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

            else if (value == PageNameEnum.FreeFolder.ToString()) { /* LATER */ }

            else if(value == PageNameEnum.PersonalCenter.ToString()) { /* LATER */ }

            else if(value == PageNameEnum.ElderFolder.ToString()) { /* LATER */ }

            else { PopUpCenter.MessagePopup("Le codeur sait pas coder"); }
        }
    }
}
