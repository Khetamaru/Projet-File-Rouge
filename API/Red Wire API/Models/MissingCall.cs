using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class MissingCall
    {
        public int id { get; set; }

        public int author { get; set; }
        public int recipient { get; set; }
        public string caller { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
        public bool read { get; set; }
    }
}
