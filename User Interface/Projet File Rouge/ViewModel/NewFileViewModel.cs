using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.EBPObject;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// New File View
    /// </summary>
    class NewFileViewModel : ViewModelBase
    {
        private string inChargeNumber;
        private SaleDocument saleDocument;
        private string optionEBP;
        private SaleDocument optionDocument;

        private string clientName;
        private string department;
        private string country;
        private string cp;
        private string city;
        private string addresse;
        private string email;
        private string cellPhoneNumber;
        private string phoneNumber;

        private RedWire.Action? actionType;
        private RedWire.EquipmentType? supportType;
        private string supportModel;
        private bool bag;
        private bool alimentation;
        private bool mouse;
        private bool battery;
        private bool other;
        private string otherText;
        private bool warranty;
        private bool problemReproduced;
        private string supportState;

        private User ActualUser;

        public NewFileViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);

            // set up view objects
            otherText = string.Empty;

            ActualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
        }

        public void ChargeSaleDocument(object sender, RoutedEventArgs e, string[] strTab)
        {
            (bool result, SaleDocument temp) = SaleDocument.FormatVerification(inChargeNumber, strTab);
            if (result)
            {
                SaleDocument = temp;
                PopUpCenter.MessagePopup("Informations importées.");
            }
        }

        /// <summary>
        /// Check boxs
        /// </summary>
        /// <param name="boxName"></param>
        public void Box_checked(string boxName)
        {
            switch(boxName)
            {
                case "Sacoche":
                    Bag = true;
                    break;

                case "Chargeur":
                    Alimentation = true;
                    break;

                case "Souris":
                    Mouse = true;
                    break;

                case "Batterie":
                    Battery = true;
                    break;

                case "Autre":
                    Other = true;
                    break;

                case "Sous Garanti":
                    Warranty = true;
                    break;

                case "Problème reproduit et/ou constaté":
                    ProblemReproduced = true;
                    break;
            }
        }

        /// <summary>
        /// Uncheck Boxs
        /// </summary>
        /// <param name="boxName"></param>
        public void Box_unchecked(string boxName)
        {
            switch (boxName)
            {
                case "Sacoche":
                    Bag = false;
                    break;

                case "Chargeur":
                    Alimentation = false;
                    break;

                case "Souris":
                    Mouse = false;
                    break;

                case "Batterie":
                    Battery = false;
                    break;

                case "Autre":
                    Other = false;
                    break;

                case "Sous Garanti":
                    Warranty = false;
                    break;

                case "Problème reproduit et/ou constaté":
                    ProblemReproduced = false;
                    break;
            }
        }

        public string InChargeNumber
        {
            get { return inChargeNumber; }
            set 
            { 
                inChargeNumber = value;
                OnPropertyChanged(nameof(InChargeNumberVisibility));
            }
        }
        public bool InChargeNumberVisibility { get => InChargeNumber != null && InChargeNumber.Length > 0; }

        public SaleDocument SaleDocument
        {
            get { return saleDocument; }
            set 
            { 
                saleDocument = value;
                UIUpdate();
            }
        }

        public string OptionEBP
        {
            get { return optionEBP; }
            set 
            { 
                optionEBP = value;
                OnPropertyChanged(nameof(OptionEBPVisibility));
            }
        }
        public bool OptionEBPVisibility { get => OptionEBP != null && OptionEBP.Length > 0; }

        public SaleDocument OptionDocument
        {
            get { return optionDocument; }
            set
            {
                optionDocument = value;
            }
        }

        /// <summary>
        /// Launch Red Wire Creation
        /// </summary>
        public void RedWireCreation()
        {
            (bool result, List<string> stringList) = FieldsVerification();
            if (result)
            {
                RedWire redWire = new RedWire(0,
                                              RequestCenter.GetUser(NavigateMainMenuCommand.cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser)),
                                              SaleDocument.DocumentNumber,
                                              SaleDocument.InvoicingAddress_ThirdName,
                                              (RedWire.Action)ActionType,
                                              (RedWire.EquipmentType)SupportType,
                                              SupportModel,
                                              SupportState,
                                              Warranty,
                                              ProblemReproduced,
                                              Bag,
                                              Alimentation,
                                              Mouse,
                                              Battery,
                                              Other);

                redWire = RequestCenter.PostRedWire(redWire.Jsonify());

                List<SaleDocumentLine> saleLines = RequestCenter.GetSaleLines(SaleDocument.Id);

                BoolAttributeStringify(stringList);

                linesEdition(redWire, saleLines, stringList);

                if (OptionEBP != null)
                {
                    RequestCenter.PostDocument(new DocumentList(OptionEBP, redWire).Jsonify());
                }

                NavigateMainMenuCommand.Execute(new object());
            }
        }

        /// <summary>
        /// Format booleans to string for red wire comment lines
        /// </summary>
        /// <param name="stringList"></param>
        private void BoolAttributeStringify(List<string> stringList)
        {
            if (Bag) { stringList.Add("Matériel supplémentaire pris en charge : Sacoche"); }
            if (Alimentation) { stringList.Add("Matériel supplémentaire pris en charge : Alimentation"); }
            if (Mouse) { stringList.Add("Matériel supplémentaire pris en charge : Souris"); }
            if (Battery) { stringList.Add("Matériel supplémentaire pris en charge : Batterie"); }
            if (Other) { stringList.Add("Matériel supplémentaire pris en charge : " + OtherText); }
            if (Warranty) { stringList.Add("ATTENTION : Matériel sous garantie"); }
            else { stringList.Add("Matériel non garantie"); }
            if (ProblemReproduced) { stringList.Add("Problème reproduit avec le client lors de la prise en charge"); }
            else { stringList.Add("ATTENTION : Problème non reproduit avec le client lors de la prise en charge"); }
            if (ActionType != null) { stringList.Add("Type de dossier : " + ActionType); }
            if (SupportModel != null) { stringList.Add("Model de l'appareil : " + SupportModel); }
            stringList.Add("Dossier créé par : " + ActualUser.Name);
        }

        /// <summary>
        /// Check if fields are correctly fills
        /// </summary>
        /// <returns></returns>
        public (bool, List<string>) FieldsVerification()
        {
            List<string> stringList = new();

            if (SaleDocument == null)
            {
                PopUpCenter.MessagePopup("Erreur : Aucune Prise en charge importée.");
                return (false, null);
            }

            if (OptionEBP != null)
            {
                string[] strTab = { "FA", "DE", "AV" };
                (bool result, SaleDocument temp) = SaleDocument.FormatVerification(OptionEBP, strTab);
                if (!result)
                {
                    return (false, null);
                }
                else { OptionDocument = temp; }

                stringList.Add("Facture de diag/devis initial : " + OptionEBP);
            }
            if (actionType == null)
            {
                PopUpCenter.MessagePopup("Erreur : Selectionnez le type de dossier pris en charge.");
                return (false, null);
            }

            if (supportType == null)
            {
                PopUpCenter.MessagePopup("Erreur : Selectionnez le type de matériel pris en charge.");
                return (false, null);
            }

            if (supportState == null)
            {
                PopUpCenter.MessagePopup("Erreur : Décrivez l'état du matériel pris en charge.");
                return (false, null);
            }
            else { stringList.Add("État du matériel Client : " + supportState); }

            if (Other)
            {
                if (OtherText.Trim() == string.Empty)
                {
                    PopUpCenter.MessagePopup("Erreur : Case \"Autre\" cochée, mais champ de texte vide.");
                    return (false, null);
                }
            }

            if (supportModel == null)
            {
                PopUpCenter.MessagePopup("Erreur : Champ de texte vide. Donnez le modèle de l'appareil ou un équivalent.");
                return (false, null);
            }

            if (SupportModel.Length > 45)
            {
                PopUpCenter.MessagePopup("Erreur : Champ \"Modèle de l'appareil\" trop long (Taille Max 45).");
                return (false, null);
            }

            if (otherText.Length > 70)
            {
                PopUpCenter.MessagePopup("Erreur : Champ \"Autre\" trop long (Taille Max 70).");
                return (false, null);
            }

            if (SupportState.Length > 70)
            {
                PopUpCenter.MessagePopup("Erreur : Champ \"Etat du matériel\" trop long (Taille Max 70).");
                return (false, null);
            }

            return (true, stringList);
        }

        /// <summary>
        /// Check if some line skips exists in the text, format and generate red wire comment lines
        /// </summary>
        /// <param name="redWire"></param>
        /// <param name="saleLines"></param>
        /// <param name="stringList"></param>
        public void linesEdition(RedWire redWire, List<SaleDocumentLine> saleLines, List<string> stringList)
        {
            foreach (SaleDocumentLine line in saleLines)
            {
                if (line.DescriptionClear.Contains("\\r\\n"))
                {
                    List<string> strTab = line.DescriptionClear.Split("\\r\\n").ToList();
                    foreach(string str in strTab)
                    {
                        Evenement temp = new Evenement(redWire, Evenement.EventType.simpleText, str);
                        RequestCenter.PostEvent(temp.Jsonify());
                    }
                }
                else
                {
                    Evenement temp = new Evenement(redWire, Evenement.EventType.simpleText, line.DescriptionClear);
                    RequestCenter.PostEvent(temp.Jsonify());
                }
            }
            foreach (string line in stringList)
            {
                Evenement temp = new Evenement(redWire, Evenement.EventType.simpleText, line);

                RequestCenter.PostEvent(temp.Jsonify());
            }
        }

        private void UIUpdate()
        {
            ClientName = saleDocument.InvoicingAddress_ThirdName;
            PhoneNumber = saleDocument.InvoicingContact_Phone;
            CellPhoneNumber = saleDocument.InvoicingContact_CellPhone;
            Email = saleDocument.InvoicingContact_Email;
            Addresse = saleDocument.DeliveryAddress_Address1;
            City = saleDocument.DeliveryAddress_City;
            CP = saleDocument.DeliveryAddress_ZipCode;
            Department = saleDocument.DeliveryAddress_State;
            Country = saleDocument.InvoicingAddress_CountryIsoCode;
        }

        public string ClientName
        {
            get { return clientName; }
            set
            {
                clientName = value;
                OnPropertyChanged("ClientName");
            }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        public string CellPhoneNumber
        {
            get { return cellPhoneNumber; }
            set
            {
                cellPhoneNumber = value;
                OnPropertyChanged("CellPhoneNumber");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Addresse
        {
            get { return addresse; }
            set
            {
                addresse = value;
                OnPropertyChanged("Addresse");
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }

        public string CP
        {
            get { return cp; }
            set
            {
                cp = value;
                OnPropertyChanged("CP");
            }
        }

        public string Department
        {
            get { return department; }
            set
            {
                department = value;
                OnPropertyChanged("Department");
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                OnPropertyChanged("Country");
            }
        }

        public List<RedWire.Action> ActionTypeList
        {
            get
            {
                List<RedWire.Action> vs = new List<RedWire.Action>();
                foreach (RedWire.Action actionType in Enum.GetValues<RedWire.Action>())
                {
                    vs.Add(actionType);
                }

                return vs;
            }
        }

        public RedWire.Action? ActionType
        {
            get { return actionType; }
            set { actionType = value;
                OnPropertyChanged(nameof(ActionTypeVisibility));
            }
        }
        public bool ActionTypeVisibility { get => ActionType != null; }

        public List<RedWire.EquipmentType> SupportTypeList 
        {
            get
            {
                List<RedWire.EquipmentType> vs = new List<RedWire.EquipmentType>();
                foreach (RedWire.EquipmentType equipmentType in Enum.GetValues<RedWire.EquipmentType>())
                {
                    vs.Add(equipmentType);
                }
            
                return vs;
            }
        }

        public RedWire.EquipmentType? SupportType
        {
            get { return supportType; }
            set { supportType = value;
                OnPropertyChanged(nameof(SupportTypeVisibility));
            }
        }
        public bool SupportTypeVisibility { get => SupportType != null; }

        public string SupportModel
        {
            get { return supportModel; }
            set { supportModel = value;
                OnPropertyChanged(nameof(SupportModelVisibility));
            }
        }
        public bool SupportModelVisibility { get => SupportModel != null && SupportModel.Length > 0; }

        private bool Bag
        {
            get { return bag; }
            set { bag = value; }
        }

        private bool Alimentation
        {
            get { return alimentation; }
            set { alimentation = value; }
        }

        private bool Mouse
        {
            get { return mouse; }
            set { mouse = value; }
        }

        private bool Battery
        {
            get { return battery; }
            set { battery = value; }
        }

        private bool Other
        {
            get { return other; }
            set { other = value; }
        }

        public string OtherText
        {
            get { return otherText; }
            set { otherText = value; }
        }

        private bool Warranty
        {
            get { return warranty; }
            set { warranty = value; }
        }

        private bool ProblemReproduced
        {
            get { return problemReproduced; }
            set { problemReproduced = value; }
        }

        public string SupportState
        {
            get { return supportState; }
            set { supportState = value;
                OnPropertyChanged(nameof(SupportStateVisibility));
            }
        }
        public bool SupportStateVisibility { get => SupportState != null && SupportState.Length > 0; }

        /// <summary>
        /// Commands
        /// </summary>
        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
    }
}
