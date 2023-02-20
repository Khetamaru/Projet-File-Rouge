using System;
using System.Linq;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.EBPObject
{
    public class SaleDocument
    {
        public Guid Id { get; set; }

        public DateTime sysCreatedDate { get; set; }
        public DateTime sysModifiedDate { get; set; }

        public string DocumentNumber { get; set; }
        public string NumberPrefix { get; set; }
        public Decimal NumberSuffix { get; set; }

        public string InvoicingAddress_ThirdName { get; set; }
        public string InvoicingContact_Phone { get; set; }
        public string InvoicingContact_CellPhone { get; set; }
        public string InvoicingContact_Email { get; set; }
        public string DeliveryAddress_Address1 { get; set; }
        public string DeliveryAddress_ZipCode { get; set; }
        public string DeliveryAddress_City { get; set; }
        public string DeliveryAddress_State { get; set; }
        public string InvoicingAddress_CountryIsoCode { get; set; }

        public Decimal CommitmentsBalanceDue { get; set; }
        public Decimal TotalDueAmount { get; set; }
        public string TotalDueAmountFormated { get => Math.Round(TotalDueAmount, 2) + " €"; }

        public SaleDocument(Guid Id,
                            DateTime sysCreatedDate,
                            DateTime sysModifiedDate,
                            string DocumentNumber,
                            string NumberPrefix,
                            Decimal NumberSuffix,
                            string InvoicingAddress_ThirdName,
                            string InvoicingContact_Phone,
                            string InvoicingContact_CellPhone,
                            string InvoicingContact_Email,
                            string DeliveryAddress_Address1,
                            string DeliveryAddress_ZipCode,
                            string DeliveryAddress_City,
                            string DeliveryAddress_State,
                            string InvoicingAddress_CountryIsoCode,
                            Decimal CommitmentsBalanceDue,
                            Decimal TotalDueAmount)
        {
            this.Id = Id;
            this.sysCreatedDate = sysCreatedDate;
            this.sysModifiedDate = sysModifiedDate;
            this.DocumentNumber = DocumentNumber;
            this.NumberPrefix = NumberPrefix;
            this.NumberSuffix = NumberSuffix;
            this.InvoicingAddress_ThirdName = InvoicingAddress_ThirdName;
            this.InvoicingContact_Phone = InvoicingContact_Phone;
            this.InvoicingContact_CellPhone = InvoicingContact_CellPhone;
            this.InvoicingContact_Email = InvoicingContact_Email;
            this.DeliveryAddress_Address1 = DeliveryAddress_Address1;
            this.DeliveryAddress_ZipCode = DeliveryAddress_ZipCode;
            this.DeliveryAddress_City = DeliveryAddress_City;
            this.DeliveryAddress_State = DeliveryAddress_State;
            this.InvoicingAddress_CountryIsoCode = InvoicingAddress_CountryIsoCode;
            this.CommitmentsBalanceDue = CommitmentsBalanceDue;
            this.TotalDueAmount = TotalDueAmount;
        }

        public string DetailInfos()
        {
            return "Date de prise en charge : " + sysCreatedDate + "\n" +
                   "Numéro de Prise en charge : " + DocumentNumber + "\n" +
                   "Nom du client : " + InvoicingAddress_ThirdName + "\n" +
                   "Téléphone : " + InvoicingContact_Phone + "\n" +
                   "Portable : " + InvoicingContact_CellPhone + "\n" +
                   "Mail : " + InvoicingContact_Email + "\n" +
                   "Adresse : " + DeliveryAddress_Address1 + "\n" +
                   DeliveryAddress_City + " - " + DeliveryAddress_ZipCode + " - " + DeliveryAddress_State;
        }

        public static bool IsValidPrefix(string prefixToTest)
        {
            foreach (PrefixEnum prefixEnum in Enum.GetValues<PrefixEnum>())
            {
                if (prefixToTest == prefixEnum.ToString()) { return true; }
            }

            return false;
        }

        public static bool IsValidPrefix(string prefixToTest, string[] strTab)
        {
            foreach (string str in strTab)
            {
                if (prefixToTest == str) { return true; }
            }

            return false;
        }

        public static (bool, SaleDocument) FormatVerification(string strEBP, string[] strTab)
        {
            if (strEBP == null)
            {
                PopUpCenter.MessagePopup("Veuillez rentrer un numéro EBP avant d'importer les informations.");
                return (false, null);
            }

            if (strEBP.Length != 10)
            {
                PopUpCenter.MessagePopup("Taille du numéro EBP incorrecte (exemple de numéro EBP : \"FA00001123\").");
                return (false, null);
            }

            string prefix = strEBP.Substring(0, 2);
            string suffix = strEBP.Substring(2, 8);

            if (!SaleDocument.IsValidPrefix(prefix, strTab))
            {
                PopUpCenter.MessagePopup("Prefix de numéro EBP invalide et/ou non autorisé (exemple de numéro EBP : \"FA00001123\").");
                return (false, null);
            }
            if (!suffix.All(char.IsDigit))
            {
                PopUpCenter.MessagePopup("Suffix de numéro EBP invalide (exemple de numéro EBP : \"FA00001123\").");
                return (false, null);
            }

            SaleDocument temp = RequestCenter.GetSaleDocument(strEBP);
            if (temp == null)
            {
                PopUpCenter.MessagePopup("Ce numéro EBP n'existe pas.");
                return (false, null);
            }

            return (true, temp);
        }
    }

    public enum SaleDocumentEnum
    {
        id,
        sysCreatedDate,
        sysModifiedDate,
        documentNumber,
        numberPrefix,
        numberSuffix,
        invoicingAddress_ThirdName,
        invoicingContact_Phone,
        invoicingContact_CellPhone,
        invoicingContact_Email,
        deliveryAddress_Address1,
        deliveryAddress_ZipCode,
        deliveryAddress_City,
        deliveryAddress_State,
        invoicingAddress_CountryIsoCode,
        commitmentsBalanceDue,
        totalDueAmount
    }

    public enum PrefixEnum
    {
        FA,
        CM,
        DE,
        AV
    }
}
