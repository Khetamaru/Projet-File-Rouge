using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// View to see and manage user infos
    /// </summary>
    public class MissingCallSendDetailsViewModel : ViewModelBase
    {
        private MissingCall missingCall;
        private User actualUser;

        public MissingCallSendDetailsViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // Set up commands
            NavigateMissingCallSendCommand = new NavigateMissingCallSendCommand(navigationStore, cacheStore);

            // Set up BDD Objects
            missingCall = RequestCenter.GetMissingCall(cacheStore.GetObjectCache(ObjectCacheStoreEnum.MissingCallDetail));
            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
        }

        public MissingCall MissingCall
        {
            get => missingCall;
            set => missingCall = value;
        }
        public User ActualUser { get => actualUser; }

        /// <summary>
        /// Commands
        /// </summary>
        public NavigateMissingCallSendCommand NavigateMissingCallSendCommand { get; }
    }
}
