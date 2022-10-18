using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Red_Wire_API.Models;
using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class User : BDDObject
    {
        public int id { get; set; }

        public string name { get; set; }
        public string password { get; set; }
        public int accessLevel { get; set; }
        public bool activated { get; set; }

        public void PostChild(DbContext dbContext) { }

        public void Post(DbContext dbContext)
        {
            dbContext.Entry(this).State = EntityState.Detached;
            PostChild(dbContext);
        }
    }
}
