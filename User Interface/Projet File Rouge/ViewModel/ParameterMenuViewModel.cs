using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Stores;

namespace Projet_File_Rouge.ViewModel
{
    public class ParameterMenuViewModel : ViewModelBase
    {

        public ParameterMenuViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);
        }

        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
    }
}
