using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Stores;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Missing Call Menu View
    /// </summary>
    public class MissingCallMenuViewModel : ViewModelBase
    {
        public MissingCallMenuViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);
            NavigateMissingCallPersoCommand = new NavigateMissingCallPersoCommand(navigationStore, cacheStore);
            NavigateMissingCallCreationCommand = new NavigateMissingCallCreationCommand(navigationStore, cacheStore);
        }

        /// <summary>
        ///  commands
        /// </summary>
        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
        public NavigateMissingCallPersoCommand NavigateMissingCallPersoCommand { get; }
        public NavigateMissingCallCreationCommand NavigateMissingCallCreationCommand { get; }
    }
}