using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class User
    {
        public int id { get; set; }

        public string name { get; set; }
        public string password { get; set; }
        public int accessLevel { get; set; }
        public bool activated { get; set; }
    }
}
