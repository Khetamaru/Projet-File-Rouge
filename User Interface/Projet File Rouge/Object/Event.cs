using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_File_Rouge.Object
{
    class Event : BDDObject
    {
        private readonly int id;

        public Event(int _id)
        {
            id = _id;
        }

        public int Id { get => id; }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + EventEnum.id + "\" : " + Id +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "}";
        }
    }

    public enum EventEnum
    {
        id
    }
}
