using System;
using System.Collections.Generic;
using System.ComponentModel;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.EBPObject;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    public class RedWireViewModel : ViewModelBase
    {
        private RedWire redWire;
        private List<Evenement> redWireEvents;
        private SaleDocument saleDocument;
        private List<DocumentList> documentList;
        private User actualUser;
        private List<string> userList;

        private bool priseEnChargePopUpIsOpen;
        private bool pECDevisPopUpIsOpen;
        private bool pECClientResponsePopUpIsOpen;
        private bool problemeQuestionPopUpIsOpen;
        private bool reponseClientPopUpIsOpen;
        private bool transfertTechPopUpIsOpen;
        private bool priseEnChargeTransfertPopUpIsOpen;
        private bool finPopUpIsOpen;
        private bool finAppelPopUpIsOpen;
        private bool finPayementPopUpIsOpen;
        private bool nonReparablePopUpIsOpen;
        private bool nonReparableAppelPopUpIsOpen;
        private bool nonReparableRenduPopUpIsOpen;
        private bool commandePiecePopUpIsOpen;

        public RedWireViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateOutRedWireCommand = new NavigateOutRedWireCommand(navigationStore, cacheStore);

            redWire = RequestCenter.GetRedWire(cacheStore.GetObjectCache(ObjectCacheStoreEnum.RedWireDetail));
            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));

            ReloadEvents();

            saleDocument = RequestCenter.GetSaleDocument(redWire.Client);
            documentList = RequestCenter.GetDocumentList(redWire.Id);

            userList = GetUserNames();
        }

        private List<string> GetUserNames()
        {
            List<string> nameList = new List<string>();
            List<User> users = RequestCenter.GetUsers();
            foreach (User user in users)
            {
                nameList.Add(user.Name);
            }
            return nameList;
        }

        private void ReloadEvents() { RedWireEvents = RequestCenter.GetEvents(redWire.Id); }

        private void RedWireMaj()
        {
            RedWire.RedWireUpdate();
            RequestCenter.PutRedWire(RedWire.Id, RedWire.JsonifyId());
        }

        private void AddEvent(Evenement evenement)
        {
            RequestCenter.PostEvent(evenement.Jsonify());
            ReloadEvents();

            OnPropertyChanged(nameof(RedWireEvents));
        }

        private void AddDocument(DocumentList documentList)
        {
            RequestCenter.PostDocument(documentList.Jsonify());
        }

        private void UIUpdate()
        {
            OnPropertyChanged(nameof(DocumentListButtonVisibility));
            OnPropertyChanged(nameof(PriseEnChargeButtonVisibility));
            OnPropertyChanged(nameof(CommandePieceButtonVisibility));
            OnPropertyChanged(nameof(ProblemeQuestionButtonVisibility));
            OnPropertyChanged(nameof(ReponseClientButtonVisibility));
            OnPropertyChanged(nameof(TransfertTechButtonVisibility));
            OnPropertyChanged(nameof(PriseEnChargeTransfertButtonVisibility));
            OnPropertyChanged(nameof(FinButtonVisibility));
            OnPropertyChanged(nameof(FinAppelButtonVisibility));
            OnPropertyChanged(nameof(FinPayementButtonVisibility));
            OnPropertyChanged(nameof(NonReparableButtonVisibility));
            OnPropertyChanged(nameof(NonReparableAppelButtonVisibility));
            OnPropertyChanged(nameof(NonReparableRenduButtonVisibility));
            OnPropertyChanged(nameof(PECDevisButtonVisibility));
            OnPropertyChanged(nameof(PECClientResponseButtonVisibility));
            OnPropertyChanged(nameof(InsertTextVisibility));
        }

        private bool AccessAuthorization() { return RedWire.ActualRepairMan != null && ActualUser.Id == RedWire.ActualRepairMan.Id ? true : false; }

        public string DetailInfos { get => SaleDocument.DetailInfos(); }

        public string textInsert;
        public string TextInsert
        {
            get => textInsert;
            set { 
                textInsert = value;
                OnPropertyChanged(nameof(TextInsert));
            }
        }
        public bool InsertTextVisibility { get => RedWire.ActualState == RedWire.state.fin || !AccessAuthorization(); }
        public void InsertText()
        {
            if (TextInsert != null && TextInsert.Trim() != string.Empty)
            {
                AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Message utilisateur : " + TextInsert));
                UIUpdate();
                TextInsert = string.Empty;
            }
        }

        public RedWire RedWire
        {
            get => redWire;
            set => redWire = value;
        }

        public List<Evenement> RedWireEvents
        {
            get => redWireEvents;
            set => redWireEvents = value;
        }

        public SaleDocument SaleDocument { get => saleDocument; }
        public List<DocumentList> DocumentList { get => documentList; }
        public User ActualUser { get => actualUser; }
        public List<string> UserList { get => userList; }

        public bool DocumentListButtonVisibility { get => DocumentList.Count <= 0; }

        public bool PriseEnChargeButtonVisibility { get => RedWire.ActualState != RedWire.state.libre; }
        public bool PriseEnChargePopUpIsOpen
        {
            get => priseEnChargePopUpIsOpen;
            set
            {
                priseEnChargePopUpIsOpen = value;
                OnPropertyChanged(nameof(PriseEnChargePopUpIsOpen));
            }
        }
        public void OpenPriseEnChargePopUp() => PriseEnChargePopUpIsOpen = true;
        public void ClosePriseEnChargePopUp() => PriseEnChargePopUpIsOpen = false;
        public void PriseEnChargeYesButton()
        {
            RedWire.ActualState = RedWire.state.start_diag;
            RedWire.ActualRepairMan = ActualUser;
            RedWire.RepairStartDate = DateTime.Now;
            RequestCenter.PostUserHistory(new UserHistory(DateTime.Now, ActualUser, RedWire).Jsonify());
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Dossier pris en charge par " + ActualUser.Name));
            UIUpdate();
            ClosePriseEnChargePopUp();
        }
        public void PriseEnChargeNoButton()
        {
            ClosePriseEnChargePopUp();
        }

        public bool PECDevisButtonVisibility { get => RedWire.ActualState != RedWire.state.start_diag || !AccessAuthorization(); }
        public bool PECDevisPopUpIsOpen
        {
            get => pECDevisPopUpIsOpen;
            set
            {
                pECDevisPopUpIsOpen = value;
                OnPropertyChanged(nameof(PECDevisPopUpIsOpen));
            }
        }
        public void OpenPECDevisPopUp() => PECDevisPopUpIsOpen = true;
        public void ClosePECDevisPopUp() => PECDevisPopUpIsOpen = false;
        public string pECDevisField;
        public string PECDevisField { get => pECDevisField; set { pECDevisField = value; OnPropertyChanged("PECDevisField"); } }
        public void PECDevisYesButton()
        {
            (bool result, SaleDocument temp) = SaleDocument.FormatVerification(PECDevisField, new string[] { "DE" });
            if (result)
            {
                RedWire.ActualState = RedWire.state.start_response;
                RedWireMaj();
                AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Devis créé et envoyé : " + PECDevisField));
                AddDocument(new DocumentList(PECDevisField, RedWire));
                UIUpdate();
                ClosePECDevisPopUp();
            }
        }
        public void PECDevisNoButton()
        {
            ClosePECDevisPopUp();
        }
        public void PECDevisSkipButton()
        {
            RedWire.ActualState = RedWire.state.en_cours;
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Pas de devis post diag créé"));
            AddDocument(new DocumentList(PECDevisField, RedWire));
            UIUpdate();
            ClosePECDevisPopUp();
        }

        public bool PECClientResponseButtonVisibility { get => RedWire.ActualState != RedWire.state.start_response || !AccessAuthorization(); }
        public bool PECClientResponsePopUpIsOpen
        {
            get => pECClientResponsePopUpIsOpen;
            set
            {
                pECClientResponsePopUpIsOpen = value;
                OnPropertyChanged(nameof(PECClientResponsePopUpIsOpen));
            }
        }
        public void OpenPECClientResponsePopUp() => PECClientResponsePopUpIsOpen = true;
        public void ClosePECClientResponsePopUp() => PECClientResponsePopUpIsOpen = false;
        public void PECClientResponseYesButton()
        {
            RedWire.ActualState = RedWire.state.en_cours;
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Devis signé par le client."));
            UIUpdate();
            ClosePECClientResponsePopUp();
        }
        public void PECClientResponseNoButton()
        {
            ClosePECClientResponsePopUp();
        }

        public bool CommandePieceButtonVisibility { get => (RedWire.ActualState != RedWire.state.en_cours && RedWire.ActualState != RedWire.state.attente_commande) || !AccessAuthorization(); }
        public bool CommandePiecePopUpIsOpen
        {
            get => commandePiecePopUpIsOpen;
            set
            {
                commandePiecePopUpIsOpen = value;
                OnPropertyChanged(nameof(CommandePiecePopUpIsOpen));
            }
        }
        private string commandePieceNameField;
        public string CommandePieceNameField
        {
            get => commandePieceNameField;
            set
            {
                commandePieceNameField = value;
                OnPropertyChanged(nameof(CommandePieceNameField));
            }
        }
        private string commandePieceUrlField;
        public string CommandePieceUrlField
        {
            get => commandePieceUrlField;
            set
            {
                commandePieceUrlField = value;
                OnPropertyChanged(nameof(CommandePieceUrlField));
            }
        }
        public void OpenCommandePiecePopUp() => CommandePiecePopUpIsOpen = true;
        public void CloseCommandePiecePopUp() => CommandePiecePopUpIsOpen = false;
        public void CommandePieceYesButton()
        {
            if (CommandePieceNameField != string.Empty && CommandePieceNameField != null && CommandePieceUrlField != string.Empty && CommandePieceUrlField != null)
            {
                CommandList command = new CommandList(RedWire, new DateTime(), CommandePieceNameField, CommandePieceUrlField);
                RequestCenter.PostCommand(command.Jsonify());
                RedWire.ActualState = RedWire.state.attente_commande;
                AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, command.Name + " : Commande envoyée au centre de commande."));
            }
            RedWireMaj();
            UIUpdate();
            CloseCommandePiecePopUp();
        }
        public void CommandePieceNoButton()
        {
            CloseCommandePiecePopUp();
        }

        public bool ProblemeQuestionButtonVisibility { get => RedWire.ActualState != RedWire.state.en_cours || !AccessAuthorization(); }
        public bool ProblemeQuestionPopUpIsOpen
        {
            get => problemeQuestionPopUpIsOpen;
            set
            {
                problemeQuestionPopUpIsOpen = value;
                OnPropertyChanged(nameof(ProblemeQuestionPopUpIsOpen));
            }
        }
        public void OpenProblemeQuestionPopUp() => ProblemeQuestionPopUpIsOpen = true;
        public void CloseProblemeQuestionPopUp() => ProblemeQuestionPopUpIsOpen = false;
        public void ProblemeQuestionYesButton()
        {
            RedWire.ActualState =  RedWire.state.attente_client;
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Prise de contact avec le client."));
            RedWireMaj();
            UIUpdate();
            CloseProblemeQuestionPopUp();
        }
        public void ProblemeQuestionNoButton()
        {
            CloseProblemeQuestionPopUp();
        }

        public bool ReponseClientButtonVisibility { get => RedWire.ActualState != RedWire.state.attente_client || !AccessAuthorization(); }
        public bool ReponseClientPopUpIsOpen
        {
            get => reponseClientPopUpIsOpen;
            set
            {
                reponseClientPopUpIsOpen = value;
                OnPropertyChanged(nameof(ReponseClientPopUpIsOpen));
            }
        }
        public void OpenReponseClientPopUp() => ReponseClientPopUpIsOpen = true;
        public void CloseReponseClientPopUp() => ReponseClientPopUpIsOpen = false;
        public string reponseClientField;
        public string ReponseClientField { get => reponseClientField; set { reponseClientField = value; OnPropertyChanged("ReponseClientField"); } }
        public void ReponseClientYesButton()
        {
            if (ReponseClientField != null && ReponseClientField != string.Empty)
            {
                RedWire.ActualState = RedWire.state.en_cours;
                AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Réponse du client : " + ReponseClientField));
                ReponseClientField = null;
                RedWireMaj();
                UIUpdate();
            }
            else { PopUpCenter.MessagePopup("Veuillez indiquer la réponse donnée par le client."); }
            CloseReponseClientPopUp();
        }
        public void ReponseClientNoButton()
        {
            CloseReponseClientPopUp();
        }

        public bool TransfertTechButtonVisibility { get => RedWire.ActualState != RedWire.state.en_cours || !AccessAuthorization(); }
        public bool TransfertTechPopUpIsOpen
        {
            get => transfertTechPopUpIsOpen;
            set
            {
                transfertTechPopUpIsOpen = value;
                OnPropertyChanged(nameof(TransfertTechPopUpIsOpen));
            }
        }
        public void OpenTransfertTechPopUp() => TransfertTechPopUpIsOpen = true;
        public void CloseTransfertTechPopUp() => TransfertTechPopUpIsOpen = false;
        public string transfertTechField;
        public string TransfertTechField { get => transfertTechField; set { transfertTechField = value; } }
        public void TransfertTechYesButton()
        {
            if (TransfertTechField != null)
            {
                User user = RequestCenter.GetUserByName(TransfertTechField);
                RedWire.TransfertTarget = user;
                RedWire.ActualState = RedWire.state.transit;
                AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Transfert du dossier initié de " + ActualUser.Name + " à " + TransfertTechField));
                RedWireMaj();
                TransfertTechField = null;
                UIUpdate();
                CloseTransfertTechPopUp();
            }
        }
        public void TransfertTechNoButton()
        {
            CloseTransfertTechPopUp();
        }

        public bool PriseEnChargeTransfertButtonVisibility { get => RedWire.ActualState != RedWire.state.transit || (RedWire.ActualRepairMan.Id != ActualUser.Id && RedWire.TransfertTarget.Id != ActualUser.Id); }
        public bool PriseEnChargeTransfertPopUpIsOpen
        {
            get => priseEnChargeTransfertPopUpIsOpen;
            set
            {
                priseEnChargeTransfertPopUpIsOpen = value;
                OnPropertyChanged(nameof(PriseEnChargeTransfertPopUpIsOpen));
            }
        }
        public void OpenPriseEnChargeTransfertPopUp() => PriseEnChargeTransfertPopUpIsOpen = true;
        public void ClosePriseEnChargeTransfertPopUp() => PriseEnChargeTransfertPopUpIsOpen = false;
        public void PriseEnChargeTransfertYesButton()
        {
            RedWire.ActualState = RedWire.state.en_cours;
            if (ActualUser.Id == RedWire.ActualRepairMan.Id) { AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Transfert annulé.")); }
            else if (ActualUser.Id == RedWire.TransfertTarget.Id)
            {
                AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Transfert terminé"));
                RedWire.ActualRepairMan = ActualUser;
                RequestCenter.PostUserHistory(new UserHistory(DateTime.Now, ActualUser, RedWire).Jsonify());
            }
            RedWireMaj();
            UIUpdate();
            ClosePriseEnChargeTransfertPopUp();
        }
        public void PriseEnChargeTransfertNoButton()
        {
            ClosePriseEnChargeTransfertPopUp();
        }

        public bool FinButtonVisibility { get => RedWire.ActualState != RedWire.state.en_cours || !AccessAuthorization(); }
        public bool FinPopUpIsOpen
        {
            get => finPopUpIsOpen;
            set
            {
                finPopUpIsOpen = value;
                OnPropertyChanged(nameof(FinPopUpIsOpen));
            }
        }
        public string finField;
        public string FinField { get => finField; set { finField = value; OnPropertyChanged("FinField"); } }
        public void OpenFinPopUp() => FinPopUpIsOpen = true;
        public void CloseFinPopUp() => FinPopUpIsOpen = false;
        public void FinYesButton()
        {
            (bool result, SaleDocument temp) = SaleDocument.FormatVerification(FinField, new string[] { "FA" });
            if (result)
            {
                RedWire.ActualState = RedWire.state.fin_facture;
                RedWireMaj();
                AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Facture finale faite : " + FinField));
                AddDocument(new DocumentList(FinField, RedWire));
                UIUpdate();
                CloseFinPopUp();
            }
        }
        public void FinNoButton()
        {
            CloseFinPopUp();
        }
        public void FinSkipButton()
        {
            RedWire.ActualState = RedWire.state.fin_facture;
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Pas de facture finale préparé"));
            UIUpdate();
            CloseFinPopUp();
        }

        public bool FinAppelButtonVisibility { get => RedWire.ActualState != RedWire.state.fin_facture || !AccessAuthorization(); }
        public bool FinAppelPopUpIsOpen
        {
            get => finAppelPopUpIsOpen;
            set
            {
                finAppelPopUpIsOpen = value;
                OnPropertyChanged(nameof(FinAppelPopUpIsOpen));
            }
        }
        public string finAppelField;
        public string FinAppelField { get => finAppelField; set { finAppelField = value; OnPropertyChanged("FinAppelField"); } }
        public void OpenFinAppelPopUp() => FinAppelPopUpIsOpen = true;
        public void CloseFinAppelPopUp() => FinAppelPopUpIsOpen = false;
        public void FinAppelYesPhoneButton()
        {
            if (FinAppelField != null && FinAppelField != string.Empty)
            {
                RedWire.ActualState = RedWire.state.fin_payé;
                RedWireMaj();
                AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Appel client, réponse : " + FinAppelField));
                AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Matériel client prêt pour rendu"));
                UIUpdate();
            }
            else { PopUpCenter.MessagePopup("Veuillez indiquer la réponse donnée par le client."); }
            CloseFinAppelPopUp();
        }
        public void FinAppelYesMailButton()
        {
            RedWire.ActualState = RedWire.state.fin_payé;
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Mail envoyé pour prévention client"));
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Matériel client prêt pour rendu"));
            UIUpdate();
            CloseFinAppelPopUp();
        }
        public void FinAppelNoButton()
        {
            CloseFinAppelPopUp();
        }
        public void FinAppelSkipButton()
        {
            RedWire.ActualState = RedWire.state.fin_payé;
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Message vocal laissé pour prévention client"));
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Matériel client prêt pour rendu"));
            UIUpdate();
            CloseFinAppelPopUp();
        }

        public bool FinPayementButtonVisibility { get => RedWire.ActualState != RedWire.state.fin_payé || !AccessAuthorization(); }
        public bool FinPayementPopUpIsOpen
        {
            get => finPayementPopUpIsOpen;
            set
            {
                finPayementPopUpIsOpen = value;
                OnPropertyChanged(nameof(FinPayementPopUpIsOpen));
            }
        }
        public void OpenFinPayementPopUp() => FinPayementPopUpIsOpen = true;
        public void CloseFinPayementPopUp() => FinPayementPopUpIsOpen = false;
        public void FinPayementYesButton()
        {
            RedWire.ActualState = RedWire.state.fin;
            RedWire.RepairEndDate = DateTime.Now;
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Facture payée et matériel rendu"));
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Dossier cloturé"));
            UIUpdate();
            CloseFinPayementPopUp();
        }
        public void FinPayementNoButton()
        {
            CloseFinPayementPopUp();
        }

        public bool NonReparableButtonVisibility { get => RedWire.ActualState != RedWire.state.en_cours || !AccessAuthorization(); }
        public bool NonReparablePopUpIsOpen
        {
            get => nonReparablePopUpIsOpen;
            set
            {
                nonReparablePopUpIsOpen = value;
                OnPropertyChanged(nameof(NonReparablePopUpIsOpen));
            }
        }
        public string nonReparableField;
        public string NonReparableField { get => nonReparableField; set { nonReparableField = value; OnPropertyChanged("NonReparableField"); } }
        public void OpenNonReparablePopUp() => NonReparablePopUpIsOpen = true;
        public void CloseNonReparablePopUp() => NonReparablePopUpIsOpen = false;
        public void NonReparableYesButton()
        {
            if (NonReparableField != null && NonReparableField != string.Empty)
            {
                RedWire.ActualState = RedWire.state.NR_appel;
                RedWireMaj();
                AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Dossier non réparable : " + NonReparableField));
                UIUpdate();
                CloseNonReparablePopUp();
            }
        }
        public void NonReparableNoButton()
        {
            CloseNonReparablePopUp();
        }

        public bool NonReparableAppelButtonVisibility { get => RedWire.ActualState != RedWire.state.NR_appel || !AccessAuthorization(); }
        public bool NonReparableAppelPopUpIsOpen
        {
            get => nonReparableAppelPopUpIsOpen;
            set
            {
                nonReparableAppelPopUpIsOpen = value;
                OnPropertyChanged(nameof(NonReparableAppelPopUpIsOpen));
            }
        }
        public string nonReparableAppelField;
        public string NonReparableAppelField { get => nonReparableAppelField; set { nonReparableAppelField = value; OnPropertyChanged("NonReparableAppelField"); } }
        public void OpenNonReparableAppelPopUp() => NonReparableAppelPopUpIsOpen = true;
        public void CloseNonReparableAppelPopUp() => NonReparableAppelPopUpIsOpen = false;
        public void NonReparableAppelYesPhoneButton()
        {
            if (NonReparableAppelField != null && NonReparableAppelField != string.Empty)
            {
                RedWire.ActualState = RedWire.state.NR_rendu;
                RedWireMaj();
                AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Appel client, réponse : " + NonReparableAppelField));
                AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Matériel client prêt pour rendu"));
                UIUpdate();
            }
            else { PopUpCenter.MessagePopup("Veuillez indiquer la réponse donnée par le client."); }
            CloseNonReparableAppelPopUp();
        }
        public void NonReparableAppelYesMailButton()
        {
            RedWire.ActualState = RedWire.state.NR_rendu;
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Mail envoyé pour prévention client"));
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Matériel client prêt pour rendu"));
            UIUpdate();
            CloseNonReparableAppelPopUp();
        }
        public void NonReparableAppelNoButton()
        {
            CloseNonReparableAppelPopUp();
        }
        public void NonReparableAppelSkipButton()
        {
            RedWire.ActualState = RedWire.state.NR_rendu;
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Message vocal laissé pour prévention client"));
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Matériel client prêt pour rendu"));
            UIUpdate();
            CloseNonReparableAppelPopUp();
        }

        public bool NonReparableRenduButtonVisibility { get => RedWire.ActualState != RedWire.state.NR_rendu || !AccessAuthorization(); }
        public bool NonReparableRenduPopUpIsOpen
        {
            get => nonReparableRenduPopUpIsOpen;
            set
            {
                nonReparableRenduPopUpIsOpen = value;
                OnPropertyChanged(nameof(NonReparableRenduPopUpIsOpen));
            }
        }
        public void OpenNonReparableRenduPopUp() => NonReparableRenduPopUpIsOpen = true;
        public void CloseNonReparableRenduPopUp() => NonReparableRenduPopUpIsOpen = false;
        public void NonReparableRenduYesButton()
        {
            RedWire.ActualState = RedWire.state.fin;
            RedWire.RepairEndDate = DateTime.Now;
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Matériel rendu"));
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Dossier cloturé"));
            UIUpdate();
            CloseNonReparableRenduPopUp();
        }
        public void NonReparableRenduNoButton()
        {
            CloseNonReparableRenduPopUp();
        }

        public NavigateOutRedWireCommand NavigateOutRedWireCommand { get; }
    }
}
