using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.EBPObject;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    public class DocumentViewModel : ViewModelBase
    {
        private SaleDocument document;
        private readonly List<SaleDocumentLine> stackPanelContent;

        public DocumentViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateDocumentCenterCommand = new NavigateDocumentCenterCommand(navigationStore, cacheStore);
            LogOutCommand = new LogOutCommand(navigationStore, cacheStore);

            document = RequestCenter.GetSaleDocument(cacheStore.GetInfoCache(InfoCacheStoreEnum.SaleDocumentDetail));
            stackPanelContent = RequestCenter.GetSaleLines(document.Id);
        }

        public SaleDocument Document
        {
            get => document;
            set => document = value;
        }

        public List<SaleDocumentLine> StackPanelContent  => stackPanelContent;

        public NavigateDocumentCenterCommand NavigateDocumentCenterCommand { get; }
        public LogOutCommand LogOutCommand { get; }
    }
}
