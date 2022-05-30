using System;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    public class CommandViewModel : ViewModelBase
    {
        private CommandList command;
        private User actualUser;

        private bool cancelCommandPopUpIsOpen;
        private bool commandDonePopUpIsOpen;
        private bool commandArrivedPopUpIsOpen;
        private bool deliveryDateUpdatePopUpIsOpen;

        public CommandViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateCommandCenterCommand = new NavigateCommandCenterCommand(navigationStore, cacheStore);

            command = RequestCenter.GetCommandList(cacheStore.GetObjectCache(ObjectCacheStoreEnum.CommandListDetail));
            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
        }

        private void CommandMaj()
        {
            RequestCenter.PutRedWire(Command.Id, Command.JsonifyId());
        }

        private void UIUpdate()
        {
            OnPropertyChanged(nameof(CancelCommandButtonVisibility));
            OnPropertyChanged(nameof(CommandDonePopUpIsOpen));
            OnPropertyChanged(nameof(CommandArrivedPopUpIsOpen));
            OnPropertyChanged(nameof(DeliveryDateUpdateButtonVisibility));
        }

        public CommandList Command
        {
            get => command;
            set => command = value;
        }
        public User ActualUser { get => actualUser; }

        public bool CancelCommandButtonVisibility { get => Command.State != CommandList.CommandStatusEnum.commande_en_attente && Command.State != CommandList.CommandStatusEnum.livraison_en_cours; }
        public bool CancelCommandPopUpIsOpen
        {
            get => cancelCommandPopUpIsOpen;
            set
            {
                cancelCommandPopUpIsOpen = value;
                OnPropertyChanged(nameof(CancelCommandPopUpIsOpen));
            }
        }
        public void OpenCancelCommandPopUp() => CancelCommandPopUpIsOpen = true;
        public void CloseCancelCommandPopUp() => CancelCommandPopUpIsOpen = false;
        public void CancelCommandYesButton()
        {
            Command.State = CommandList.CommandStatusEnum.annulé;
            CommandMaj();
            UIUpdate();
            CloseCancelCommandPopUp();
        }
        public void CancelCommandNoButton()
        {
            CloseCancelCommandPopUp();
        }

        public bool CommandDoneButtonVisibility { get => Command.State != CommandList.CommandStatusEnum.commande_en_attente; }
        public bool CommandDonePopUpIsOpen
        {
            get => commandDonePopUpIsOpen;
            set
            {
                commandDonePopUpIsOpen = value;
                OnPropertyChanged(nameof(CommandDonePopUpIsOpen));
            }
        }
        public void OpenCommandDonePopUp() => CommandDonePopUpIsOpen = true;
        public void CloseCommandDonePopUp() => CommandDonePopUpIsOpen = false;
        public DateTime commandDoneDateField;
        public DateTime CommandDoneDateField { get => commandDoneDateField; set { commandDoneDateField = value; OnPropertyChanged(nameof(CommandDoneDateField)); } }
        public void CommandDoneYesButton()
        {
            if (CommandDoneDateField != new DateTime())
            {
                Command.State = CommandList.CommandStatusEnum.livraison_en_cours;
                Command.DeliveryDate = CommandDoneDateField;
                CommandMaj();
                UIUpdate();
            }
            CloseCommandDonePopUp();
        }
        public void CommandDoneNoButton()
        {
            CloseCommandDonePopUp();
        }

        public bool CommandArrivedButtonVisibility { get => Command.State != CommandList.CommandStatusEnum.livraison_en_cours; }
        public bool CommandArrivedPopUpIsOpen
        {
            get => commandArrivedPopUpIsOpen;
            set
            {
                commandArrivedPopUpIsOpen = value;
                OnPropertyChanged(nameof(CommandArrivedPopUpIsOpen));
            }
        }
        public void OpenCommandArrivedPopUp() => CommandArrivedPopUpIsOpen = true;
        public void CloseCommandArrivedPopUp() => CommandArrivedPopUpIsOpen = false;
        public void CommandArrivedYesButton()
        {
            Command.State = CommandList.CommandStatusEnum.livré;
            CommandMaj();
            UIUpdate();
            CloseCommandArrivedPopUp();
        }
        public void CommandArrivedNoButton()
        {
            CloseCommandArrivedPopUp();
        }

        public bool DeliveryDateUpdateButtonVisibility { get => Command.State != CommandList.CommandStatusEnum.livraison_en_cours; }
        public bool DeliveryDateUpdatePopUpIsOpen
        {
            get => deliveryDateUpdatePopUpIsOpen;
            set
            {
                deliveryDateUpdatePopUpIsOpen = value;
                OnPropertyChanged(nameof(DeliveryDateUpdatePopUpIsOpen));
            }
        }
        public void OpenDeliveryDateUpdatePopUp() => DeliveryDateUpdatePopUpIsOpen = true;
        public void CloseDeliveryDateUpdatePopUp() => DeliveryDateUpdatePopUpIsOpen = false;
        public DateTime deliveryDateUpdateField;
        public DateTime DeliveryDateUpdateField { get => deliveryDateUpdateField; set => deliveryDateUpdateField = value; }
        public void DeliveryDateUpdateYesButton()
        {
            if (DeliveryDateUpdateField != new DateTime())
            {
                Command.DeliveryDate = DeliveryDateUpdateField;
                CommandMaj();
                UIUpdate();
            }
            CloseDeliveryDateUpdatePopUp();
        }
        public void DeliveryDateUpdateNoButton()
        {
            CloseDeliveryDateUpdatePopUp();
        }

        public NavigateCommandCenterCommand NavigateCommandCenterCommand { get; }
    }
}
