using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Command View
    /// </summary>
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
            // set up commands
            NavigateCommandCenterCommand = new NavigateCommandCenterCommand(navigationStore, cacheStore);
            NavigateRedWireCommand = new NavigateRedWireCommand(navigationStore, cacheStore);

            // set up BDD objects
            command = RequestCenter.GetCommandList(cacheStore.GetObjectCache(ObjectCacheStoreEnum.CommandListDetail));
            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));

            // set up view objects
            NavigateRedWireCommand.LoadRedWire(Command.RedWire, PageNameEnum.CommandView);
            commandDoneDateField = DateTime.Now;
            deliveryDateUpdateField = DateTime.Now;
            OnPropertyChanged(nameof(deliveryDateUpdateField));
        }

        private void CommandMaj()
        {
            RequestCenter.PutCommand(Command.Id, Command.Jsonify());
        }

        private void RedWireMaj()
        {
            if (Command.State == CommandList.CommandStatusEnum.annulé)
            {
                RequestCenter.PostEvent(new Evenement(Command.RedWire, Evenement.EventType.simpleText, Command.Name + " : La commande n'a pas aboutie.").Jsonify());
            }
            else if (Command.State == CommandList.CommandStatusEnum.livré)
            {
                RequestCenter.PostEvent(new Evenement(Command.RedWire, Evenement.EventType.simpleText, Command.Name + " : La commande a été livrée.").Jsonify());
            }
            else if (Command.State == CommandList.CommandStatusEnum.commande_en_attente)
            {
                RequestCenter.PostEvent(new Evenement(Command.RedWire, Evenement.EventType.simpleText, Command.Name + " : La commande a été effectuée. Date de délai dépassé prévue : " + Command.DeliveryDateFormated).Jsonify());
            }
            else if (Command.State == CommandList.CommandStatusEnum.livraison_en_cours)
            {
                RequestCenter.PostEvent(new Evenement(Command.RedWire, Evenement.EventType.simpleText, Command.Name + " : Date de délai dépassé prévue passée à " + Command.DeliveryDateFormated).Jsonify());
            }
            if (RequestCenter.GetCommandListRedWireNumber(Command.RedWire.Id))
            {
                Command.RedWire.ActualState = RedWire.State.en_cours;
                RequestCenter.PutRedWire(Command.RedWire.Id, Command.RedWire.Jsonify());
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

        /// <summary>
        /// Cancel Command Button Logic
        /// </summary>
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
            RequestCenter.PostLog(new Log("Annulation de commande : " + Command.Name, DateTime.Now, Log.LogTypeEnum.CommandList, actualUser).Jsonify());
            CommandMaj();
            RedWireMaj();
            UIUpdate();
            CloseCancelCommandPopUp();
        }
        public void CancelCommandNoButton()
        {
            CloseCancelCommandPopUp();
        }

        /// <summary>
        /// Command Done Button Logic
        /// </summary>
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
                RequestCenter.PostLog(new Log("Commande Faite : " + Command.Name, DateTime.Now, Log.LogTypeEnum.CommandList, actualUser).Jsonify());
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

        /// <summary>
        /// Command Arrived Button Logic
        /// </summary>
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
            RequestCenter.PostLog(new Log("Commande arrivée : " + Command.Name, DateTime.Now, Log.LogTypeEnum.CommandList, actualUser).Jsonify());
            RequestCenter.PostMissingCall(new MissingCall(ActualUser, Command.RedWire.ActualRepairMan, "Antho Bot", "La commande : " + Command.Name + " est arrivée.", false).Jsonify());
            CommandMaj();
            RedWireMaj();
            UIUpdate();
            CloseCommandArrivedPopUp();
        }
        public void CommandArrivedNoButton()
        {
            CloseCommandArrivedPopUp();
        }

        /// <summary>
        /// Update Delivery Date Button Logic
        /// </summary>
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

        /// <summary>
        /// Commands
        /// </summary>
        public NavigateCommandCenterCommand NavigateCommandCenterCommand { get; }
        public NavigateRedWireCommand NavigateRedWireCommand { get; }
    }
}
