using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private User user;
        private User actualUser;
        private List<string> accessLevelList;
        private int accessLevelField;

        private bool changeNamePopUpIsOpen;
        private bool resetPasswordPopUpIsOpen;
        private bool changeAccessLevelPopUpIsOpen;
        private bool desablePopUpIsOpen;
        private bool unablePopUpIsOpen;

        public UserViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateUserCenterCommand = new NavigateUserCenterCommand(navigationStore, cacheStore);
            LogOutCommand = new LogOutCommand(navigationStore, cacheStore);

            user = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.UserDetail));
            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));

            accessLevelList = new List<string>();
            foreach (string levelName in Enum.GetNames(typeof(User.AccessLevel)))
            {
                accessLevelList.Add(levelName);
            }
            accessLevelField = (int)user.UserLevel;
        }

        public void MajUI()
        {
            OnPropertyChanged(nameof(User));
            OnPropertyChanged(nameof(DesableButtonVisibility));
            OnPropertyChanged(nameof(UnableButtonVisibility));
        }

        public void MajUser()
        {
            RequestCenter.PutUser(User.Id, User.JsonifyId());
        }

        public User User
        {
            get => user;
            set => user = value;
        }
        public User ActualUser { get => actualUser; }

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
            }
        }

        public bool ListVisibility
        {
            get => AccessLevelField >= 0;
        }

        public bool ChangeNamePopUpIsOpen
        {
            get => changeNamePopUpIsOpen;
            set
            {
                changeNamePopUpIsOpen = value;
                OnPropertyChanged(nameof(ChangeNamePopUpIsOpen));
            }
        }
        public void OpenChangeNamePopUp() => ChangeNamePopUpIsOpen = true;
        public void CloseChangeNamePopUp() => ChangeNamePopUpIsOpen = false;
        public string changeNameField;
        public string ChangeNameField { get => changeNameField; set { changeNameField = value; OnPropertyChanged(nameof(changeNameField)); } }
        public void ChangeNameYesButton()
        {
            RequestCenter.PostLog(new Log("Changement de nom d'utilisateur " + ChangeNameField, DateTime.Now, Log.LogTypeEnum.User, ActualUser).Jsonify());
            User.Name = ChangeNameField;
            MajUI();
            MajUser();
            CloseChangeNamePopUp();
        }
        public void ChangeNameNoButton()
        {
            CloseChangeNamePopUp();
        }

        public bool ResetPasswordPopUpIsOpen
        {
            get => resetPasswordPopUpIsOpen;
            set
            {
                resetPasswordPopUpIsOpen = value;
                OnPropertyChanged(nameof(ResetPasswordPopUpIsOpen));
            }
        }
        public void OpenResetPasswordPopUp() => ResetPasswordPopUpIsOpen = true;
        public void CloseResetPasswordPopUp() => ResetPasswordPopUpIsOpen = false;
        public void ResetPasswordYesButton()
        {
            RequestCenter.PostLog(new Log("Mot de passe d'utilisateur " + User.Name + " remis à zéro", DateTime.Now, Log.LogTypeEnum.User, ActualUser).Jsonify());
            User.Password = SHA256Cypher.Cyphing("");
            MajUI();
            MajUser();
            CloseResetPasswordPopUp();
        }
        public void ResetPasswordNoButton()
        {
            CloseResetPasswordPopUp();
        }
        
        public bool ChangeAccessLevelPopUpIsOpen
        {
            get => changeAccessLevelPopUpIsOpen;
            set
            {
                changeAccessLevelPopUpIsOpen = value;
                OnPropertyChanged(nameof(ChangeAccessLevelPopUpIsOpen));
            }
        }
        public void OpenChangeAccessLevelPopUp() => ChangeAccessLevelPopUpIsOpen = true;
        public void CloseChangeAccessLevelPopUp() => ChangeAccessLevelPopUpIsOpen = false;
        public int changeAccessLevelField;
        public int ChangeAccessLevelField { get => changeAccessLevelField; set { changeAccessLevelField = value; OnPropertyChanged(nameof(ChangeAccessLevelField)); } }
        public void ChangeAccessLevelYesButton()
        {
            RequestCenter.PostLog(new Log("Niveau d'accès changer " + User.Name + " : " + User.UserLevel + " => " + (User.AccessLevel)ChangeAccessLevelField, DateTime.Now, Log.LogTypeEnum.User, ActualUser).Jsonify());
            User.UserLevel = (User.AccessLevel)ChangeAccessLevelField;
            MajUI();
            MajUser();
            CloseChangeAccessLevelPopUp();
            if (User.Id == ActualUser.Id) { if (User.UserLevel != User.AccessLevel.Admin) { LogOutCommand.Execute(null); } }
            else if (User.UserLevel == User.AccessLevel.Admin) { NavigateUserCenterCommand.Execute(null); }
        }
        public void ChangeAccessLevelNoButton()
        {
            CloseChangeAccessLevelPopUp();
        }

        public bool DesableButtonVisibility { get => User.Activated != true; }
        public bool DesablePopUpIsOpen
        {
            get => desablePopUpIsOpen;
            set
            {
                desablePopUpIsOpen = value;
                OnPropertyChanged(nameof(DesablePopUpIsOpen));
            }
        }
        public void OpenDesablePopUp() => DesablePopUpIsOpen = true;
        public void CloseDesablePopUp() => DesablePopUpIsOpen = false;
        public void DesableYesButton()
        {
            if (PopUpCenter.ActionValidPopup("Êtes-vous sûr de vouloir désactiver l'utilisateur ?\nL'utilisateur ne pourra plus se connecter avec si il n'est pas réactivé plus tard."))
            {
                RequestCenter.PostLog(new Log("Désactivation d'utilisateur " + User.Name, DateTime.Now, Log.LogTypeEnum.User, ActualUser).Jsonify());
                User.Activated = false;
                User.UserLevel = User.AccessLevel.Intern;
                MajUI();
                MajUser();
                if (User.Id == ActualUser.Id && User.UserLevel != User.AccessLevel.Admin) { LogOutCommand.Execute(null); }
            }
            CloseDesablePopUp();
        }
        public void DesableNoButton()
        {
            CloseDesablePopUp();
        }

        public bool UnableButtonVisibility { get => User.Activated != false; }
        public bool UnablePopUpIsOpen
        {
            get => unablePopUpIsOpen;
            set
            {
                unablePopUpIsOpen = value;
                OnPropertyChanged(nameof(UnablePopUpIsOpen));
            }
        }
        public void OpenUnablePopUp() => UnablePopUpIsOpen = true;
        public void CloseUnablePopUp() => UnablePopUpIsOpen = false;
        public void UnableYesButton()
        {
            RequestCenter.PostLog(new Log("Réactivation d'utilisateur " + User.Name, DateTime.Now, Log.LogTypeEnum.User, ActualUser).Jsonify());
            User.Activated = true;
            MajUI();
            MajUser();
            CloseUnablePopUp();
        }
        public void UnableNoButton()
        {
            CloseUnablePopUp();
        }

        public NavigateUserCenterCommand NavigateUserCenterCommand { get; }
        public LogOutCommand LogOutCommand { get; }
    }
}
