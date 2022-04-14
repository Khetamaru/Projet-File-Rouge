using System;
using System.Collections.Generic;
using Projet_File_Rouge.Stores;

namespace Projet_File_Rouge.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore navigationStore;

        public ViewModelBase CurrentViewModel => navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore _navigationStore)
        {
            navigationStore = _navigationStore;

            navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}