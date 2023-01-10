using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_File_Rouge.Object
{
    public class Log : BDDObject
    {
        [JsonIgnore]
        [JsonProperty]
        private readonly int id;
        [JsonProperty]
        private string text;
        [JsonProperty]
        private DateTime date;
        [JsonProperty]
        private LogTypeEnum type;
        [JsonProperty]
        private readonly User user;

        public enum LogTypeEnum
        {
            User,
            RedWire,
            CommandList,
            Version,
            BDD
        }

        [JsonConstructor]
        public Log(int id, string text, DateTime date, LogTypeEnum type, User user)
        {
            this.id = id;
            this.text = text;
            this.date = date;
            this.type = type;
            this.user = user;
        }

        public Log(string _text, DateTime _date, LogTypeEnum _type, User _user)
            : this(0, _text, _date, _type, _user)
        {
        }

        public int Id => id;
        public string Text => text;
        public DateTime Date => date;
        public string DateFormated => Date.ToString("dd'-'MM'-'yyyy");
        public LogTypeEnum Type => type;
        public User User => user;

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
