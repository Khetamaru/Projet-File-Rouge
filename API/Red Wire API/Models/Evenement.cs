using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class Evenement
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int redWire { get; set; }
        public int type { get; set; }
        public string log { get; set; }
    }
}
