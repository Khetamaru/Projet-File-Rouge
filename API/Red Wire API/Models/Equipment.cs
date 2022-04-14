using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class Equipment
    {
        public int id { get; set; }

        public int equipmentType { get; set; }
        public string model { get; set; }
        public string description { get; set; }
        public bool warranty { get; set; }
        public bool problemReproduced { get; set; }
        public bool bag { get; set; }
        public bool charger { get; set; }
        public bool mouse { get; set; }
        public bool battery { get; set; }
        public bool other { get; set; }
    }
}
