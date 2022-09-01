using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Stores
{
    /// <summary>
    /// Trigger class for view changement
    /// </summary>
    public class NavigationStore
    {
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
            CurrentViewModelChanged?.Invoke();
        }
    }
}
