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
    public class MissingCallDetailsViewModel : ViewModelBase
    {
        private MissingCall missingCall;
        private User actualUser;

        public MissingCallDetailsViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // Set up commands
            NavigateMissingCallPersoCommand = new NavigateMissingCallPersoCommand(navigationStore, cacheStore);

            // Set up BDD Objects
            missingCall = RequestCenter.GetMissingCall(cacheStore.GetObjectCache(ObjectCacheStoreEnum.MissingCallDetail));
            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));

            ViewingTrigger();
        }

        private void ViewingTrigger()
        {
            if (!MissingCall.Read)
            {
                MissingCall.Read = true;
                RequestCenter.PutMissingCall(MissingCall.Id, MissingCall.JsonifyId());
            }
        }

        internal void DeleteMissingCall()
        {
            if (PopUpCenter.ActionValidPopup("Êtes-vous sûr ?"))
            {
                RequestCenter.DeleteMissingCall(MissingCall.Id);
                NavigateMissingCallPersoCommand.Execute(null);
            }
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
        public NavigateMissingCallPersoCommand NavigateMissingCallPersoCommand { get; }
    }
}
