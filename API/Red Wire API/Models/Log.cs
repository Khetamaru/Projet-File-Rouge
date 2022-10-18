using Microsoft.EntityFrameworkCore;
using Red_Wire_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_File_Rouge.Object
{
    public class Log : BDDObject
    {
        public int id { get; set; }
        public string text { get; set; }
        public DateTime date { get; set; }
        public int type { get; set; }
        public User user { get; set; }

        public void PostChild(DbContext dbContext)
        {
            user.Post(dbContext);
        }

        public void Post(DbContext dbContext)
        {
            dbContext.Entry(this).State = EntityState.Detached;
            PostChild(dbContext);
        }
    }
}
