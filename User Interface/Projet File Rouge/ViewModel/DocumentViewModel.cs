using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.EBPObject;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Document View
    /// </summary>
    public class DocumentViewModel : ViewModelBase
    {
        private SaleDocument document;
        private DocumentList documentList;
        private RedWire redwire;
        private readonly List<SaleDocumentLine> stackPanelContent;

        public DocumentViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            NavigateDocumentCenterCommand = new NavigateDocumentCenterCommand(navigationStore, cacheStore);
            LogOutCommand = new LogOutCommand(navigationStore, cacheStore);

            // set up BDD objects
            document = RequestCenter.GetSaleDocument(cacheStore.GetInfoCache(InfoCacheStoreEnum.SaleDocumentDetail));
            documentList = RequestCenter.GetDocumentListById(cacheStore.GetObjectCache(ObjectCacheStoreEnum.DocumentListDetail));
            redwire = RequestCenter.GetRedWire(cacheStore.GetObjectCache(ObjectCacheStoreEnum.RedWireDetail));
            stackPanelContent = linesEdition(RequestCenter.GetSaleLines(document.Id));
        }

        /// <summary>
        /// Verify and replace specials characters (like line skiping)
        /// </summary>
        /// <param name="Lines"></param>
        /// <returns></returns>
        public List<SaleDocumentLine> linesEdition(List<SaleDocumentLine> Lines)
        {
            foreach (SaleDocumentLine line in Lines)
            {
                line.DescriptionClear = line.DescriptionClear.Replace("\\r", "\r");
                line.DescriptionClear = line.DescriptionClear.Replace("\\n", "\n");
            }
            return Lines;
        }

        internal void DeleteFile()
        {
            RequestCenter.DeleteDocumentList(documentList.Id);
            RequestCenter.PostEvent(new Evenement(redwire, Evenement.EventType.simpleText, "Suppression du document : " + documentList.Document).Jsonify());
            NavigateDocumentCenterCommand.Execute(null);
        }

        public SaleDocument Document
        {
            get => document;
            set => document = value;
        }

        public List<SaleDocumentLine> StackPanelContent  => stackPanelContent;

        /// <summary>
        /// Commands
        /// </summary>
        public NavigateDocumentCenterCommand NavigateDocumentCenterCommand { get; }
        public LogOutCommand LogOutCommand { get; }
    }
}
