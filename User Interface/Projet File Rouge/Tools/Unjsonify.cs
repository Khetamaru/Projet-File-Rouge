using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet_File_Rouge.Object;

namespace Projet_File_Rouge.Tools
{
    static class Unjsonify
    {
        const string empty = "[]";

        public static Client ClientUnjsoning(string json)
        {
            int id = 42;
            string name = string.Empty;
            string email = string.Empty;
            string phoneNumber = string.Empty;
            string address = string.Empty;

            string temp;

            int i = 0;

            string[] splitTab = json.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == ClientEnum.id.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    id = Int32.Parse(temp);
                }
                else if (split == ClientEnum.name.ToString())
                {
                    name = splitTab[i + 2];
                }
                else if (split == ClientEnum.email.ToString())
                {
                    email = splitTab[i + 2];
                }
                else if (split == ClientEnum.phoneNumber.ToString())
                {
                    phoneNumber = splitTab[i + 2];
                }
                else if (split == ClientEnum.address.ToString())
                {
                    address = splitTab[i + 2];
                }
                i++;
            }
            Client client = new Client(id, name, email, phoneNumber, address);

            return client;
        }

        public static List<Client> ClientsUnjsoning(string json)
        {
            if (json == empty) { return new List<Client>(); }

            string[] objectTab = json.Split(new string[] { ",{" }, StringSplitOptions.None);
            List<Client> clients = new List<Client>();

            foreach(string jsonStr in objectTab)
            {
                clients.Add(ClientUnjsoning(jsonStr));
            }

            return clients;
        }

        public static Document DocumentUnjsoning(string json)
        {
            int id = 42;
            Document.DocType docType = Document.DocType.Devis;
            DateTime date = DateTime.Now;
            User author = null;
            Client client = null;

            string temp;

            int i = 0;

            string[] splitTab = json.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == DocumentEnum.id.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    id = Int32.Parse(temp);
                }
                else if (split == DocumentEnum.documentType.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    docType = (Document.DocType)Int32.Parse(temp);
                }
                else if (split == DocumentEnum.date.ToString())
                {
                    date = Convert.ToDateTime(splitTab[i + 2]);
                }
                else if (split == DocumentEnum.author.ToString())
                {
                    author = UserUnjsoning(splitTab[i + 2]);
                }
                else if (split == DocumentEnum.client.ToString())
                {
                    client = ClientUnjsoning(splitTab[i + 2]);
                }
                i++;
            }

            Document document = new Document(id, docType, date, author, client);

            return document;
        }

        public static List<Document> DocumentsUnjsoning(string json)
        {
            if (json == empty) { return new List<Document>(); }

            string[] objectTab = json.Split(new string[] { ",{" }, StringSplitOptions.None);
            List<Document> documents = new List<Document>();

            foreach (string jsonStr in objectTab)
            {
                documents.Add(DocumentUnjsoning(jsonStr));
            }

            return documents;
        }

        public static Equipment EquipmentUnjsoning(string json)
        {
            int id = 42;
            Equipment.EquipmentType equipmentType = Equipment.EquipmentType.Autre;
            string model = string.Empty;
            string description = string.Empty;
            bool warranty = false;
            bool problemReproduced = false;
            bool bag = false;
            bool charger = false;
            bool mouse = false;
            bool battery = false;
            bool other = false;

            string temp;

            int i = 0;

            string[] splitTab = json.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == EquipmentEnum.id.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    id = Int32.Parse(temp);
                }
                if (split == EquipmentEnum.type.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    equipmentType = (Equipment.EquipmentType)Int32.Parse(temp);
                }
                if (split == EquipmentEnum.model.ToString())
                {
                    model = splitTab[i + 2];
                }
                if (split == EquipmentEnum.description.ToString())
                {
                    description = splitTab[i + 2];
                }
                if (split == EquipmentEnum.warranty.ToString())
                {
                    warranty = Convert.ToBoolean(splitTab[i + 2]);
                }
                if (split == EquipmentEnum.problemReproduced.ToString())
                {
                    problemReproduced = Convert.ToBoolean(splitTab[i + 2]);
                }
                if (split == EquipmentEnum.bag.ToString())
                {
                    bag = Convert.ToBoolean(splitTab[i + 2]);
                }
                if (split == EquipmentEnum.charger.ToString())
                {
                    charger = Convert.ToBoolean(splitTab[i + 2]);
                }
                if (split == EquipmentEnum.mouse.ToString())
                {
                    mouse = Convert.ToBoolean(splitTab[i + 2]);
                }
                if (split == EquipmentEnum.battery.ToString())
                {
                    battery = Convert.ToBoolean(splitTab[i + 2]);
                }
                if (split == EquipmentEnum.other.ToString())
                {
                    other = Convert.ToBoolean(splitTab[i + 2]);
                }
            }

            Equipment equipment = new Equipment(id, equipmentType, model, description, warranty, problemReproduced, bag, charger, mouse, battery, other);

            return equipment;
        }

        public static List<Equipment> EquipmentsUnjsoning(string json)
        {
            if (json == empty) { return new List<Equipment>(); }

            string[] objectTab = json.Split(new string[] { ",{" }, StringSplitOptions.None);
            List<Equipment> equipments = new List<Equipment>();

            foreach (string jsonStr in objectTab)
            {
                equipments.Add(EquipmentUnjsoning(jsonStr));
            }

            return equipments;
        }

        public static Event EventUnjsoning(string json)
        {
            int id = 42;

            string temp;

            int i = 0;

            string[] splitTab = json.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == EquipmentEnum.id.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    id = Int32.Parse(temp);
                }
            }

            Event evenement = new Event(id);

            return evenement;
        }

        public static List<Event> EventsUnjsoning(string json)
        {
            if (json == empty) { return new List<Event>(); }

            string[] objectTab = json.Split(new string[] { ",{" }, StringSplitOptions.None);
            List<Event> events = new List<Event>();

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
            Client client = null;
            Equipment equipmentToRepair = null;
            List<Document> documentLibrary = null;
            User recorder = null;
            List<UserHistory> repairmenHistory = null;
            RedWire.state actualState = RedWire.state.attente;
            List<Event> eventList = null;

            string temp;

            int i = 0;

            string[] splitTab = json.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == RedWireEnum.id.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    id = Int32.Parse(temp);
                }
                if (split == RedWireEnum.inChargeDate.ToString())
                {
                    inChargeDate = Convert.ToDateTime(splitTab[i + 2]);
                }
                if (split == RedWireEnum.repairStartDate.ToString())
                {
                    repairStartDate = Convert.ToDateTime(splitTab[i + 2]);
                }
                if (split == RedWireEnum.repairEndDate.ToString())
                {
                    repairEndDate = Convert.ToDateTime(splitTab[i + 2]);
                }
                if (split == RedWireEnum.client.ToString())
                {
                    client = ClientUnjsoning(splitTab[i + 2]);
                }
                if (split == RedWireEnum.equipmentToRepair.ToString())
                {
                    equipmentToRepair = EquipmentUnjsoning(splitTab[i + 2]);
                }
                if (split == RedWireEnum.documentLibrary.ToString())
                {
                    documentLibrary = DocumentsUnjsoning(splitTab[i + 2]);
                }
                if (split == RedWireEnum.recorder.ToString())
                {
                    recorder = UserUnjsoning(splitTab[i + 2]);
                }
                if (split == RedWireEnum.repairmenHistory.ToString())
                {
                    repairmenHistory = UserHistorysUnjsoning(splitTab[i + 2]);
                }
                if (split == RedWireEnum.actualState.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    actualState = (RedWire.state)Int32.Parse(temp);
                }
                if (split == RedWireEnum.eventList.ToString())
                {
                    eventList = EventsUnjsoning(splitTab[i + 2]);
                }
            }

            RedWire redWire = new RedWire(id, inChargeDate, repairStartDate, repairEndDate, client, equipmentToRepair, documentLibrary, recorder, repairmenHistory, actualState, eventList);

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
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    id = Int32.Parse(temp);
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
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    accessLevel = (User.AccessLevel)Int32.Parse(temp);
                }
                i++;
            }

            User user = new User(id, name, accessLevel);

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
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    id = Int32.Parse(temp);
                }
                if (split == UserHistoryEnum.time.ToString())
                {
                    time = Convert.ToDateTime(splitTab[i + 2]);
                }
                if (split == UserHistoryEnum.user.ToString())
                {
                    user = UserUnjsoning(splitTab[i + 2]);
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
    }
}
