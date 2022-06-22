using System;
using System.Collections.Generic;
using System.Windows.Threading;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    public class FolderPurgeViewModel : ViewModelBase
    {
        private User actualUser;

        private int purgeFolderNumber;
        private int nonPurgeFolderNumber;
        private bool purgePopUpIsOpen;
        public string purgeDetails;
        public int actualIndex;

        private DateTime startPurgeDate;

        public FolderPurgeViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateParameterMenuCommand = new NavigateParameterMenuCommand(navigationStore, cacheStore);

            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));

            DateTime purgeDate = DateTime.Now.AddYears(-3);
            startPurgeDate = Convert.ToDateTime(purgeDate.Year + "-08-31T00:00:00");

            if (purgeDate < startPurgeDate)
            {
                startPurgeDate = startPurgeDate.AddYears(-1);
            }
            purgeFolderNumber = RequestCenter.GetRedWirePurgeNumber();
            nonPurgeFolderNumber = RequestCenter.GetRedWireNumber(new DateTime(), -1, -1, "") - purgeFolderNumber;

            ActualIndex = 0;
            PurgeDetails = "";
        }

        public void LaunchPurge()
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

                PurgeDetails = "Suppression des documents liées";
                RequestCenter.DeleteDocumentListByRedWire(redWire.Id);

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

        public NavigateParameterMenuCommand NavigateParameterMenuCommand { get; }
    }
}