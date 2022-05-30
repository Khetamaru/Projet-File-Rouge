using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class RedWire
    {
        public int id { get; set; }

        public DateTime lastUpdate { get; set; }
        public DateTime inChargeDate { get; set; }
        public DateTime repairStartDate { get; set; }
        public DateTime repairEndDate { get; set; }

        public string client { get; set; }
        public string clientName { get; set; }
        public int recorder { get; set; }
        public int? actualRepairMan { get; set; }
        public int? transfertTarget { get; set; }
        public int actualState { get; set; }

        public int type { get; set; }
        public string model { get; set; }
        public bool bag { get; set; }
        public bool alimentation { get; set; }
        public bool mouse { get; set; }
        public bool battery { get; set; }
        public bool other { get; set; }
        public bool warranty { get; set; }
        public bool problemReproduced { get; set; }
        public string equipmentState { get; set; }
    }
}
