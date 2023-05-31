using System;
using System.Collections.Generic;
using System.Diagnostics;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Documents Center View
    /// </summary>
    public class DocumentFTPCenterViewModel : ViewModelBase
    {
        private List<DocumentFTP> documentsFTP;
        private User actualUser;

        private int indexCache;

        KeyValuePair<NavigationStore, CacheStore> Cache;

        FTPCenter client;

        public DocumentFTPCenterViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            NavigateRedWireCommand = new NavigateRedWireCommand(navigationStore, cacheStore);
            NavigateDocumentFTPCenterCommand = new NavigateDocumentFTPCenterCommand(navigationStore, cacheStore);

            // set up BDD objects
            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
            documentsFTP = RequestCenter.GetDocumentFTPByRedWire(cacheStore.GetObjectCache(ObjectCacheStoreEnum.RedWireDetail));

            Cache = new KeyValuePair<NavigationStore, CacheStore>(navigationStore, cacheStore);
            (bool trigger, KeyValuePair<string, string> keyValuePair) = Cache.Value.GetFTPCache();

            if (trigger) client = new FTPCenter(keyValuePair.Key, keyValuePair.Value);
            else client = null;
        }

        public void HyperlinkRequest(string url)
        {
            try
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch
            {
                PopUpCenter.MessagePopup("Le billy qui a écrit l'url sait pas faire un \"Ctrl + C & Ctrl + V\"");
            }
        }

        public bool fTPPopUpIsOpen;
        public bool FTPPopUpIsOpen
        {
            get => fTPPopUpIsOpen;
            set
            {
                fTPPopUpIsOpen = value;
                OnPropertyChanged(nameof(FTPPopUpIsOpen));
            }
        }
        public void OpenFTPPopUp() => FTPPopUpIsOpen = true;
        public void CloseFTPPopUp() => FTPPopUpIsOpen = false;

        public string fTPLoginField;
        public string FTPLoginField { get => fTPLoginField; set { fTPLoginField = value; OnPropertyChanged(nameof(FTPLoginField)); } }
        public string fTPPasswordField;
        public string FTPPasswordField { get => fTPPasswordField; set { fTPPasswordField = value; OnPropertyChanged(nameof(FTPPasswordField)); } }
        public void FTPYesButton()
        {
            if (FTPLoginField != null && FTPLoginField != string.Empty && FTPPasswordField != null && FTPPasswordField != string.Empty)
            {
                CloseFTPPopUp();
                client = new FTPCenter(FTPLoginField, FTPPasswordField);

                if (client.IsConnected() && client.IsAuthenticated())
                {
                    CacheMaj();
                    DeleteDocumentFTP(indexCache);
                }
                else OpenFTPPopUp();
            }
        }

        internal void DeleteDocumentFTP(int id)
        {
            indexCache = id;

            if (client != null && client.Connected && client.Authenticated)
            {

                DeleteFTPFiles(id);
            }
            else
            {
                OpenFTPPopUp();
            }
        }

        public void FTPNoButton()
        {
            CloseFTPPopUp();
        }

        private void DeleteFTPFiles(int id)
        {
            DocumentFTP documentFTP = RequestCenter.GetDocumentFTP(id);

            client.DataDroping(documentFTP.UploadString());

            RequestCenter.DeleteDocumentFTP(id);
            RequestCenter.PostEvent(new Evenement(documentFTP.RedWire, Evenement.EventType.simpleText, "Document FTP supprimé : " + documentFTP.DocumentName).Jsonify());

            NavigateDocumentFTPCenterCommand.Execute(null);
        }

        private void CacheMaj()
        {
            Cache.Value.SetFTPCache(FTPLoginField, FTPPasswordField);

            NavigateRedWireCommand = new NavigateRedWireCommand(Cache.Key, Cache.Value);
            NavigateDocumentFTPCenterCommand = new NavigateDocumentFTPCenterCommand(Cache.Key, Cache.Value);
        }

        public List<DocumentFTP> DocumentsFTP
        {
            get { return documentsFTP; }
            set
            {
                documentsFTP = value;
                OnPropertyChanged(nameof(DocumentsFTP));
            }
        }
        public User ActualUser => actualUser;

        /// <summary>
        /// Commands
        /// </summary>
        public NavigateRedWireCommand NavigateRedWireCommand { get; set; }
        public NavigateDocumentFTPCenterCommand NavigateDocumentFTPCenterCommand { get; set; }
    }
}
