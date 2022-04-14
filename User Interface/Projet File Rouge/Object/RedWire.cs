using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    class RedWire : BDDObject
    {
        private readonly int? id;

        private readonly DateTime inChargeDate;
        private DateTime repairStartDate;
        private DateTime repairEndDate;

        private readonly Client client;
        private readonly Equipment equipmentToRepair;

        private readonly List<Document> documentLibrary;

        private readonly User recorder;
        private readonly List<UserHistory> repairmenHistory;

        private state actualState;

        private readonly List<Event> eventList;

        public enum state
        {
            création,
            attente,
            réparation,
            fini
        }

        public RedWire(User _recorder, Client _client) 
            : this(null, _recorder, _client) { }

        public RedWire(int? _id, User _recorder, Client _client)
        {
            id = _id;
            recorder = _recorder;
            client = _client;

            inChargeDate = DateTime.Now;
            actualState = state.création;
        }

        public RedWire(int? _id, DateTime _inChargeDate, DateTime _repairStartDate, DateTime _repairEndDate, Client _client, Equipment _equipmentToRepair, List<Document> _documentLibrary, User _recorder, List<UserHistory> _repairmenHistory, RedWire.state _actualState, List<Event> _eventList) 
            : this(_id, _recorder, _client)
        {
            inChargeDate = _inChargeDate;
            repairStartDate = _repairStartDate;
            repairEndDate = _repairEndDate;
            equipmentToRepair = _equipmentToRepair;
            documentLibrary = _documentLibrary;
            repairmenHistory = _repairmenHistory;
            actualState = _actualState;
            eventList = _eventList;
        }

        public int Id { get => id == null ? -1 : (int)id; }
        public DateTime InChargeDate { get => inChargeDate; }
        public DateTime RepairStartDate { get => repairStartDate; set => repairStartDate = value; }
        public DateTime RepairEndDate { get => repairEndDate; set => repairEndDate = value; }
        public Client Client { get => client; }
        public Equipment EquipmentToRepair { get => equipmentToRepair; }
        public List<Document> DocumentLibrary { get => documentLibrary; }
        public User Recorder { get => recorder; }
        public List<UserHistory> RepairmenHistory { get => repairmenHistory; }
        public state ActualState { get => actualState; set => actualState = value; }
        public List<Event> EventList { get => eventList; }

        public void addDocumentLibrary(Document document)
        {
            documentLibrary.Add(document);
        }
        public void addEventList(Event evenement) 
        {
            eventList.Add(evenement);
        }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + RedWireEnum.id + "\" : " + Id + "," +
                   "\"" + RedWireEnum.inChargeDate + "\" : \"" + InChargeDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + RedWireEnum.repairStartDate + "\" : \"" + RepairStartDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + RedWireEnum.repairEndDate + "\" : \"" + RepairEndDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + RedWireEnum.client + "\" : " + Client.Id + "," +
                   "\"" + RedWireEnum.equipmentToRepair + "\" : " + EquipmentToRepair.Id + "," +
                   "\"" + RedWireEnum.documentLibrary + "\" : \"" + JsonifyDocumentLibrary() + "\"," +
                   "\"" + RedWireEnum.recorder + "\" : " + Recorder.Id + "," +
                   "\"" + RedWireEnum.repairmenHistory + "\" : \"" + JsonifyRepairmenHistory() + "\"," +
                   "\"" + RedWireEnum.actualState + "\" : " + ((int)ActualState) + "," +
                   "\"" + RedWireEnum.eventList + "\" : \"" + JsonifyEventList() + "\"" +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "\"" + RedWireEnum.inChargeDate + "\" : \"" + InChargeDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + RedWireEnum.repairStartDate + "\" : \"" + RepairStartDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + RedWireEnum.repairEndDate + "\" : \"" + RepairEndDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + RedWireEnum.client + "\" : " + Client.Id + "," +
                   "\"" + RedWireEnum.equipmentToRepair + "\" : " + EquipmentToRepair.Id + "," +
                   "\"" + RedWireEnum.documentLibrary + "\" : \"" + JsonifyDocumentLibrary() + "\"," +
                   "\"" + RedWireEnum.recorder + "\" : " + Recorder.Id + "," +
                   "\"" + RedWireEnum.repairmenHistory + "\" : \"" + JsonifyRepairmenHistory() + "\"," +
                   "\"" + RedWireEnum.actualState + "\" : " + ((int)ActualState) + "," +
                   "\"" + RedWireEnum.eventList + "\" : \"" + JsonifyEventList() + "\"" +
                   "}";
        }

        public string JsonifyDocumentLibrary()
        {
            int i = 0;
            string str = "{";
            foreach(Document document in DocumentLibrary)
            {
                str += document.JsonifyId();
                if (i < DocumentLibrary.Count - 1)
                {
                    str += ",";
                }
                i++;
            }
            return str + "}";
        }

        public string JsonifyRepairmenHistory()
        {
            int i = 0;
            string str = "{";
            foreach (UserHistory userHistory in RepairmenHistory)
            {
                str += userHistory.JsonifyId();
                if (i < RepairmenHistory.Count - 1)
                {
                    str += ",";
                }
                i++;
            }
            return str + "}";
        }

        public string JsonifyEventList()
        {
            int i = 0;
            string str = "{";
            foreach (Event evenement in EventList)
            {
                str += evenement.JsonifyId();
                if (i < EventList.Count - 1)
                {
                    str += ",";
                }
                i++;
            }
            return str + "}";
        }
    }

    public enum RedWireEnum
    {
        id,
        inChargeDate,
        repairStartDate,
        repairEndDate,
        client,
        equipmentToRepair,
        documentLibrary,
        recorder,
        repairmenHistory,
        actualState,
        eventList
    }
}
