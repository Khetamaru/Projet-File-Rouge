using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.Object
{
    public class RedWire : BDDObject
    {
        [JsonIgnore]
        [JsonProperty]
        private readonly int id;

        [JsonProperty]
        private DateTime lastUpdate;
        [JsonProperty]
        private readonly DateTime inChargeDate;
        [JsonProperty]
        private DateTime repairStartDate;
        [JsonProperty]
        private DateTime repairEndDate;

        [JsonProperty]
        private readonly string client;
        [JsonProperty]
        private readonly string clientName;

        [JsonProperty]
        private readonly User recorder;
        [JsonProperty]
        private User actualRepairMan;
        [JsonProperty]
        private User transfertTarget;

        [JsonProperty]
        private state actualState;

        [JsonProperty]
        private Action actionType;

        [JsonProperty]
        private EquipmentType type;
        [JsonProperty]
        private readonly string model;

        [JsonProperty]
        private readonly bool bag;
        [JsonProperty]
        private readonly bool alimentation;
        [JsonProperty]
        private readonly bool mouse;
        [JsonProperty]
        private readonly bool battery;
        [JsonProperty]
        private readonly bool other;

        [JsonProperty]
        private bool warranty;
        [JsonProperty]
        private readonly bool problemReproduced;

        [JsonProperty]
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
            fin,
            non_réparable_appel,
            non_réparable_rendu,
            abandon,
            attente_fournisseur,
            payement_différé
        }

        public enum Action
        {
            commande,
            diag,
            SAV,
            presta
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

        [JsonConstructor]
        public RedWire(int id, User recorder, string client, string clientName, Action actionType, EquipmentType type, string model, string equipmentState, bool warranty, bool problemReproduced, bool bag, bool alimentation, bool mouse, bool battery, bool other)
        {
            this.id = id;
            this.recorder = recorder;
            this.client = client;
            this.clientName = clientName;

            this.actionType = actionType;
            this.type = type;
            this.model = model;
            this.equipmentState = equipmentState;
            this.warranty = warranty;
            this.problemReproduced = problemReproduced;
            this.bag = bag;
            this.alimentation = alimentation;
            this.mouse = mouse;
            this.battery = battery;
            this.other = other;

            inChargeDate = DateTime.Now;
            lastUpdate = DateTime.Now;
            actualState = state.libre;
        }

        public RedWire(int _id, DateTime _lastUpdate, DateTime _inChargeDate, DateTime _repairStartDate, DateTime _repairEndDate, string _client, string _clientName, User _recorder, User _actualRepairMan, User _transfertTarget, RedWire.state _actualState, Action actionType, EquipmentType _type, string _model, string _equipmentState, bool _warranty, bool _problemReproduced, bool _bag, bool _alimentation, bool _mouse, bool _battery, bool _other) 
            : this(_id, _recorder, _client, _clientName, actionType, _type, _model, _equipmentState, _warranty, _problemReproduced, _bag, _alimentation, _mouse, _battery, _other)
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

        public int Id { get => id; }
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
        public User ActualRepairMan { get => actualRepairMan; set => actualRepairMan = value; }
        public User TransfertTarget { get => transfertTarget; set => transfertTarget = value; }
        public state ActualState { get => actualState; set => actualState = value; }
        public Action ActionType { get => actionType; set => actionType = value; }
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

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
