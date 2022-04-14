using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private List<string> userNameList;

        public LoginViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            ConnectionCommand = new ConnectionCommand(navigationStore, cacheStore);
            userNameList = GetUserNames();
        }

        private List<string> GetUserNames()
        {
            List<string> nameList = new List<string>();
            List<User> users = RequestCenter.GetUsers();
            foreach(User user in users)
            {
                nameList.Add(user.Name);
            }
            return nameList;
        }

        public string SelectedItem { get { return UserNameList[0]; } }
        public List<string> UserNameList { get { return userNameList; } }
        public ConnectionCommand ConnectionCommand { get; }
    }
}
