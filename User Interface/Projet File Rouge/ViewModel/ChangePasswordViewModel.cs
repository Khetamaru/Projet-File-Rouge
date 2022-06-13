using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    public class ChangePasswordViewModel : ViewModelBase
    {
        private User actualUser;

        private string oldPassword;
        private string newPassword;
        private string newPasswordConfirm;

        public ChangePasswordViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateParameterMenuCommand = new NavigateParameterMenuCommand(navigationStore, cacheStore);

            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
        }

        public void ChangePassword()
        {
            if (ActualUser.Password == SHA256Cypher.Cyphing(OldPassword))
            {
                if (OldPassword != NewPassword)
                {
                    if (NewPassword == NewPasswordConfirm)
                    {
                        ActualUser.Password = SHA256Cypher.Cyphing(NewPassword);
                        RequestCenter.PutUser(ActualUser.Id, ActualUser.JsonifyId());
                        PopUpCenter.MessagePopup("Mot de passe correctement modifié.");
                        NavigateParameterMenuCommand.Execute(null);
                    }
                    else { PopUpCenter.MessagePopup("Nouveau mot de passe et confirmation différentes."); }
                }
                else { PopUpCenter.MessagePopup("Pas de différence entre le nouveau et l'ancien mot de passe."); }
            }
            else { PopUpCenter.MessagePopup("Mot de passe incorrecte."); }
        }

        public User ActualUser
        {
            get => actualUser;
            set => actualUser = value;
        }

        public string OldPassword
        {
            get => oldPassword;
            set => oldPassword = value;
        }

        public string NewPassword
        {
            get => newPassword;
            set => newPassword = value;
        }

        public string NewPasswordConfirm
        {
            get => newPasswordConfirm;
            set => newPasswordConfirm = value;
        }

        public NavigateParameterMenuCommand NavigateParameterMenuCommand { get; }
    }
}
