using Microsoft.EntityFrameworkCore;
using Red_Wire_API.Models;
using System;

namespace Projet_File_Rouge.Object
{
    public class CommandList : BDDObject
    {
        public int id { get; set; }

        public DateTime deliveryDate { get; set; }
        public string name { get; set; }
        public RedWire redWire { get; set; }
        public int state { get; set; }
        public string url { get; set; }

        public void PostChild(DbContext dbContext)
        {
            redWire.Post(dbContext);
        }

        public void Post(DbContext dbContext)
        {
            dbContext.Entry(this).State = EntityState.Detached;
            PostChild(dbContext);
        }
    }
}
