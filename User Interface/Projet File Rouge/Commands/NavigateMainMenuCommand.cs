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
            User user = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
            OutDatedNotif(user);
            MissingCallNumber(user);
            navigationStore.CurrentViewModel = new MainMenuViewModel(navigationStore, cacheStore);
        }

        /// <summary>
        /// Checkout for notifs and switch page
        /// </summary>
        /// <param name="user"></param>
        private void OutDatedNotif(User user)
        {
            int Count = 0;

            if (user.UserLevel == User.AccessLevel.Admin)
            {
                Count += RequestCenter.GetRedWireNotifAdminNumber();
                Count += RequestCenter.GetCommandListNotifNumber();
                Count += RequestCenter.GetRedWirePurgeNumber();
            }
            else
            {
                Count += RequestCenter.GetRedWireNotifNumber(user.Id);
                if ((int)user.UserLevel >= (int)User.AccessLevel.SuperUser) { Count += RequestCenter.GetCommandListNotifNumber(); }
            }
            Count += RequestCenter.GetMissingCallUnreadNumber(user.Id);

            cacheStore.SetObjectCache(ObjectCacheStoreEnum.notifNumber, Count);
        }

        /// <summary>
        /// Checkout for missingCalls
        /// </summary>
        /// <param name="user"></param>
        private void MissingCallNumber(User user)
        {
            cacheStore.SetObjectCache(ObjectCacheStoreEnum.missingCallNumber, RequestCenter.GetMissingCallsByUser(user.Id).Count);
        }
    }
}
