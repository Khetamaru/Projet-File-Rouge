using System;

namespace Projet_File_Rouge.Object
{
    public class UserHistoryList
    {
        public int id { get; set; }

        public DateTime time { get; set; }
        public virtual User userId { get; set; }
        public virtual RedWire redWireId { get; set; }
    }
}
