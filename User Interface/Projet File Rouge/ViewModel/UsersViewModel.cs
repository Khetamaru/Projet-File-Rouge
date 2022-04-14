using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    class UsersViewModel : ViewModelBase
    {
        private List<User> users;
        private User userSelected;

        public UsersViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateDetailCommand = new NavigateDetailCommand(navigationStore, cacheStore);
            users = RequestCenter.GetUsers();
        }

        public List<User> Users
        {
            get => users;
        }

        public User UserSelected
        {
            get { return userSelected; }
            set
            {
                if (userSelected != value)
                {
                    userSelected = value;
                    NavigateDetailCommand.cacheStore.SetCache(CacheStoreEnum.UserDetail, userSelected);
                }
            }
        }

        public NavigateDetailCommand NavigateDetailCommand { get; }
    }
}