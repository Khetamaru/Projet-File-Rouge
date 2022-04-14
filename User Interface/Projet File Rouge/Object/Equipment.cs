
namespace Projet_File_Rouge.Object
{
    class Equipment : BDDObject
    {
        private readonly int id;

        private EquipmentType type;

        private readonly string model;
        private string description;

        private bool warranty;
        private readonly bool problemReproduced;

        private readonly bool bag;
        private readonly bool charger;
        private readonly bool mouse;
        private readonly bool battery;
        private readonly bool other;

        public enum EquipmentType
        {
            Portable,
            Tour,
            AllInOne,
            Téléphone,
            Iphone,
            Tablette,
            Autre
        }

        public Equipment(int _id, EquipmentType _type, string _model, string _description, bool _warranty, bool _problemReproduced, bool _bag, bool _charger, bool _mouse, bool _battery, bool _other)
        {
            id = _id;

            type = _type;
            model = _model;
            description = _description;
            warranty = _warranty;
            problemReproduced = _problemReproduced;
            bag = _bag;
            charger = _charger;
            mouse = _mouse;
            battery = _battery;
            other = _other;
        }

        public int Id { get => id; }
        public EquipmentType Type { get => type; set => type = value; }
        public string Model { get => model; }
        public string Description { get => description; set => description = value; }
        public bool Warranty { get => warranty; set => warranty = value; }
        public bool ProblemReproduced { get => problemReproduced; }
        public bool Bag { get => bag; }
        public bool Charger { get => charger; }
        public bool Mouse { get => mouse; }
        public bool Battery { get => battery; }
        public bool Other { get => other; }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + EquipmentEnum.id + "\" : " + Id + "," +
                   "\"" + EquipmentEnum.type + "\" : " + ((int)Type) + "," +
                   "\"" + EquipmentEnum.model + "\" : \"" + Model + "\"," +
                   "\"" + EquipmentEnum.description + "\" : \"" + Description + "\"," +
                   "\"" + EquipmentEnum.warranty + "\" : " + Warranty.ToString().ToLower() + "," +
                   "\"" + EquipmentEnum.problemReproduced + "\" : " + ProblemReproduced.ToString().ToLower() + "," +
                   "\"" + EquipmentEnum.bag + "\" : " + Bag.ToString().ToLower() + "," +
                   "\"" + EquipmentEnum.charger + "\" : " + Charger.ToString().ToLower() + "," +
                   "\"" + EquipmentEnum.mouse + "\" : " + Mouse.ToString().ToLower() + "," +
                   "\"" + EquipmentEnum.battery + "\" : " + Battery.ToString().ToLower() + "," +
                   "\"" + EquipmentEnum.other + "\" : " + Other.ToString().ToLower() +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "\"" + EquipmentEnum.type + "\" : " + ((int)Type) + "," +
                   "\"" + EquipmentEnum.model + "\" : \"" + Model + "\"," +
                   "\"" + EquipmentEnum.description + "\" : \"" + Description + "\"," +
                   "\"" + EquipmentEnum.warranty + "\" : " + Warranty.ToString().ToLower() + "," +
                   "\"" + EquipmentEnum.problemReproduced + "\" : " + ProblemReproduced.ToString().ToLower() + "," +
                   "\"" + EquipmentEnum.bag + "\" : " + Bag.ToString().ToLower() + "," +
                   "\"" + EquipmentEnum.charger + "\" : " + Charger.ToString().ToLower() + "," +
                   "\"" + EquipmentEnum.mouse + "\" : " + Mouse.ToString().ToLower() + "," +
                   "\"" + EquipmentEnum.battery + "\" : " + Battery.ToString().ToLower() + "," +
                   "\"" + EquipmentEnum.other + "\" : " + Other.ToString().ToLower() +
                   "}";
        }
    }

    public enum EquipmentEnum
    {
        id,
        type,
        model,
        description,
        warranty,
        problemReproduced,
        bag,
        charger,
        mouse,
        battery,
        other
    }
}
