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
    /// <summary>
    /// Login View
    /// </summary>
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
            // set up commands
            connectionCommand = new ConnectionCommand(navigationStore, cacheStore);

            // set up view objects
            CloseEvent = cacheStore.CloseEvent;
            IsProgramUpToDate();
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
            string lvn = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;
            lvn = lvn.Remove(lvn.Length - 2, 2);

            bool urgent = Boolean.Parse(ConfigurationManager.AppSettings["urgent"]);

            Object.Version svn = RequestCenter.GetVersion();

            if (string.Compare(lvn, svn.VersionNumber) < 0)
            {
                if (svn.Ugent)
                {
                    PopUpCenter.MessagePopup("Mise à jour urgente en attente.\nVeuillez contacter votre technicien pour mise à jour.\n\nVersion actuelle : " + lvn + "\nVersion nécéssaire : " + svn.VersionNumber);
                    CloseEvent.Invoke();
                }
                else if (DateTime.Now.AddDays(-3) < svn.Date)
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
                RequestCenter.PostVersion(new Object.Version(lvn, DateTime.Now, urgent).Jsonify());
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
                OnPropertyChanged(nameof(PasswordFieldVisibility));
            }
        }
        public bool PasswordFieldVisibility { get => PasswordField.Length > 0; }

        /// <summary>
        /// Commands
        /// </summary>
        public List<string> UserNameList { get { return userNameList; } }
        public ConnectionCommand ConnectionCommand { get { return connectionCommand.ChargeParameters(UserNameField, PasswordField); } }
    }
}
