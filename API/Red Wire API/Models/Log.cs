using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class Log
    {
        public int id { get; set; }

        public string text { get; set; }
        public DateTime date { get; set; }
        public int type { get; set; }
        public int user { get; set; }
    }
}
