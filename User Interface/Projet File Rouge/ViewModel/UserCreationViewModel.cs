using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    public class UserCreationViewModel : ViewModelBase
    {
        private string userNameField;
        private List<string> accessLevelList;
        private int accessLevelField;
        private string adminPasswordField;

        private readonly User actualUser;

        public UserCreationViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateParameterMenuCommand = new NavigateParameterMenuCommand(navigationStore, cacheStore);

            accessLevelList = new List<string>();
            foreach (string levelName in Enum.GetNames(typeof(User.AccessLevel)))
            {
                accessLevelList.Add(levelName);
            }
            accessLevelField = -1;

            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
            adminPasswordField = string.Empty;
        }

        internal void UserCreation()
        {
            if (FieldCheck())
            {
                if (AccessLevelField == (int)User.AccessLevel.Admin)
                {
                    RequestCenter.PostUser(new User(UserNameField, AdminPasswordField, (User.AccessLevel)AccessLevelField).JsonifyLogIn());
                }
                else
                {
                    RequestCenter.PostUser(new User(UserNameField, string.Empty, (User.AccessLevel)AccessLevelField).JsonifyLogIn());
                }
                PopUpCenter.MessagePopup("L'utilisateur " + UserNameField + " a bien été crééer avec le niveau d'accès : " + ((User.AccessLevel)AccessLevelField).ToString());
                RequestCenter.PostLog(new Log("Création de l'utilisateur " + UserNameField + " => " + AccessLevelField, DateTime.Now, Log.LogTypeEnum.User, actualUser).Jsonify());
                NavigateParameterMenuCommand.Execute(null);
            }
            else
            {
                PopUpCenter.MessagePopup("Veuillez remplir tous les champs.");
            }
        }

        private bool FieldCheck()
        {
            if (UserNameField == null || UserNameField == string.Empty)
            {
                return false;
            }
            if (AccessLevelField < 0)
            {
                return false;
            }
            if (AccessLevelField == (int)User.AccessLevel.Admin && (AdminPasswordField == null || AdminPasswordField == string.Empty))
            {
                return false;
            } 
            return true;
        }

        public string UserNameField
        {
            get => userNameField;
            set
            {
                userNameField = value;
                OnPropertyChanged(nameof(UserNameField));
            }
        }

        public List<string> AccessLevelList
        {
            get => accessLevelList;
        }
        public int AccessLevelField
        {
            get => accessLevelField;
            set
            {
                accessLevelField = value;
                OnPropertyChanged(nameof(AccessLevelField));
                OnPropertyChanged(nameof(ListVisibility));
                OnPropertyChanged(nameof(AdminVisibility));
            }
        }

        public string AdminPasswordField
        {
            get => adminPasswordField;
            set
            {
                adminPasswordField = value;
                OnPropertyChanged(nameof(AdminPasswordField));
            }
        }
        public bool AdminVisibility => AccessLevelField != (int)User.AccessLevel.Admin;

        public bool ListVisibility
        {
            get => AccessLevelField >= 0;
        }

        public NavigateParameterMenuCommand NavigateParameterMenuCommand { get; }
    }
}
