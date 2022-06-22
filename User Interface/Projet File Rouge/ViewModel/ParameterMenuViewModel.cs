using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Stores;

namespace Projet_File_Rouge.ViewModel
{
    public class ParameterMenuViewModel : ViewModelBase
    {

        public ParameterMenuViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);
            NavigateUserCreationCommand = new NavigateUserCreationCommand(navigationStore, cacheStore);
            NavigateUserCenterCommand = new NavigateUserCenterCommand(navigationStore, cacheStore);
            NavigateChangePasswordCommand = new NavigateChangePasswordCommand(navigationStore, cacheStore);
            NavigateFolderPurgeCommand = new NavigateFolderPurgeCommand(navigationStore, cacheStore);
            NavigateLogCenterCommand = new NavigateLogCenterCommand(navigationStore, cacheStore);
        }

        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
        public NavigateUserCreationCommand NavigateUserCreationCommand { get; }
        public NavigateUserCenterCommand NavigateUserCenterCommand { get; }
        public NavigateChangePasswordCommand NavigateChangePasswordCommand { get; }
        public NavigateFolderPurgeCommand NavigateFolderPurgeCommand { get; }
        public NavigateLogCenterCommand NavigateLogCenterCommand { get; }
    }
}
