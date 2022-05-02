using System;

namespace Projet_File_Rouge.Object
{
    public class UserHistoryList
    {
        public int id { get; set; }

        public DateTime time { get; set; }
        public int userId { get; set; }
        public int redWireId { get; set; }
    }
}
