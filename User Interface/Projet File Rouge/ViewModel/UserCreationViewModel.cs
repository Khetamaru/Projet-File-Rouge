using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// User Creation View
    /// </summary>
    public class UserCreationViewModel : ViewModelBase
    {
        private string userNameField;
        private List<string> accessLevelList;
        private int accessLevelField;
        private string adminPasswordField;

        private readonly User actualUser;

        public UserCreationViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // Set up command
            NavigateParameterMenuCommand = new NavigateParameterMenuCommand(navigationStore, cacheStore);

            // Set up view objets
            accessLevelList = new List<string>();
            foreach (string levelName in Enum.GetNames(typeof(User.AccessLevel)))
            {
                accessLevelList.Add(levelName);
            }
            accessLevelField = -1;

            // Set up BDD objects
            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
            adminPasswordField = string.Empty;
        }

        /// <summary>
        /// Launch user creation
        /// </summary>
        internal void UserCreation()
        {
            if (FieldCheck())
            {
                // Check permissions
                if (AccessLevelField == (int)User.AccessLevel.Admin)
                {
                    // user admin creation
                    RequestCenter.PostUser(new User(UserNameField, AdminPasswordField, (User.AccessLevel)AccessLevelField).JsonifyLogIn());
                }
                else
                {
                    // user creation
                    RequestCenter.PostUser(new User(UserNameField, string.Empty, (User.AccessLevel)AccessLevelField).JsonifyLogIn());
                }
                PopUpCenter.MessagePopup("L'utilisateur " + UserNameField + " a bien été crééer avec le niveau d'accès : " + ((User.AccessLevel)AccessLevelField).ToString());
                RequestCenter.PostLog(new Log("Création de l'utilisateur " + UserNameField + " => " + AccessLevelField, DateTime.Now, Log.LogTypeEnum.User, actualUser).Jsonify());
                NavigateParameterMenuCommand.Execute(null);
            }
            else
            {
                // error message
                PopUpCenter.MessagePopup("Veuillez remplir tous les champs.");
            }
        }

        /// <summary>
        /// Check if there if empty fields or errors
        /// </summary>
        private bool FieldCheck()
        {
            if (UserNameField == null || UserNameField == string.Empty)
            {
                return false;
            }
            if (UserNameField.Length > 45)
            {
                PopUpCenter.MessagePopup("Champ trop long (Taille Max 45)");
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

        /// <summary>
        /// Command
        /// </summary>
        public NavigateParameterMenuCommand NavigateParameterMenuCommand { get; }
    }
}
