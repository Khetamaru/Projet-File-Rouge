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
            NavigateRedWireCommand = new NavigateRedWireCommand(navigationStore, cacheStore);

            command = RequestCenter.GetCommandList(cacheStore.GetObjectCache(ObjectCacheStoreEnum.CommandListDetail));
            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
            NavigateRedWireCommand.LoadRedWire(Command.RedWire, PageNameEnum.CommandView);
        }

        private void CommandMaj()
        {
            RequestCenter.PutCommand(Command.Id, Command.JsonifyId());
        }

        private void RedWireMaj()
        {
            if (Command.State == CommandList.CommandStatusEnum.annulé)
            {
                RequestCenter.PostEvent(new Evenement(Command.RedWire.Id, Evenement.EventType.simpleText, Command.Name + " : La commande n'a pas aboutie.").Jsonify());
            }
            else if (Command.State == CommandList.CommandStatusEnum.livré)
            {
                RequestCenter.PostEvent(new Evenement(Command.RedWire.Id, Evenement.EventType.simpleText, Command.Name + " : La commande a été livrée.").Jsonify());
            }
            else if (Command.State == CommandList.CommandStatusEnum.commande_en_attente)
            {
                RequestCenter.PostEvent(new Evenement(Command.RedWire.Id, Evenement.EventType.simpleText, Command.Name + " : La commande a été effectuée. Date de livraison prévue : " + Command.DeliveryDateFormated).Jsonify());
            }
            else if (Command.State == CommandList.CommandStatusEnum.livraison_en_cours)
            {
                RequestCenter.PostEvent(new Evenement(Command.RedWire.Id, Evenement.EventType.simpleText, Command.Name + " : Date de livraison prévue passée à " + Command.DeliveryDateFormated).Jsonify());
            }
            if (RequestCenter.GetCommandListRedWireNumber(Command.RedWire.Id))
            {
                Command.RedWire.ActualState = RedWire.state.en_cours;
                RequestCenter.PutRedWire(Command.RedWire.Id, Command.RedWire.JsonifyId());
            }
        }

        private void UIUpdate()
        {
            OnPropertyChanged(nameof(CancelCommandButtonVisibility));
            OnPropertyChanged(nameof(CommandDoneButtonVisibility));
            OnPropertyChanged(nameof(CommandArrivedButtonVisibility));
            OnPropertyChanged(nameof(DeliveryDateUpdateButtonVisibility));
            OnPropertyChanged(nameof(Command));
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
            RedWireMaj();
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
                Command.DeliveryDate = CommandDoneDateField;
                RedWireMaj();
                Command.State = CommandList.CommandStatusEnum.livraison_en_cours;
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
            RedWireMaj();
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
                RedWireMaj();
                UIUpdate();
            }
            CloseDeliveryDateUpdatePopUp();
        }
        public void DeliveryDateUpdateNoButton()
        {
            CloseDeliveryDateUpdatePopUp();
        }

        public NavigateCommandCenterCommand NavigateCommandCenterCommand { get; }
        public NavigateRedWireCommand NavigateRedWireCommand { get; }
    }
}
