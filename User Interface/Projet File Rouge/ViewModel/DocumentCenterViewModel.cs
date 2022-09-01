using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Documents Center View
    /// </summary>
    public class DocumentCenterViewModel : ViewModelBase
    {
        private List<DocumentList> documentList;
        private User actualUser;

        public DocumentCenterViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            NavigateRedWireCommand = new NavigateRedWireCommand(navigationStore, cacheStore);
            NavigateDocumentCommand = new NavigateDocumentCommand(navigationStore, cacheStore);

            // set up BDD objects
            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
            documentList = RequestCenter.GetDocumentLists(cacheStore.GetObjectCache(ObjectCacheStoreEnum.RedWireDetail));
        }

        public List<DocumentList> DocumentList
        {
            get { return documentList; }
            set
            {
                documentList = value;
                OnPropertyChanged(nameof(DocumentList));
            }
        }
        public User ActualUser => actualUser;

        /// <summary>
        /// Commands
        /// </summary>
        public NavigateRedWireCommand NavigateRedWireCommand { get; }
        public NavigateDocumentCommand NavigateDocumentCommand { get; }
    }
}
