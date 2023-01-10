using System;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Stores
{
    /// <summary>
    /// Trigger class for view changement
    /// </summary>
    public class NavigationStore
    {
        public event Action NavBarViewModelChanged;
        private ViewModelBase navBarViewModel;
        public ViewModelBase NavBarViewModel
        {

            get => navBarViewModel;
            set
            {
                navBarViewModel = value;
                OnCurrentViewModelChanged();
            }
        }
        public event Action CurrentViewModelChanged;
        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel {

            get => currentViewModel;
            set
            {
                currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            NavBarViewModelChanged?.Invoke();
            CurrentViewModelChanged?.Invoke();
        }
    }
}
