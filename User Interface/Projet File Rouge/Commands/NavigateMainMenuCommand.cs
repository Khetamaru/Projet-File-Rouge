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
    public class NavigateMainMenuCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        public NavigateMainMenuCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        public override void Execute(object parameter)
        {
            OutDatedNotif(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
            navigationStore.CurrentViewModel = new MainMenuViewModel(navigationStore, cacheStore);
        }

        private void OutDatedNotif(int userId)
        {
            int redWiresNotifNumber = RequestCenter.GetRedWireNotifNumber(userId);
            User user = RequestCenter.GetUser(userId);

            int Count = redWiresNotifNumber;
            if ((int)user.UserLevel >= (int)User.AccessLevel.SuperUser) { Count += RequestCenter.GetCommandListNotifNumber(); }

            cacheStore.SetObjectCache(ObjectCacheStoreEnum.notifNumber, Count);
        }
    }
}
