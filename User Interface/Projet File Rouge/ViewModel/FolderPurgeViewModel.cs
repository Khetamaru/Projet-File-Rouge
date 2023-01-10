using System;
using System.Collections.Generic;
using System.Windows.Threading;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Purge Folder View
    /// </summary>
    public class FolderPurgeViewModel : ViewModelBase
    {
        private User actualUser;

        private int purgeFolderNumber;
        private int nonPurgeFolderNumber;
        private bool purgePopUpIsOpen;
        public string purgeDetails;
        public int actualIndex;

        private DateTime startPurgeDate;

        KeyValuePair<NavigationStore, CacheStore> Cache;

        FTPCenter client;

        public FolderPurgeViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            NavigateParameterMenuCommand = new NavigateParameterMenuCommand(navigationStore, cacheStore);

            // set up BDD objects
            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
            purgeFolderNumber = RequestCenter.GetRedWirePurgeNumber();
            nonPurgeFolderNumber = RequestCenter.GetRedWireNumber(new DateTime(), -1, -1, "") - purgeFolderNumber;

            Cache = new KeyValuePair<NavigationStore, CacheStore>(navigationStore, cacheStore);

            (bool trigger, KeyValuePair<string, string> keyValuePair) = Cache.Value.GetFTPCache();

            if (trigger) client = new FTPCenter(keyValuePair.Key, keyValuePair.Value);
            else client = null;

            // set up view objects
            DateTime purgeDate = DateTime.Now.AddYears(-3);
            startPurgeDate = Convert.ToDateTime(purgeDate.Year + "-08-31T00:00:00");

            if (purgeDate < startPurgeDate)
            {
                startPurgeDate = startPurgeDate.AddYears(-1);
            }
            ActualIndex = 0;
            PurgeDetails = "";
        }

        /// <summary>
        /// Start purge process
        /// </summary>
        public void LaunchPurge()
        {
            if (client != null && client.Connected && client.Authenticated)
            {
                ActualIndex = 0;
                PurgeDetails = "Initialisation de la purge";
                PurgePopUpIsOpen = true;
                List<RedWire> redWires = RequestCenter.GetRedWirePurge();

                foreach (RedWire redWire in redWires)
                {
                    ActualIndex++;

                    PurgeDetails = "Suppression des commandes liées";
                    RequestCenter.DeleteCommandListByRedWire(redWire.Id);

                    PurgeDetails = "Suppression des documents EBP liées";
                    RequestCenter.DeleteDocumentListByRedWire(redWire.Id);

                    PurgeDetails = "Suppression des documents FTP liées";
                    RequestCenter.DeleteDocumentFTPByRedWire(redWire.Id);

                    PurgeDetails = "Suppression des documents FTP côté NAS";
                    DeleteFTPFiles(redWire.Id);

                    PurgeDetails = "Suppression des évènements liès";
                    RequestCenter.DeleteEvenementByRedWire(redWire.Id);

                    PurgeDetails = "Suppression de l'historique";
                    RequestCenter.DeleteUserHistoryListByRedWire(redWire.Id);

                    PurgeDetails = "Suppression du suivi de dossier";
                    RequestCenter.DeleteRedWire(redWire.Id);
                }
                PurgePopUpIsOpen = false;
                RequestCenter.PostLog(new Log("Purge dossiers effectiuée. Nombre de dossiers purgés : " + PurgeFolderNumber + " / " + (nonPurgeFolderNumber + PurgeFolderNumber), DateTime.Now, Log.LogTypeEnum.RedWire, actualUser).Jsonify());
                PurgeFolderNumber = 0;
                PopUpCenter.MessagePopup("Purge dossiers terminée.");
            }
            else
            {
                OpenFTPPopUp();
            }
        }

        /// <summary>
        /// Force UI to update during animation
        /// </summary>
        void AllowUIToUpdate()
        {
            DispatcherFrame frame = new DispatcherFrame();
            _ = Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(delegate (object parameter)
            {
                frame.Continue = false;
                return null;
            }), null);
            Dispatcher.PushFrame(frame);
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
                    LaunchPurge();
                }
                else OpenFTPPopUp();
            }
        }
        public void FTPNoButton()
        {
            CloseFTPPopUp();
        }

        private void DeleteFTPFiles(int id)
        {
            List<DocumentFTP> documentFTPs = RequestCenter.GetDocumentFTPByRedWire(id);
            foreach(DocumentFTP documentFTP in documentFTPs)
            {
                client.DataDroping(documentFTP.UploadString());
            }
        }

        public bool PurgePopUpIsOpen
        {
            get => purgePopUpIsOpen;
            set
            {
                purgePopUpIsOpen = value;
                OnPropertyChanged(nameof(PurgePopUpIsOpen));
            }
        }
        public string PurgeIndexing => ActualIndex + "/" + PurgeFolderNumber;

        public string PurgeDetails
        {
            get => purgeDetails;
            set
            {
                purgeDetails = value;
                OnPropertyChanged(nameof(PurgeDetails));
                AllowUIToUpdate();
            }
        }

        public int ActualIndex
        {
            get => actualIndex;
            set
            {
                actualIndex = value;
                OnPropertyChanged(nameof(PurgeIndexing));
                AllowUIToUpdate();
            }
        }

        public User ActualUser
        {
            get => actualUser;
        }

        public int PurgeFolderNumber
        {
            get => purgeFolderNumber;
            set
            {
                purgeFolderNumber = value;
                OnPropertyChanged(nameof(PurgeFolderNumber));
            }
        }

        public int NonPurgeFolderNumber
        {
            get => nonPurgeFolderNumber;
        }

        public string StartPurgeDate
        {
            get => startPurgeDate.ToString("dd'/'MM'/'yyyy");
        }

        private void CacheMaj()
        {
            Cache.Value.SetFTPCache(FTPLoginField, FTPPasswordField);

            NavigateParameterMenuCommand = new NavigateParameterMenuCommand(Cache.Key, Cache.Value);
        }

        /// <summary>
        /// Commands
        /// </summary>
        public NavigateParameterMenuCommand NavigateParameterMenuCommand { get; set; }
    }
}