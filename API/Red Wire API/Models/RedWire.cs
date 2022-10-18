using Local_API_Server.Controllers;
using Microsoft.EntityFrameworkCore;
using Red_Wire_API.Models;
using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class RedWire : BDDObject
    {
        public int id { get; set; }

        public DateTime lastUpdate { get; set; }
        public DateTime inChargeDate { get; set; }
        public DateTime repairStartDate { get; set; }
        public DateTime repairEndDate { get; set; }

        public string client { get; set; }
        public string clientName { get; set; }
        public User recorder { get; set; }
        public User? actualRepairMan { get; set; }
        public User? transfertTarget { get; set; }
        public int actualState { get; set; }

        public int actionType { get; set; }
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

        public void PostChild(DbContext dbContext)
        {
            recorder.Post(dbContext);
            if (actualRepairMan != null) actualRepairMan.Post(dbContext);
            if (transfertTarget != null) { transfertTarget.Post(dbContext); }
        }

        public void Post(DbContext dbContext)
        {
            dbContext.Entry(this).State = EntityState.Detached;
            PostChild(dbContext);
        }
    }
}
