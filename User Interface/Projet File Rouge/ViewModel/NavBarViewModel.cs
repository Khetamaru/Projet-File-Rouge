using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Main Menu View
    /// </summary>
    public class NavBarViewModel : ViewModelBase
    {

        public NavBarViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
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
        }

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