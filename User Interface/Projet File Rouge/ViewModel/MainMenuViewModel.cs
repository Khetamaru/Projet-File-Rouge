using System;
using System.Collections.Generic;
using System.Configuration;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Main Menu View
    /// </summary>
    public class MainMenuViewModel : ViewModelBase
    {
        private string userName;
        private int notifNumber;
        private int missingCallNumber;

        public MainMenuViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            LogOutCommand = new LogOutCommand(navigationStore, cacheStore);
            NavigateMissingCallMenuCommand = new NavigateMissingCallMenuCommand(navigationStore, cacheStore);
            NavigateNotificationPageCommand = new NavigateNotificationPageCommand(navigationStore, cacheStore);
            NavigateGlobalCenterCommand = new NavigateGlobalCenterCommand(navigationStore, cacheStore);
            NavigateNewFileCommand = new NavigateNewFileCommand(navigationStore, cacheStore);
            NavigatePersoSpaceCommand = new NavigatePersoSpaceCommand(navigationStore, cacheStore);
            NavigateOldFolderCommand = new NavigateOldFolderCommand(navigationStore, cacheStore);
            NavigateFreeFolderCommand = new NavigateFreeFolderCommand(navigationStore, cacheStore);
            NavigateReturnFolderCommand = new NavigateReturnFolderCommand(navigationStore, cacheStore);
            NavigateCommandCenterCommand = new NavigateCommandCenterCommand(navigationStore, cacheStore);
            NavigateParameterMenuCommand = new NavigateParameterMenuCommand(navigationStore, cacheStore);

            // set up BDD objects
            User user = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
            userName = user.Name;
            notifNumber = cacheStore.GetObjectCache(ObjectCacheStoreEnum.notifNumber);
            missingCallNumber = cacheStore.GetObjectCache(ObjectCacheStoreEnum.missingCallNumber);

            // patch note
            string versionNumber = RequestCenter.GetVersion().Item1.VersionNumber;
            if (versionNumber != user.VersionSynced)
            {
                ShowPatchNote(user.VersionSynced, versionNumber);
                user.VersionSynced = versionNumber;
                RequestCenter.PutUser(user.Id, user.Jsonify());
            }
        }

        private void ShowPatchNote(string versionSynced, string versionNumber)
        {
            PopUpCenter.MessagePopup("PATCH NOTE\n\n" +
                                     "Ancienne Version : " + versionSynced + "\n" +
                                     "Nouvelle Version : " + versionNumber + "\n\n" +
                                     "Description : \n" + ConfigurationManager.AppSettings["patch_note"]);
        }

        public string UserName => userName;
        public int NotifNumber => notifNumber;
        public int MissingCallNumber => missingCallNumber;
        public bool NotificationVisibility => NotifNumber == 0;
        public string NotificationText => "Notifications (" + NotifNumber + ")";
        public string MissingCallText => MissingCallTrigger ? "Main Courante (" + MissingCallNumber + ")" : "Main Courante";
        public bool MissingCallTrigger => MissingCallNumber > 0;
        public bool MissingCallTriggerRevert => !MissingCallTrigger;

        /// <summary>
        ///  commands
        /// </summary>
        public LogOutCommand LogOutCommand { get; }
        public NavigateMissingCallMenuCommand NavigateMissingCallMenuCommand { get; }
        public NavigateNotificationPageCommand NavigateNotificationPageCommand { get; }
        public NavigateGlobalCenterCommand NavigateGlobalCenterCommand { get; }
        public NavigateNewFileCommand NavigateNewFileCommand { get; }
        public NavigatePersoSpaceCommand NavigatePersoSpaceCommand { get; }
        public NavigateOldFolderCommand NavigateOldFolderCommand { get; }
        public NavigateFreeFolderCommand NavigateFreeFolderCommand { get; }
        public NavigateCommandCenterCommand NavigateCommandCenterCommand { get; }
        public NavigateParameterMenuCommand NavigateParameterMenuCommand { get; }
        public NavigateReturnFolderCommand NavigateReturnFolderCommand { get; }
    }
}