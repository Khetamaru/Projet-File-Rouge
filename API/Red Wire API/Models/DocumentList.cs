using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class DocumentList
    {
        public int id { get; set; }

        public int documentId { get; set; }
        public virtual RedWire redWireId { get; set; }
    }
}
