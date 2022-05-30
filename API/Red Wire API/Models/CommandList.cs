using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class CommandList
    {
        public int id { get; set; }

        public DateTime deliveryDate { get; set; }
        public string name { get; set; }
        public int redWire { get; set; }
        public int state { get; set; }
        public string url { get; set; }
    }
}
