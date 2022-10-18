using Local_API_Server.Models;
using Microsoft.EntityFrameworkCore;
using Red_Wire_API.Models;
using System;

namespace Projet_File_Rouge.Object
{
    public class UserHistoryList: BDDObject
    {
        public int id { get; set; }

        public DateTime time { get; set; }
        public User user { get; set; }
        public RedWire redWire { get; set; }

        public void PostChild(DbContext controllerBase)
        {
            user.Post(controllerBase);
            redWire.Post(controllerBase);
        }
        public void Post(DbContext controllerBase)
        {
            controllerBase.Entry(this).State = EntityState.Detached;
            PostChild(controllerBase);
        }
    }
}
