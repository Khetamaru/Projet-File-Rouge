using Microsoft.EntityFrameworkCore;
using Red_Wire_API.Models;
using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class DocumentList : BDDObject
    {
        public int id { get; set; }

        public string document { get; set; }
        public RedWire redWire { get; set; }

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
