using System;
using System.Collections.Generic;
using System.Configuration;
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

        private readonly Action CloseEvent;

        public LoginViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            CloseEvent = cacheStore.CloseEvent;
            IsProgramUpToDate();

            connectionCommand = new ConnectionCommand(navigationStore, cacheStore);

            userNameList = GetUserNames();
            loginCacheString = LoginCacheFile.Read();
            passwordField = string.Empty;
            if (UserNameList.Count > 0) { UserNameField = UserNameList.Exists(x => x.Contains(loginCacheString)) ? loginCacheString : userNameList[0]; }

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

        public void IsProgramUpToDate()
        {
            string lvn = ConfigurationManager.AppSettings["version"];
            Object.Version svn = RequestCenter.GetVersion();

            if (string.Compare(lvn, svn.VersionNumber) < 0)
            {
                if (DateTime.Now.AddDays(-3) < svn.Date)
                {
                    PopUpCenter.MessagePopup("Votre version du logiciel n'est plus à jour.\nVous avez jusqu'à 3 jours après le " + svn.Date.ToString("dd'-'mm'-'yy' 'HH'H'mm") + "\n\nVersion actuelle : " + lvn + "\nVersion nécéssaire : " + svn.VersionNumber);
                }
                else
                {
                    PopUpCenter.MessagePopup("Votre version du logiciel n'est plus à jour.\nVeuillez contacter votre technicien pour mise à jour.\n\nVersion actuelle : " + lvn + "\nVersion nécéssaire : " + svn.VersionNumber);
                    CloseEvent.Invoke();
                }
            }
            if (string.Compare(lvn, svn.VersionNumber) > 0)
            {
                RequestCenter.PostVersion(new Object.Version(lvn, DateTime.Now).Jsonify());
            }
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
