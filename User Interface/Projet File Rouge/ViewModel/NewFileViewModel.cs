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

        private RedWire.EquipmentType? supportType;
        private string supportModel;
        private bool bag;
        private bool alimentation;
        private bool mouse;
        private bool battery;
        private bool other;
        private bool warranty;
        private bool problemReproduced;
        private string supportState;

        public NewFileViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);
        }

        public bool FormatVerification(string strEBP, string[] strTab)
        {
            if (strEBP == null) 
            {
                PopUpCenter.MessagePopup("Veuillez rentrer un numéro EBP avant d'importer les informations.");
                return false;
            }

            if (strEBP.Length != 10)
            {
                PopUpCenter.MessagePopup("Taille du numéro EBP incorrecte (exemple de numéro EBP : \"FA00001123\").");
                return false;
            }

            string prefix = strEBP.Substring(0, 2);
            string suffix = strEBP.Substring(2, 8);

            if (!SaleDocument.IsValidPrefix(prefix, strTab))
            {
                PopUpCenter.MessagePopup("Prefix de numéro EBP invalide et/ou non autorisé (exemple de numéro EBP : \"FA00001123\").");
                return false;
            }
            if (!suffix.All(char.IsDigit))
            {
                PopUpCenter.MessagePopup("Suffix de numéro EBP invalide (exemple de numéro EBP : \"FA00001123\").");
                return false;
            }

            return true;
        }

        public void ChargeSaleDocument(object sender, RoutedEventArgs e, string[] strTab)
        {
            if (FormatVerification(inChargeNumber, strTab))
            {
                SaleDocument temp = RequestCenter.GetSaleDocument(InChargeNumber);

                if (temp != null)
                { 
                    SaleDocument = temp;
                    PopUpCenter.MessagePopup("Informations importées.");
                }
                else { PopUpCenter.MessagePopup("Ce numéro EBP n'existe pas."); }
            }
        }

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
            set { inChargeNumber = value; }
        }

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
            set { optionEBP = value; }
        }

        public SaleDocument OptionDocument
        {
            get { return optionDocument; }
            set
            {
                optionDocument = value;
            }
        }

        public void RedWireCreation()
        {
            if (FieldsVerification())
            {
                RedWire redWire = new RedWire(null,
                                              NavigateMainMenuCommand.cacheStore.GetCache(CacheStoreEnum.ActualUser) as User,
                                              SaleDocument.DocumentNumber,
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

                linesEdition(redWire, saleLines);

                NavigateMainMenuCommand.Execute(new object());
            }
        }

        public bool FieldsVerification()
        {
            if (SaleDocument == null)
            {
                PopUpCenter.MessagePopup("Erreur : Aucune Prise en charge importée.");
                return false;
            }

            if (OptionEBP != null)
            {
                string[] strTab = { "FA", "DE" };
                if (!FormatVerification(OptionEBP, strTab))
                {
                    return false;
                }
                else
                {
                    SaleDocument temp = RequestCenter.GetSaleDocument(OptionEBP);

                    if (temp == null)
                    {
                        return false;
                    }
                    else { OptionDocument = temp; }
                }
            }

            if (supportType == null)
            {
                PopUpCenter.MessagePopup("Erreur : Selectionnez le type de matériel pris en charge.");
                return false;
            }

            if (supportState == null)
            {
                PopUpCenter.MessagePopup("Erreur : Décrivez l'état du matériel pris en charge.");
                return false;
            }

            return true;
        }

        public void linesEdition(RedWire redWire, List<SaleDocumentLine> saleLines)
        {
            foreach (SaleDocumentLine line in saleLines)
            {
                Evenement temp = new Evenement(redWire, Evenement.EventType.simpleText, line.DescriptionClear);

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
            set { supportType = value; }
        }

        public string SupportModel
        {
            get { return supportModel; }
            set { supportModel = value; }
        }

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
            set { supportState = value; }
        }

        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
    }
}
