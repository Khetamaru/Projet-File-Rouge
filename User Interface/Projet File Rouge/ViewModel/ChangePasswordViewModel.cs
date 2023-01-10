using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Password Changing View
    /// </summary>
    public class ChangePasswordViewModel : ViewModelBase
    {
        private User actualUser;

        private string oldPassword;
        private string newPassword;
        private string newPasswordConfirm;

        public ChangePasswordViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            NavigateParameterMenuCommand = new NavigateParameterMenuCommand(navigationStore, cacheStore);

            // set up BDD objects
            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));

            // set up view objects
            oldPassword = newPassword = newPasswordConfirm = string.Empty;
        }

        /// <summary>
        /// Launch Password Modification
        /// </summary>
        public void ChangePassword()
        {
            // test if saved password and "old" one are equals
            if (ActualUser.Password == SHA256Cypher.Cyphing(OldPassword))
            {
                // test if "old" and "new" are differents
                if (OldPassword != NewPassword)
                {
                    // test if "new" and "confirmed" passwords are equals
                    if (NewPassword == NewPasswordConfirm)
                    {
                        if (NewPassword.Length < 120)
                        {
                            RequestCenter.PostLog(new Log("Changement de mot de passe utilisateur " + ActualUser.Name, DateTime.Now, Log.LogTypeEnum.User, ActualUser).Jsonify());
                            ActualUser.Password = SHA256Cypher.Cyphing(NewPassword);
                            RequestCenter.PutUser(ActualUser.Id, ActualUser.Jsonify());
                            PopUpCenter.MessagePopup("Mot de passe correctement modifié.");
                            NavigateParameterMenuCommand.Execute(null);
                        }
                        else { PopUpCenter.MessagePopup("Nouveau mot de passe trop long (Taille Max 120)"); }
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
            set 
            {
                oldPassword = value;
                OnPropertyChanged(nameof(OldPasswordVisibility));
            }
        }
        public bool OldPasswordVisibility => OldPassword != null && OldPassword.Length > 0;

        public string NewPassword
        {
            get => newPassword;
            set
            {
                newPassword = value;
                OnPropertyChanged(nameof(NewPasswordVisibility));
            }
        }
        public bool NewPasswordVisibility => NewPassword != null && NewPassword.Length > 0;

        public string NewPasswordConfirm
        {
            get => newPasswordConfirm;
            set
            {
                newPasswordConfirm = value;
                OnPropertyChanged(nameof(NewPasswordConfirmVisibility));
            }
        }
        public bool NewPasswordConfirmVisibility => NewPasswordConfirm != null && NewPasswordConfirm.Length > 0;

        /// <summary>
        /// Commands
        /// </summary>
        public NavigateParameterMenuCommand NavigateParameterMenuCommand { get; }
    }
}
