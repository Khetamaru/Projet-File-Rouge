using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    public class UserCenterViewModel : ViewModelBase
    {
        private List<User> userList;
        private User actualUser;

        public UserCenterViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateParameterMenuCommand = new NavigateParameterMenuCommand(navigationStore, cacheStore);
            NavigateUserCommand = new NavigateUserCommand(navigationStore, cacheStore);

            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
            userList = RequestCenter.GetUsers();
        }

        public List<User> UserList
        {
            get { return userList; }
            set
            {
                userList = value;
                OnPropertyChanged(nameof(UserList));
            }
        }
        public User ActualUser => actualUser;

        public NavigateParameterMenuCommand NavigateParameterMenuCommand { get; }
        public NavigateUserCommand NavigateUserCommand { get; }
    }
}
