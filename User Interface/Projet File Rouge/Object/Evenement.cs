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
        public readonly DateTime date;
        public readonly int redWireId;
        public readonly EventType type;
        public readonly string log;

        public enum EventType
        {
            simpleText
        }

        public Evenement(int _id, DateTime _date, int _redWireId, EventType _type, string _log)
            : this(_redWireId, _type, _log)
        {
            id = _id;
            date = _date;
        }

        public Evenement(int _redWire, EventType _type, string _log)
        {
            date = DateTime.Now;
            redWireId = _redWire;
            type = _type;
            log = _log;
        }

        public int Id { get => id; }
        public DateTime Date { get => date; }
        public int RedWireId { get => redWireId; }
        public EventType Type { get => type; }
        public string Log { get => log; }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + EventEnum.id + "\" : " + Id + "," +
                   "\"" + EventEnum.date + "\" : \"" + Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + EventEnum.redWire + "\" : " + RedWireId + "," +
                   "\"" + EventEnum.type + "\" : " + (int)Type + "," +
                   "\"" + EventEnum.log + "\" : \"" + Log + "\"" +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "\"" + EventEnum.date + "\" : \"" + Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + EventEnum.redWire + "\" : " + RedWireId + "," +
                   "\"" + EventEnum.type + "\" : " + (int)Type + "," +
                   "\"" + EventEnum.log + "\" : \"" + Log + "\"" +
                   "}";
        }
    }

    public enum EventEnum
    {
        id,
        date,
        redWire,
        type,
        log
    }
}
