﻿using Microsoft.EntityFrameworkCore;
using Red_Wire_API.Models;
using System;

namespace Projet_File_Rouge.Object
{
    public class Version : BDDObject
    {
        public int id { get; set; }
        public string versionNumber { get; set; }
        public DateTime date { get; set; }
        public bool urgent { get; set; }

        public void PostChild(DbContext dbContext) { }

        public void Post(DbContext dbContext)
        {
            dbContext.Entry(this).State = EntityState.Detached;
            PostChild(dbContext);
        }
    }
}
