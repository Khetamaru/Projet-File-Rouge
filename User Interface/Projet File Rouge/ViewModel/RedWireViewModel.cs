﻿using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.EBPObject;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Red Wire view
    /// </summary>
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
        private bool reopeningPopUpIsOpen;
        private bool commandePiecePopUpIsOpen;
        private bool adminAsignPopUpIsOpen;
        private bool ajoutDocumentPopUpIsOpen;
        private bool giveUpPopUpIsOpen;
        private bool providerCallPopUpIsOpen;
        private bool endProviderCallPopUpIsOpen;

        public RedWireViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            NavigateOutRedWireCommand = new NavigateOutRedWireCommand(navigationStore, cacheStore);
            NavigateDocumentCenterCommand = new NavigateDocumentCenterCommand(navigationStore, cacheStore);
            NavigateUserHistoryCommand = new NavigateUserHistoryCommand(navigationStore, cacheStore);

            // set up BDD objects
            redWire = RequestCenter.GetRedWire(cacheStore.GetObjectCache(ObjectCacheStoreEnum.RedWireDetail));
            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
            saleDocument = RequestCenter.GetSaleDocument(redWire.Client);
            documentList = RequestCenter.GetDocumentList(redWire.Id);

            // set up view objects
            ReloadEvents();
            userList = GetUserNames();
        }

        /// <summary>
        /// Generate user list
        /// </summary>
        /// <returns></returns>
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
            OnPropertyChanged(nameof(ReopeningButtonVisibility));
            OnPropertyChanged(nameof(NonReparableRenduButtonVisibility));
            OnPropertyChanged(nameof(PECDevisButtonVisibility));
            OnPropertyChanged(nameof(PECClientResponseButtonVisibility));
            OnPropertyChanged(nameof(InsertTextVisibility));
            OnPropertyChanged(nameof(AdminAsignButtonVisibility));
            OnPropertyChanged(nameof(GiveUpButtonVisibility));
            OnPropertyChanged(nameof(AjoutDocumentButtonVisibility));
            OnPropertyChanged(nameof(ProviderCallButtonVisibility));
            OnPropertyChanged(nameof(EndProviderCallButtonVisibility));
        }

        /// <summary>
        /// Check Access Level
        /// </summary>
        private bool AccessAuthorization() { return RedWire.ActualRepairMan != null && (ActualUser.Id == RedWire.ActualRepairMan.Id || ActualUser.UserLevel >= User.AccessLevel.SuperUser) ? true : false; }

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
        public bool InsertTextVisibility { get => RedWire.ActualState == RedWire.state.fin || RedWire.ActualState == RedWire.state.abandon || !AccessAuthorization(); }
        public void InsertText()
        {
            if (TextInsert != null && TextInsert.Trim() != string.Empty)
            {
                if (TextInsert.Length < 98)
                {
                    if (ActualUser.Id == RedWire.ActualRepairMan.Id)
                    {
                        AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Message utilisateur : " + TextInsert));
                    }
                    else
                    {
                        AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Message utilisateur (" + ActualUser.Name + ") : " + TextInsert));
                    }
                    RedWireMaj();
                    UIUpdate();
                    TextInsert = string.Empty;
                }
                else
                {
                    PopUpCenter.MessagePopup("Message trop long (98 characters max)");
                }
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

        /// <summary>
        /// Document List Button Logic
        /// </summary>
        public bool DocumentListButtonVisibility { get => DocumentList.Count <= 0; }

        /// <summary>
        /// In Charge Button Logic
        /// </summary>
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
            RedWire.ActualState = RedWire.state.début_diag;
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

        /// <summary>
        /// In Charge Sale Document Button Logic
        /// </summary>
        public bool PECDevisButtonVisibility { get => RedWire.ActualState != RedWire.state.début_diag || !AccessAuthorization(); }
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
                RedWire.ActualState = RedWire.state.début_reponse_client;
                RedWireMaj();
                AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Devis créé et envoyé : " + PECDevisField));
                DocumentList document = new DocumentList(PECDevisField, RedWire);
                AddDocument(document);
                DocumentList.Add(document);
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
            UIUpdate();
            ClosePECDevisPopUp();
        }

        /// <summary>
        /// In Charge Sale Document Client Response Button Logic
        /// </summary>
        public bool PECClientResponseButtonVisibility { get => RedWire.ActualState != RedWire.state.début_reponse_client || !AccessAuthorization(); }
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

        /// <summary>
        /// Piece Command Button Logic
        /// </summary>
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
                if (CommandePieceNameField.Length < 78)
                {
                    CommandList command = new CommandList(RedWire, new DateTime(), CommandePieceNameField, CommandePieceUrlField);
                    RequestCenter.PostCommand(command.Jsonify());
                    RedWire.ActualState = RedWire.state.attente_commande;
                    AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, command.Name + " : Commande envoyée au centre de commande."));
                    RedWireMaj();
                    UIUpdate();
                    CloseCommandePiecePopUp();
                }
                else
                {
                    PopUpCenter.MessagePopup("Message trop long (78 characters max)");
                }
            }
        }
        public void CommandePieceNoButton()
        {
            CloseCommandePiecePopUp();
        }

        /// <summary>
        /// Problem or Asking Button Logic
        /// </summary>
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
        public string problemeQuestionField;
        public string ProblemeQuestionField { get => problemeQuestionField; set { problemeQuestionField = value; OnPropertyChanged("ProblemeQuestionField"); } }
        public void ProblemeQuestionYesButton()
        {
            if (ProblemeQuestionField != null && ProblemeQuestionField != string.Empty)
            {
                if (ProblemeQuestionField.Length < 96)
                {
                    RedWire.ActualState = RedWire.state.attente_client;
                    AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Question(s) au client : " + ProblemeQuestionField));
                    RedWireMaj();
                    UIUpdate();
                    CloseProblemeQuestionPopUp();
                }
                else
                {
                    PopUpCenter.MessagePopup("Message trop long (96 characters max)");
                }
            }
        }

        public void ProblemeQuestionNoButton()
        {
            CloseProblemeQuestionPopUp();
        }

        /// <summary>
        /// Client Response Button Logic
        /// </summary>
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
                if (ReponseClientField.Length < 100)
                {
                    RedWire.ActualState = RedWire.state.en_cours;
                    AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Réponse du client : " + ReponseClientField));
                    ReponseClientField = null;
                    RedWireMaj();
                    UIUpdate();
                }
                else
                {
                    PopUpCenter.MessagePopup("Message trop long (100 characters max)");
                }
            }
            else { PopUpCenter.MessagePopup("Veuillez indiquer la réponse donnée par le client."); }
            CloseReponseClientPopUp();
        }
        public void ReponseClientNoButton()
        {
            CloseReponseClientPopUp();
        }

        /// <summary>
        /// Tech Switch Button Logic
        /// </summary>
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

        /// <summary>
        /// Tech Swicth In Charge Button Logic
        /// </summary>
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

        /// <summary>
        /// End Button Logic
        /// </summary>
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
                RedWire.ActualState = RedWire.state.fin_facture_OK;
                RedWire.RepairEndDate = DateTime.Now;
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
            RedWire.ActualState = RedWire.state.fin_facture_OK;
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Pas de facture finale préparé"));
            UIUpdate();
            CloseFinPopUp();
        }

        /// <summary>
        /// End Client Call Button Logic
        /// </summary>
        public bool FinAppelButtonVisibility { get => RedWire.ActualState != RedWire.state.fin_facture_OK || !AccessAuthorization(); }
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
                if (FinAppelField.Length < 96)
                {
                    RedWire.ActualState = RedWire.state.fin_payement_OK;
                    RedWireMaj();
                    AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Appel client, réponse : " + FinAppelField));
                    AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Matériel client prêt pour rendu"));
                    UIUpdate();
                }
                else
                {
                    PopUpCenter.MessagePopup("Message trop long (96 characters max)");
                }
            }
            else { PopUpCenter.MessagePopup("Veuillez indiquer la réponse donnée par le client."); }
            CloseFinAppelPopUp();
        }

        public void FinAppelYesMailButton()
        {
            RedWire.ActualState = RedWire.state.fin_payement_OK;
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
            RedWire.ActualState = RedWire.state.fin_payement_OK;
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Message vocal laissé pour prévention client"));
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Matériel client prêt pour rendu"));
            UIUpdate();
            CloseFinAppelPopUp();
        }

        /// <summary>
        /// End Pay Button Logic
        /// </summary>
        public bool FinPayementButtonVisibility { get => RedWire.ActualState != RedWire.state.fin_payement_OK || !(ActualUser.UserLevel >= User.AccessLevel.User); }
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

        /// <summary>
        /// Unrepairable Button Logic
        /// </summary>
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
                if (NonReparableField.Length < 96)
                {
                    RedWire.ActualState = RedWire.state.non_réparable_appel;
                    RedWire.RepairEndDate = DateTime.Now;
                    RedWireMaj();
                    AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Dossier non réparable : " + NonReparableField));
                    UIUpdate();
                    CloseNonReparablePopUp();
                }
                else
                {
                    PopUpCenter.MessagePopup("Message trop long (96 characters max)");
                }
            }
        }

        public void NonReparableNoButton()
        {
            CloseNonReparablePopUp();
        }

        /// <summary>
        /// Unrepairable Call Button Logic
        /// </summary>
        public bool NonReparableAppelButtonVisibility { get => RedWire.ActualState != RedWire.state.non_réparable_appel || !AccessAuthorization(); }
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
                if (NonReparableAppelField.Length < 96)
                {
                    RedWire.ActualState = RedWire.state.non_réparable_rendu;
                    RedWireMaj();
                    AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Appel client, réponse : " + NonReparableAppelField));
                    AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Matériel client prêt pour rendu"));
                    UIUpdate();
                }
                else
                {
                    PopUpCenter.MessagePopup("Message trop long (96 characters max)");
                }
            }
            else
            {
                PopUpCenter.MessagePopup("Veuillez indiquer la réponse donnée par le client.");
            }
            CloseNonReparableAppelPopUp();
        }

        public void NonReparableAppelYesMailButton()
        {
            RedWire.ActualState = RedWire.state.non_réparable_rendu;
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
            RedWire.ActualState = RedWire.state.non_réparable_rendu;
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Message vocal laissé pour prévention client"));
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Matériel client prêt pour rendu"));
            UIUpdate();
            CloseNonReparableAppelPopUp();
        }

        /// <summary>
        /// Unrepairable Give Back Button Logic
        /// </summary>
        public bool NonReparableRenduButtonVisibility { get => RedWire.ActualState != RedWire.state.non_réparable_rendu || !(ActualUser.UserLevel >= User.AccessLevel.User); }
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
        public void NonReparableRenduDestructionButton()
        {
            RedWire.ActualState = RedWire.state.fin;
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Matériel parti en destruction"));
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Dossier cloturé"));
            UIUpdate();
            CloseNonReparableRenduPopUp();
        }

        /// <summary>
        /// Reopen Button Logic
        /// </summary>
        public bool ReopeningButtonVisibility { get => RedWire.ActualState != RedWire.state.fin || !(ActualUser.UserLevel >= User.AccessLevel.User); }
        public bool ReopeningPopUpIsOpen
        {
            get => reopeningPopUpIsOpen;
            set
            {
                reopeningPopUpIsOpen = value;
                OnPropertyChanged(nameof(ReopeningPopUpIsOpen));
            }
        }
        public void OpenReopeningPopUp() => ReopeningPopUpIsOpen = true;
        public void CloseReopeningPopUp() => ReopeningPopUpIsOpen = false;
        public void ReopeningYesButton()
        {
            RedWire.ActualState = RedWire.state.en_cours;
            RedWireMaj();
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Dossier réouvert par " + ActualUser.Name));
            UIUpdate();
            CloseReopeningPopUp();
        }
        public void ReopeningNoButton()
        {
            CloseReopeningPopUp();
        }

        /// <summary>
        /// Admin Asign Button Logic
        /// </summary>
        public bool AdminAsignButtonVisibility { get => RedWire.ActualState == RedWire.state.fin || RedWire.ActualState == RedWire.state.abandon || !(ActualUser.UserLevel >= User.AccessLevel.Admin); }
        public bool AdminAsignPopUpIsOpen
        {
            get => adminAsignPopUpIsOpen;
            set
            {
                adminAsignPopUpIsOpen = value;
                OnPropertyChanged(nameof(AdminAsignPopUpIsOpen));
            }
        }
        public void OpenAdminAsignPopUp() => AdminAsignPopUpIsOpen = true;
        public void CloseAdminAsignPopUp() => AdminAsignPopUpIsOpen = false;
        public string adminAsignField;
        public string AdminAsignField { get => adminAsignField; set { adminAsignField = value; } }
        public void AdminAsignYesButton()
        {
            if (AdminAsignField != null)
            {
                User user = RequestCenter.GetUserByName(AdminAsignField);

                if (RedWire.ActualRepairMan == null)
                {
                    RedWire.ActualRepairMan = user;
                    RedWire.ActualState = RedWire.state.début_diag;
                    RedWire.RepairStartDate = DateTime.Now;
                    AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, ActualUser.Name + " a assigné le dossier à " + AdminAsignField));
                }
                else
                {
                    AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, ActualUser.Name + " a fait passer le dossier de " + RedWire.ActualRepairMan.Name + " à " + AdminAsignField));
                    RedWire.ActualRepairMan = user;
                }
                RequestCenter.PostUserHistory(new UserHistory(DateTime.Now, RedWire.ActualRepairMan, RedWire).Jsonify());
                RedWireMaj();
                UIUpdate();
                AdminAsignField = null;
            }
            CloseAdminAsignPopUp();
        }
        public void AdminAsignNoButton()
        {
            CloseAdminAsignPopUp();
        }

        /// <summary>
        /// Give Up Button Logic
        /// </summary>
        public bool GiveUpButtonVisibility { get => RedWire.ActualState == RedWire.state.libre || RedWire.ActualState == RedWire.state.fin || RedWire.ActualState == RedWire.state.abandon || !(ActualUser.UserLevel >= User.AccessLevel.User) || !(DateTime.Now.AddMonths(-6) > RedWire.RepairStartDate); }
        public bool GiveUpPopUpIsOpen
        {
            get => giveUpPopUpIsOpen;
            set
            {
                giveUpPopUpIsOpen = value;
                OnPropertyChanged(nameof(GiveUpPopUpIsOpen));
            }
        }
        public void OpenGiveUpPopUp() => GiveUpPopUpIsOpen = true;
        public void CloseGiveUpPopUp() => GiveUpPopUpIsOpen = false;
        public void GiveUpYesButton()
        {
            RedWire.ActualState = RedWire.state.abandon;
            RedWire.RepairEndDate = DateTime.Now;
            RequestCenter.PostLog(new Log("Dossier abandonné : " + RedWire.ClientName + " - " + RedWire.RepairStartDateFormated, DateTime.Now, Log.LogTypeEnum.RedWire, actualUser).Jsonify());
            AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Dossier et matériel abandonnés par le client."));
            RedWireMaj();
            UIUpdate();
            CloseGiveUpPopUp();
        }
        public void GiveUpNoButton()
        {
            CloseGiveUpPopUp();
        }

        /// <summary>
        /// Add Sale Document Button Logic
        /// </summary>
        public bool AjoutDocumentButtonVisibility { get => RedWire.ActualState != RedWire.state.en_cours || !AccessAuthorization(); }
        public bool AjoutDocumentPopUpIsOpen
        {
            get => ajoutDocumentPopUpIsOpen;
            set
            {
                ajoutDocumentPopUpIsOpen = value;
                OnPropertyChanged(nameof(AjoutDocumentPopUpIsOpen));
            }
        }
        public void OpenAjoutDocumentPopUp() => AjoutDocumentPopUpIsOpen = true;
        public void CloseAjoutDocumentPopUp() => AjoutDocumentPopUpIsOpen = false;
        public string ajoutDocumentField;
        public string AjoutDocumentField { get => ajoutDocumentField; set { ajoutDocumentField = value; OnPropertyChanged("AjoutDocumentField"); } }
        public void AjoutDocumentYesButton()
        {
            (bool result, SaleDocument temp) = SaleDocument.FormatVerification(AjoutDocumentField, new string[] { "DE", "FA", "CM" });
            if (result)
            {
                RedWireMaj();
                AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Document créé : " + AjoutDocumentField));
                DocumentList document = new DocumentList(AjoutDocumentField, RedWire);
                AddDocument(document);
                DocumentList.Add(document);
                UIUpdate();
                CloseAjoutDocumentPopUp();
            }
        }
        public void AjoutDocumentNoButton()
        {
            CloseAjoutDocumentPopUp();
        }

        /// <summary>
        /// You need to do a call to provider
        /// </summary>
        public bool ProviderCallButtonVisibility { get => RedWire.ActualState != RedWire.state.en_cours || !AccessAuthorization(); }
        public bool ProviderCallPopUpIsOpen
        {
            get => providerCallPopUpIsOpen;
            set
            {
                providerCallPopUpIsOpen = value;
                OnPropertyChanged(nameof(ProviderCallPopUpIsOpen));
            }
        }
        public void OpenProviderCallPopUp() => ProviderCallPopUpIsOpen = true;
        public void CloseProviderCallPopUp() => ProviderCallPopUpIsOpen = false;

        public string providerCallField;
        public string ProviderCallField { get => providerCallField; set { providerCallField = value; OnPropertyChanged("ProviderCallField"); } }
        public void ProviderCallYesButton()
        {
            if (ProviderCallField != null)
            {
                if (ProviderCallField.Length < 81)
                {
                    RedWire.ActualState = RedWire.state.attente_fournisseur;
                    RedWireMaj();
                    AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Contact avec le fournisseur effectué : " + ProviderCallField));
                    UIUpdate();
                    CloseProviderCallPopUp();
                }
                else
                {
                    PopUpCenter.MessagePopup("Message trop long (81 characters max)");
                }
            }
        }
        public void ProviderCallNoButton()
        {
            CloseProviderCallPopUp();
        }

        /// <summary>
        /// End of call to provider
        /// </summary>
        public bool EndProviderCallButtonVisibility { get => RedWire.ActualState != RedWire.state.attente_fournisseur || !AccessAuthorization(); }
        public bool EndProviderCallPopUpIsOpen
        {
            get => endProviderCallPopUpIsOpen;
            set
            {
                endProviderCallPopUpIsOpen = value;
                OnPropertyChanged(nameof(EndProviderCallPopUpIsOpen));
            }
        }
        public void OpenEndProviderCallPopUp() => EndProviderCallPopUpIsOpen = true;
        public void CloseEndProviderCallPopUp() => EndProviderCallPopUpIsOpen = false;

        public string endProviderCallField;
        public string EndProviderCallField { get => endProviderCallField; set { endProviderCallField = value; OnPropertyChanged("EndProviderCallField"); } }
        public void EndProviderCallYesButton()
        {
            if (EndProviderCallField != null)
            {
                if (EndProviderCallField.Length < 95)
                {
                    RedWire.ActualState = RedWire.state.en_cours;
                    RedWireMaj();
                    AddEvent(new Evenement(RedWire.Id, Evenement.EventType.simpleText, "Réponse du fournisseur : " + EndProviderCallField));
                    UIUpdate();
                    CloseEndProviderCallPopUp();
                }
                else
                {
                    PopUpCenter.MessagePopup("Message trop long (95 characters max)");
                }
            }
        }
        public void EndProviderCallNoButton()
        {
            CloseEndProviderCallPopUp();
        }

        /// <summary>
        /// Commands
        /// </summary>
        public NavigateDocumentCenterCommand NavigateDocumentCenterCommand { get; }
        public NavigateOutRedWireCommand NavigateOutRedWireCommand { get; }
        public NavigateUserHistoryCommand NavigateUserHistoryCommand { get; }
    }
}