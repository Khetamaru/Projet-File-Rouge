using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class EventList
    {
        public int id { get; set; }

        public virtual Event eventId { get; set; }
        public virtual RedWire redWireId { get; set; }
    }
}
