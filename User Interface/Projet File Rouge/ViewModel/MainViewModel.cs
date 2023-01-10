using System;
using System.Collections.Generic;
using Projet_File_Rouge.Stores;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Base View
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore navigationStore;

        public ViewModelBase NavBar => navigationStore.NavBarViewModel;
        public ViewModelBase CurrentViewModel => navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore _navigationStore)
        {
            navigationStore = _navigationStore;

            navigationStore.NavBarViewModelChanged += OnCurrentViewModelChanged;
            navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(NavBar));
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}