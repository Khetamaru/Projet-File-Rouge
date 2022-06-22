using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    public class UserHistoryViewModel : ViewModelBase
    {
        private readonly List<UserHistory> stackPanelContent;

        public UserHistoryViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateRedWireCommand = new NavigateRedWireCommand(navigationStore, cacheStore);

            stackPanelContent = RequestCenter.GetUserHistoryByRedWire(cacheStore.GetObjectCache(ObjectCacheStoreEnum.RedWireDetail));
        }

        public List<UserHistory> StackPanelContent => stackPanelContent;

        public NavigateRedWireCommand NavigateRedWireCommand { get; }
    }
}
