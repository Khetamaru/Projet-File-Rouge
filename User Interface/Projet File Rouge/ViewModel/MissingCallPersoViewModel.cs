using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;
using System.Collections.Generic;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Missing Call Perso View
    /// </summary>
    public class MissingCallPersoViewModel : ViewModelBase
    {
        private List<MissingCall> missingCallList;

        public MissingCallPersoViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            NavigateMissingCallMenuCommand = new NavigateMissingCallMenuCommand(navigationStore, cacheStore);
            NavigateMissingCallDetailsCommand = new NavigateMissingCallDetailsCommand(navigationStore, cacheStore);

            // set up BDD objects
            missingCallList = RequestCenter.GetMissingCallsByUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
        }

        public List<MissingCall> MissingCallList
        {
            get { return missingCallList; }
            set
            {
                missingCallList = value;
                OnPropertyChanged(nameof(missingCallList));
            }
        }

        /// <summary>
        ///  commands
        /// </summary>
        public NavigateMissingCallMenuCommand NavigateMissingCallMenuCommand { get; }
        public NavigateMissingCallDetailsCommand NavigateMissingCallDetailsCommand { get; }
    }
}