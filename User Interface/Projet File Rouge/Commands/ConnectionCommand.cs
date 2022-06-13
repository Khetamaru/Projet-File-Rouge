using System.Collections.Generic;
using Projet_File_Rouge.ExternalInfoFile;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Commands
{
    public class ConnectionCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        private string UserNameField;
        private string PasswordField;

        public ConnectionCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        public ConnectionCommand ChargeParameters(string userNameField, string passwordField)
        {
            UserNameField = userNameField;
            PasswordField = passwordField;

            return this;
        }

        public override void Execute(object parameter)
        {
            if (RequestCenter.SignInRequest(UserNameField, PasswordField))
            {
                LoginCacheFile.Write(UserNameField);
                cacheStore.SetObjectCache(ObjectCacheStoreEnum.ActualUser, RequestCenter.GetUserByName(UserNameField).Id);
                OutDatedNotif(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
                navigationStore.CurrentViewModel = new MainMenuViewModel(navigationStore, cacheStore);
            }
            else
            {
                PopUpCenter.MessagePopup("Mot de passe invalide");
            }
        }

        private void OutDatedNotif(int userId)
        {
            int Count = 0;
            User user = RequestCenter.GetUser(userId);

            if (user.UserLevel == User.AccessLevel.Admin)
            {
                Count += RequestCenter.GetRedWireNotifAdminNumber();
                Count += RequestCenter.GetCommandListNotifNumber();
                Count += RequestCenter.GetRedWirePurgeNumber();
            }
            else
            {
                Count += RequestCenter.GetRedWireNotifNumber(userId);
                if ((int)user.UserLevel >= (int)User.AccessLevel.SuperUser) { Count += RequestCenter.GetCommandListNotifNumber(); }
            }
            cacheStore.SetObjectCache(ObjectCacheStoreEnum.notifNumber, Count);
        }
    }
}
