using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class RedWire
    {
        public int id { get; set; }

        public DateTime inChargeDate { get; set; }
        public DateTime repairStartDate { get; set; }
        public DateTime repairEndDate { get; set; }

        public int clientId { get; set; }
        public virtual Equipment equipmentId { get; set; }
        public virtual User recorderId { get; set; }
        public int actualState { get; set; }
    }
}
