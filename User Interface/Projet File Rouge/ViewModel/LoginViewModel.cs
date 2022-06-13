using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.ExternalInfoFile;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        ConnectionCommand connectionCommand;
        private List<string> userNameList;

        private string userNameField;
        private string passwordField;
        private string loginCacheString;

        public LoginViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            connectionCommand = new ConnectionCommand(navigationStore, cacheStore);

            userNameList = GetUserNames();
            loginCacheString = LoginCacheFile.Read();
            passwordField = string.Empty;
            UserNameField = UserNameList.Exists(x => x.Contains(loginCacheString)) ? loginCacheString : userNameList[0];
        }

        private List<string> GetUserNames()
        {
            List<string> nameList = new List<string>();
            List<User> users = RequestCenter.GetActiveUsers();
            foreach(User user in users)
            {
                nameList.Add(user.Name);
            }
            return nameList;
        }

        public string UserNameField 
        { 
            get { return userNameField; }
            set 
            { 
                userNameField = value;
                connectionCommand = ConnectionCommand;
            }
        }

        public string PasswordField
        {
            get { return passwordField; }
            set 
            { 
                passwordField = value;
                connectionCommand = ConnectionCommand;
            }
        }

        public List<string> UserNameList { get { return userNameList; } }
        public ConnectionCommand ConnectionCommand { get { return connectionCommand.ChargeParameters(UserNameField, PasswordField); } }
    }
}
