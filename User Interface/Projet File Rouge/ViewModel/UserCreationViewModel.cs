using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;

namespace Projet_File_Rouge.ViewModel
{
    public class UserCreationViewModel : ViewModelBase
    {
        private string userNameField;
        private List<string> accessLevelList;
        private string accessLevelField;

        public UserCreationViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);

            accessLevelList = new List<string>();
            foreach (string levelName in Enum.GetNames(typeof(User.AccessLevel)))
            {
                accessLevelList.Add(levelName);
            }
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
        public string AccessLevelField
        {
            get => accessLevelField;
            set
            {
                accessLevelField = value;
                OnPropertyChanged(nameof(AccessLevelField));
            }
        }

        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
    }
}
