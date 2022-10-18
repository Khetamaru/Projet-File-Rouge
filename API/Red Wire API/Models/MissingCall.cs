using Microsoft.EntityFrameworkCore;
using Red_Wire_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_File_Rouge.Object
{
    public class MissingCall : BDDObject
    {
        public int id { get; set; }

        public User author { get; set; }
        public User recipient { get; set; }
        public string caller { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
        public bool read { get; set; }

        public void PostChild(DbContext dbContext)
        {
            author.Post(dbContext);
            recipient.Post(dbContext);
        }

        public void Post(DbContext dbContext)
        {
            dbContext.Entry(this).State = EntityState.Detached;
            PostChild(dbContext);
        }
    }
}
