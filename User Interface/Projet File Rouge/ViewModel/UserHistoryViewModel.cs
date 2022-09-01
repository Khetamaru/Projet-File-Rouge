using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Historic of users in a red wire
    /// </summary>
    public class UserHistoryViewModel : ViewModelBase
    {
        private readonly List<UserHistory> stackPanelContent;

        public UserHistoryViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // Set up command
            NavigateRedWireCommand = new NavigateRedWireCommand(navigationStore, cacheStore);

            // Set up stack panel
            stackPanelContent = RequestCenter.GetUserHistoryByRedWire(cacheStore.GetObjectCache(ObjectCacheStoreEnum.RedWireDetail));
        }

        public List<UserHistory> StackPanelContent => stackPanelContent;

        /// <summary>
        /// Command
        /// </summary>
        public NavigateRedWireCommand NavigateRedWireCommand { get; }
    }
}
