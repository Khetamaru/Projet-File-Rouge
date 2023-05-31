using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Projet_File_Rouge.Object
{
    public class Evenement : IBDDObject
    {
        [JsonIgnore]
        [JsonProperty]
        private readonly int id;
        [JsonProperty]
        public readonly DateTime date;
        [JsonProperty]
        public readonly RedWire redWire;
        [JsonProperty]
        public readonly EventType type;
        [JsonProperty]
        public string log;

        public enum EventType
        {
            simpleText
        }

        [JsonConstructor]
        public Evenement(int id, DateTime date, RedWire redWire, int type, string log)
            : this(redWire, type, log)
        {
            this.id = id;
            this.date = date;
        }

        public Evenement(RedWire _redWire, int _type, string _log)
        {
            date = DateTime.Now;
            redWire = _redWire;
            type = (EventType)_type;
            log = _log;
        }

        public Evenement(RedWire _redWire, EventType _type, string _log)
        {
            date = DateTime.Now;
            redWire = _redWire;
            type = _type;
            log = _log;
        }

        public DateTime Date { get => date; }
        public string DateFormated => Date.ToString("dd'-'MM'-'yyyy");
        public string Log { get => log; }

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
