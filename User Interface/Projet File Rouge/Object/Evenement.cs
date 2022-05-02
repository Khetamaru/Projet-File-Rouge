using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_File_Rouge.Object
{
    public class Evenement : BDDObject
    {
        private readonly int id;
        public readonly RedWire redWire;
        public readonly EventType type;
        public readonly string log;

        public enum EventType
        {
            simpleText
        }

        public Evenement(int _id, RedWire _redWire, EventType _type, string _log)
            : this(_redWire, _type, _log)
        {
            id = _id;
        }

        public Evenement(RedWire _redWire, EventType _type, string _log)
        {
            redWire = _redWire;
            type = _type;
            log = _log;
        }

        public int Id { get => id; }
        public RedWire RedWire { get => redWire; }
        public EventType Type { get => type; }
        public string Log { get => log; }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + EventEnum.id + "\" : " + Id + "," +
                   "\"" + EventEnum.redWire + "\" : " + RedWire.Id + "," +
                   "\"" + EventEnum.type + "\" : " + (int)Type + "," +
                   "\"" + EventEnum.log + "\" : \"" + Log + "\"" +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "\"" + EventEnum.redWire + "\" : " + RedWire.Id + "," +
                   "\"" + EventEnum.type + "\" : " + (int)Type + "," +
                   "\"" + EventEnum.log + "\" : \"" + Log + "\"" +
                   "}";
        }
    }

    public enum EventEnum
    {
        id,
        redWire,
        type,
        log
    }
}
