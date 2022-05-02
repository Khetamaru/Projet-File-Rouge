using System;
using System.Collections.Generic;
using Projet_File_Rouge.EBPObject;
using Projet_File_Rouge.Object;

namespace Projet_File_Rouge.Tools
{
    static class Unjsonify
    {
        const string empty = "[]";

        public static SaleDocument SaleDocumentUnjsoning(string json)
        {
            Guid Id = new Guid();
            DateTime sysCreatedDate = new DateTime();
            DateTime sysModifiedDate = new DateTime();
            string DocumentNumber = string.Empty;
            string NumberPrefix = string.Empty;
            Decimal NumberSuffix = 0;
            string InvoicingAddress_ThirdName = string.Empty;
            string InvoicingContact_Phone = string.Empty;
            string InvoicingContact_CellPhone = string.Empty;
            string InvoicingContact_Email = string.Empty;
            string DeliveryAddress_Address1 = string.Empty;
            string DeliveryAddress_ZipCode = string.Empty;
            string DeliveryAddress_City = string.Empty;
            string DeliveryAddress_State = string.Empty;
            string InvoicingAddress_CountryIsoCode = string.Empty;
            Decimal CommitmentsBalanceDue = 0;
            Decimal TotalDueAmount = 0;

            string temp;

            int i = 0;

            string[] splitTab = json.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == SaleDocumentEnum.id.ToString())
                {
                    Id = GetGuid(splitTab[i + 2]);
                }
                else if (split == SaleDocumentEnum.sysCreatedDate.ToString())
                {
                    sysCreatedDate = GetDate(splitTab[i + 2]);
                }
                else if (split == SaleDocumentEnum.sysModifiedDate.ToString())
                {
                    sysModifiedDate = GetDate(splitTab[i + 2]);
                }
                else if (split == SaleDocumentEnum.documentNumber.ToString())
                {
                    DocumentNumber = splitTab[i + 2];
                }
                else if (split == SaleDocumentEnum.numberPrefix.ToString())
                {
                    NumberPrefix = splitTab[i + 2];
                }
                else if (split == SaleDocumentEnum.numberSuffix.ToString())
                {
                    temp = GetShort(splitTab[i + 1]);

                    NumberSuffix = decimal.Parse(temp.Replace('.', ','));
                }
                else if (split == SaleDocumentEnum.invoicingAddress_ThirdName.ToString())
                {
                    if (splitTab[i + 1] != ":null,") { InvoicingAddress_ThirdName = splitTab[i + 2]; }
                }
                else if (split == SaleDocumentEnum.invoicingContact_Phone.ToString())
                {
                    if (splitTab[i + 1] != ":null,") { InvoicingContact_Phone = splitTab[i + 2]; }
                }
                else if (split == SaleDocumentEnum.invoicingContact_CellPhone.ToString())
                {
                    if (splitTab[i + 1] != ":null,") { InvoicingContact_CellPhone = splitTab[i + 2]; }
                }
                else if (split == SaleDocumentEnum.invoicingContact_Email.ToString())
                {
                    if (splitTab[i + 1] != ":null,") { InvoicingContact_Email = splitTab[i + 2]; }
                }
                else if (split == SaleDocumentEnum.deliveryAddress_Address1.ToString())
                {
                    if (splitTab[i + 1] != ":null,") { DeliveryAddress_Address1 = splitTab[i + 2]; }
                }
                else if (split == SaleDocumentEnum.deliveryAddress_ZipCode.ToString())
                {
                    if (splitTab[i + 1] != ":null,") { DeliveryAddress_ZipCode = splitTab[i + 2]; }
                }
                else if (split == SaleDocumentEnum.deliveryAddress_City.ToString())
                {
                    if (splitTab[i + 1] != ":null,") { DeliveryAddress_City = splitTab[i + 2]; }
                }
                else if (split == SaleDocumentEnum.deliveryAddress_State.ToString())
                {
                    if (splitTab[i + 1] != ":null,") { DeliveryAddress_State = splitTab[i + 2]; }
                }
                else if (split == SaleDocumentEnum.invoicingAddress_CountryIsoCode.ToString())
                {
                    if (splitTab[i + 1] != ":null,") { InvoicingAddress_CountryIsoCode = splitTab[i + 2]; }
                }
                else if (split == SaleDocumentEnum.commitmentsBalanceDue.ToString()) 
                {
                    temp = GetShort(splitTab[i + 1]);

                    CommitmentsBalanceDue = decimal.Parse(temp.Replace('.', ','));
                }
                else if (split == SaleDocumentEnum.totalDueAmount.ToString())
                {
                    temp = GetShort(splitTab[i + 1]);

                    TotalDueAmount = decimal.Parse(temp.Replace('.', ','));
                }
                i++;
            }

