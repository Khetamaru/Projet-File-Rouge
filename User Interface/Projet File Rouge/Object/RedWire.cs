using System;
using System.Collections.Generic;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.Object
{
    public class RedWire : BDDObject
    {
        private readonly int? id;

        private DateTime lastUpdate;
        private readonly DateTime inChargeDate;
        private DateTime repairStartDate;
        private DateTime repairEndDate;

        private readonly string client;
        private readonly string clientName;

        private readonly User recorder;
        private User actualRepairMan;
        private User transfertTarget;

        private state actualState;

        private EquipmentType type;
        private readonly string model;

        private readonly bool bag;
        private readonly bool alimentation;
        private readonly bool mouse;
        private readonly bool battery;
        private readonly bool other;

        private bool warranty;
        private readonly bool problemReproduced;

        private readonly string equipmentState;

        public enum state
        {
            libre,
            début,
            début_diag,
            début_reponse_client,
            en_cours,
            attente_commande,
            attente_client,
            transit,
            fin_facture_OK,
            fin_appel_OK,
            fin_payement_OK,
            fin,
            non_réparable_appel,
            non_réparable_rendu,
            abandon,
            attente_fournisseur
        }

        public enum EquipmentType
        {
            Ordinateur_Portable,
            Tour,
            AllInOne,
            Téléphone,
            Iphone,
            Tablette,
            Console,
            Autre
        }

        public RedWire(int? _id, User _recorder, string _client, string _clientName, EquipmentType _type, string _model, string _equipmentState, bool _warranty, bool _problemReproduced, bool _bag, bool _alimentation, bool _mouse, bool _battery, bool _other)
        {
            id = _id;
            recorder = _recorder;
            client = _client;
            clientName = _clientName;

            type = _type;
            model = _model;
            equipmentState = _equipmentState;
            warranty = _warranty;
            problemReproduced = _problemReproduced;
            bag = _bag;
            alimentation = _alimentation;
            mouse = _mouse;
            battery = _battery;
            other = _other;

            inChargeDate = DateTime.Now;
            lastUpdate = DateTime.Now;
            actualState = state.libre;
        }

        public RedWire(int? _id, DateTime _lastUpdate, DateTime _inChargeDate, DateTime _repairStartDate, DateTime _repairEndDate, string _client, string _clientName, User _recorder, User _actualRepairMan, User _transfertTarget, RedWire.state _actualState, EquipmentType _type, string _model, string _equipmentState, bool _warranty, bool _problemReproduced, bool _bag, bool _alimentation, bool _mouse, bool _battery, bool _other) 
            : this(_id, _recorder, _client, _clientName, _type, _model, _equipmentState, _warranty, _problemReproduced, _bag, _alimentation, _mouse, _battery, _other)
        {
            inChargeDate = _inChargeDate;
            repairStartDate = _repairStartDate;
            repairEndDate = _repairEndDate;
            actualState = _actualState;
            actualRepairMan = _actualRepairMan;
            transfertTarget = _transfertTarget;
            lastUpdate = _lastUpdate;
        }

        public void RedWireUpdate() { LastUpdate = DateTime.Now; }

        public int Id { get => id == null ? -1 : (int)id; }
        public DateTime LastUpdate { get => lastUpdate; set => lastUpdate = value; }
        public string LastUpdateFormated { get => LastUpdate.ToString("dd'/'MM'/'yy' 'HH':'mm"); }
        public DateTime InChargeDate { get => inChargeDate; }
        public string InChargeDateFormated { get => InChargeDate.ToString("dd'/'MM'/'yy' 'HH':'mm"); }
        public DateTime RepairStartDate { get => repairStartDate; set => repairStartDate = value; }
        public string RepairStartDateFormated { get => RepairStartDate.ToString("dd'/'MM'/'yy' 'HH':'mm"); }
        public DateTime RepairEndDate { get => repairEndDate; set => repairEndDate = value; }
        public string RepairEndDateFormated
        {
            get
            {
                if (RepairEndDate != new DateTime())
                {
                    return RepairEndDate.ToString("dd'/'MM'/'yy' 'HH':'mm");
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string Client { get => client; }
        public string ClientName { get => clientName; }
        public User Recorder { get => recorder; }
        public User ActualRepairMan { get => actualRepairMan; set => actualRepairMan = value; }
        public User TransfertTarget { get => transfertTarget; set => transfertTarget = value; }

        public string ActualRepairManId {
            get {
                if (ActualRepairMan == null) 
                { 
                    return "null"; 
                }
                return ActualRepairMan.Id.ToString();
            }
        }
        public string TransfertTargetId
        {
            get {
                if (TransfertTarget == null)
                {
                    return "null";
                }
                return TransfertTarget.Id.ToString();
            }
        }
        public state ActualState { get => actualState; set => actualState = value; }
        public EquipmentType Type { get => type; set => type = value; }
        public string Model { get => model; }
        public string EquipmentState { get => equipmentState; }
        public bool Warranty { get => warranty; set => warranty = value; }
        public bool ProblemReproduced { get => problemReproduced; }
        public bool Bag { get => bag; }
        public bool Alimentation { get => alimentation; }
        public bool Mouse { get => mouse; }
        public bool Battery { get => battery; }
        public bool Other { get => other; }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + RedWireEnum.id + "\" : " + Id + "," +
                   "\"" + RedWireEnum.lastUpdate + "\" : \"" + LastUpdate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + RedWireEnum.inChargeDate + "\" : \"" + InChargeDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + RedWireEnum.repairStartDate + "\" : \"" + RepairStartDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + RedWireEnum.repairEndDate + "\" : \"" + RepairEndDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + RedWireEnum.client + "\" : \"" + Client + "\"," +
                   "\"" + RedWireEnum.clientName + "\" : \"" + ClientName + "\"," +
                   "\"" + RedWireEnum.recorder + "\" : " + Recorder.Id + "," +
                   "\"" + RedWireEnum.actualRepairMan + "\" : " + ActualRepairManId + "," +
                   "\"" + RedWireEnum.transfertTarget + "\" : " + TransfertTargetId + "," +
                   "\"" + RedWireEnum.actualState + "\" : " + (int)ActualState + "," +
                   "\"" + RedWireEnum.type + "\" : " + (int)Type + "," +
                   "\"" + RedWireEnum.model + "\" : \"" + Model + "\"," +
                   "\"" + RedWireEnum.equipmentState + "\" : \"" + EquipmentState + "\"," +
                   "\"" + RedWireEnum.warranty + "\" : " + Warranty.ToString().ToLower() + "," +
                   "\"" + RedWireEnum.problemReproduced + "\" : " + ProblemReproduced.ToString().ToLower() + "," +
                   "\"" + RedWireEnum.bag + "\" : " + Bag.ToString().ToLower() + "," +
                   "\"" + RedWireEnum.alimentation + "\" : " + Alimentation.ToString().ToLower() + "," +
                   "\"" + RedWireEnum.mouse + "\" : " + Mouse.ToString().ToLower() + "," +
                   "\"" + RedWireEnum.battery + "\" : " + Battery.ToString().ToLower() + "," +
                   "\"" + RedWireEnum.other + "\" : " + Other.ToString().ToLower() +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "\"" + RedWireEnum.lastUpdate + "\" : \"" + LastUpdate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + RedWireEnum.inChargeDate + "\" : \"" + InChargeDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + RedWireEnum.repairStartDate + "\" : \"" + RepairStartDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + RedWireEnum.repairEndDate + "\" : \"" + RepairEndDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + RedWireEnum.client + "\" : \"" + Client + "\"," +
                   "\"" + RedWireEnum.clientName + "\" : \"" + ClientName + "\"," +
                   "\"" + RedWireEnum.recorder + "\" : " + Recorder.Id + "," +
                   "\"" + RedWireEnum.actualRepairMan + "\" : " + ActualRepairManId + "," +
                   "\"" + RedWireEnum.transfertTarget + "\" : " + TransfertTargetId + "," +
                   "\"" + RedWireEnum.actualState + "\" : " + ((int)ActualState) + "," +
                   "\"" + RedWireEnum.type + "\" : " + ((int)Type) + "," +
                   "\"" + RedWireEnum.model + "\" : \"" + Model + "\"," +
                   "\"" + RedWireEnum.equipmentState + "\" : \"" + EquipmentState + "\"," +
                   "\"" + RedWireEnum.warranty + "\" : " + Warranty.ToString().ToLower() + "," +
                   "\"" + RedWireEnum.problemReproduced + "\" : " + ProblemReproduced.ToString().ToLower() + "," +
                   "\"" + RedWireEnum.bag + "\" : " + Bag.ToString().ToLower() + "," +
                   "\"" + RedWireEnum.alimentation + "\" : " + Alimentation.ToString().ToLower() + "," +
                   "\"" + RedWireEnum.mouse + "\" : " + Mouse.ToString().ToLower() + "," +
                   "\"" + RedWireEnum.battery + "\" : " + Battery.ToString().ToLower() + "," +
                   "\"" + RedWireEnum.other + "\" : " + Other.ToString().ToLower() +
                   "}";
        }
    }

    public enum RedWireEnum
    {
        id,
        lastUpdate,
        inChargeDate,
        repairStartDate,
        repairEndDate,
        client,
        clientName,
        recorder,
        actualRepairMan,
        transfertTarget,
        actualState,
        type,
        model,
        equipmentState,
        warranty,
        problemReproduced,
        bag,
        alimentation,
        mouse,
        battery,
        other
    }
}