            SaleDocument saleDocument = new SaleDocument(Id, 
                                                         sysCreatedDate, 
                                                         sysModifiedDate, 
                                                         DocumentNumber, 
                                                         NumberPrefix, 
                                                         NumberSuffix, 
                                                         InvoicingAddress_ThirdName, 
                                                         InvoicingContact_Phone, 
                                                         InvoicingContact_CellPhone, 
                                                         InvoicingContact_Email, 
                                                         DeliveryAddress_Address1,
                                                         DeliveryAddress_ZipCode, 
                                                         DeliveryAddress_City, 
                                                         DeliveryAddress_State,
                                                         InvoicingAddress_CountryIsoCode,
                                                         CommitmentsBalanceDue, 
                                                         TotalDueAmount);

            return saleDocument;
        }

        public static List<SaleDocument> SaleDocumentsUnjsoning(string json)
        {
            if (json == empty) { return new List<SaleDocument>(); }

            string[] objectTab = json.Split(new string[] { ",{" }, StringSplitOptions.None);
            List<SaleDocument> saleDocuments = new List<SaleDocument>();

            foreach (string jsonStr in objectTab)
            {
                saleDocuments.Add(SaleDocumentUnjsoning(jsonStr));
            }

            return saleDocuments;
        }

        public static SaleDocumentLine SaleDocumentLineUnjsoning(string json)
        {
            Guid documentId = new Guid();
            int LineOrder = 0;
            string DescriptionClear = string.Empty;

            string temp;

            int i = 0;

            string[] splitTab = json.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == SaleDocumentLineEnum.documentId.ToString())
                {
                    documentId = GetGuid(splitTab[i + 2]);
                }
                else if (split == SaleDocumentLineEnum.lineOrder.ToString())
                {
                    LineOrder = GetInt(splitTab[i + 1]);
                }
                else if (split == SaleDocumentLineEnum.descriptionClear.ToString())
                {
                    DescriptionClear = splitTab[i + 2];
                }
                i++;
            }

            SaleDocumentLine saleDocumentLine = new SaleDocumentLine(documentId, LineOrder/100, DescriptionClear);

            return saleDocumentLine;
        }

        public static List<SaleDocumentLine> SaleDocumentLinesUnjsoning(string json)
        {
            if (json == empty) { return new List<SaleDocumentLine>(); }

            string[] objectTab = json.Split(new string[] { ",{" }, StringSplitOptions.None);
            List<SaleDocumentLine> saleDocuments = new List<SaleDocumentLine>();

            foreach (string jsonStr in objectTab)
            {
                saleDocuments.Add(SaleDocumentLineUnjsoning(jsonStr));
            }

            return saleDocuments;
        }

        public static Evenement EventUnjsoning(string json)
        {
            int id = 42;
            RedWire redWire = null;
            Evenement.EventType type = Evenement.EventType.simpleText;
            string log = string.Empty;

            string temp;

            int i = 0;

            string[] splitTab = json.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == EventEnum.id.ToString())
                {
                    id = GetInt(splitTab[i + 1]);
                }
                if (split == EventEnum.redWire.ToString())
                {
                    temp = GetShort(splitTab[i + 1]);

                    redWire = RequestCenter.GetRedWire(Int32.Parse(temp));
                }
                if (split == EventEnum.type.ToString())
                {
                    type = (Evenement.EventType)GetInt(splitTab[i + 1]);
                }
                if (split == EventEnum.log.ToString())
                {
                    log = splitTab[i + 2];
                }
            }

            Evenement evenement = new Evenement(id, redWire, type, log);

            return evenement;
        }

        public static List<Evenement> EventsUnjsoning(string json)
        {
            if (json == empty) { return new List<Evenement>(); }

            string[] objectTab = json.Split(new string[] { ",{" }, StringSplitOptions.None);
            List<Evenement> events = new List<Evenement>();

            foreach (string jsonStr in objectTab)
            {
                events.Add(EventUnjsoning(jsonStr));
            }

            return events;
        }

        public static RedWire RedWireUnjsoning(string json)
        {
            int id = 42;
            DateTime inChargeDate = new DateTime();
            DateTime repairStartDate = new DateTime();
            DateTime repairEndDate = new DateTime();
            string client = null;
            User recorder = null;
            User actualRepairMan = null;
            RedWire.state actualState = RedWire.state.attente;
            RedWire.EquipmentType equipmentType = RedWire.EquipmentType.Autre;
            string model = string.Empty;
            string equipmentState = string.Empty;
            bool warranty = false;
            bool problemReproduced = false;
            bool bag = false;
            bool alimentation = false;
            bool mouse = false;
            bool battery = false;
            bool other = false;

            string temp;

            int i = 0;

            string[] splitTab = json.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == RedWireEnum.id.ToString())
                {
                    id = GetInt(splitTab[i + 1]);
                }
                if (split == RedWireEnum.inChargeDate.ToString())
                {
                    inChargeDate = GetDate(splitTab[i + 2]);
                }
                if (split == RedWireEnum.repairStartDate.ToString())
                {
                    repairStartDate = GetDate(splitTab[i + 2]);
                }
                if (split == RedWireEnum.repairEndDate.ToString())
                {
                    repairEndDate = GetDate(splitTab[i + 2]);
                }
                if (split == RedWireEnum.client.ToString())
                {
                    client = splitTab[i + 2];
                }
                if (split == RedWireEnum.recorder.ToString())
                {
                    recorder = RequestCenter.GetUser(GetInt(splitTab[i + 1]));
                }
                if (split == RedWireEnum.actualRepairMan.ToString())
                {
                    int? tempInt = GetIntNullable(splitTab[i + 1]);
                    actualRepairMan = tempInt == null ? null : RequestCenter.GetUser((int)tempInt);
                }
                if (split == RedWireEnum.actualState.ToString())
                {
                    actualState = (RedWire.state)GetInt(splitTab[i + 1]);
                }
                if (split == RedWireEnum.id.ToString())
                {
                    id = GetInt(splitTab[i + 1]);
                }
                if (split == RedWireEnum.type.ToString())
                {
                    equipmentType = (RedWire.EquipmentType)GetInt(splitTab[i + 1]);
                }
                if (split == RedWireEnum.model.ToString())
                {
                    model = splitTab[i + 2];
                }
                if (split == RedWireEnum.equipmentState.ToString())
                {
                    equipmentState = splitTab[i + 2];
                }
                if (split == RedWireEnum.warranty.ToString())
                {
                    warranty = GetBool(splitTab[i + 1]);
                }
                if (split == RedWireEnum.problemReproduced.ToString())
                {
                    problemReproduced = GetBool(splitTab[i + 1]);
                }
                if (split == RedWireEnum.bag.ToString())
                {
                    bag = GetBool(splitTab[i + 1]);
                }
                if (split == RedWireEnum.alimentation.ToString())
                {
                    alimentation = GetBool(splitTab[i + 1]);
                }
                if (split == RedWireEnum.mouse.ToString())
                {
                    mouse = GetBool(splitTab[i + 1]);
                }
                if (split == RedWireEnum.battery.ToString())
                {
                    battery = GetBool(splitTab[i + 1]);
                }
                if (split == RedWireEnum.other.ToString())
                {
                    other = GetBool(splitTab[i + 1]);
                }
                i++;
            }

            RedWire redWire = new RedWire(id, inChargeDate, repairStartDate, repairEndDate, client, recorder, actualRepairMan, actualState, equipmentType, model, equipmentState, warranty, problemReproduced, bag, alimentation, mouse, battery, other);

            return redWire;
        }

        public static List<RedWire> RedWiresUnjsoning(string json)
        {
            if (json == empty) { return new List<RedWire>(); }

            string[] objectTab = json.Split(new string[] { ",{" }, StringSplitOptions.None);
            List<RedWire> redWires = new List<RedWire>();

            foreach (string jsonStr in objectTab)
            {
                redWires.Add(RedWireUnjsoning(jsonStr));
            }

            return redWires;
        }

        public static User UserUnjsoning(string json)
        {
            int id = 42;
            string name = string.Empty;
            string password = string.Empty;
            User.AccessLevel accessLevel = User.AccessLevel.User;

            string temp;

            int i = 0;

            string[] splitTab = json.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == UserEnum.id.ToString())
                {
                    id = GetInt(splitTab[i + 1]);
                }
                if (split == UserEnum.name.ToString())
                {
                    name = splitTab[i + 2];
                }
                if (split == UserEnum.password.ToString())
                {
                    password = splitTab[i + 2];
                }
                if (split == UserEnum.accessLevel.ToString())
                {
                    accessLevel = (User.AccessLevel)GetInt(splitTab[i + 1]);
                }
                i++;
            }

            User user = new User(id, name, password, accessLevel);

            return user;
        }

        public static List<User> UsersUnjsoning(string json)
        {
            if (json == empty) { return new List<User>(); }

            json = json.Remove(0, 1);
            json = json.Remove(json.Length - 1, 1);
            string[] objectTab = json.Split(new string[] { ",{" }, StringSplitOptions.None);

            List<User> users = new List<User>();

            foreach (string jsonStr in objectTab)
            {
                users.Add(UserUnjsoning(jsonStr));
            }

            return users;
        }

        public static UserHistory UserHistoryUnjsoning(string json)
        {
            int id = 42;
            DateTime time = new DateTime();
            User user = null;

            string temp;

            int i = 0;

            string[] splitTab = json.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == UserHistoryEnum.id.ToString())
                {
                    id = GetInt(splitTab[i + 1]);
                }
                if (split == UserHistoryEnum.time.ToString())
                {
                    time = GetDate(splitTab[i + 2]);
                }
                if (split == UserHistoryEnum.user.ToString())
                {
                    user = RequestCenter.GetUser(GetInt(splitTab[i + 1]));
                }
            }

            UserHistory userHistory = new UserHistory(id, time, user);

            return userHistory;
        }

        public static List<UserHistory> UserHistorysUnjsoning(string json)
        {
            if (json == empty) { return new List<UserHistory>(); }

            string[] objectTab = json.Split(new string[] { ",{" }, StringSplitOptions.None);
            List<UserHistory> userHistories = new List<UserHistory>();

            foreach (string jsonStr in objectTab)
            {
                userHistories.Add(UserHistoryUnjsoning(jsonStr));
            }

            return userHistories;
        }

        public static int GetInt(string json)
        {
            json = GetShort(json);

            return Int32.Parse(json);
        }

        public static int? GetIntNullable(string json)
        {
            json = GetShort(json);

            return json == "null" ? null : Int32.Parse(json);
        }

        public static bool GetBool(string json)
        {
            json = GetShort(json);

            return Convert.ToBoolean(json);
        }

        public static DateTime GetDate(string json)
        {
            return Convert.ToDateTime(json);
        }

        public static Guid GetGuid(string json)
        {
            return Guid.Parse(json);
        }

        public static string GetShort(string json)
        {
            json = json.Remove(0, 1);
            json = json.Remove(json.Length - 1, 1);

            return json;
        }
    }
}
