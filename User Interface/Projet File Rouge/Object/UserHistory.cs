using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class UserHistory : BDDObject
    {
        [JsonIgnore]
        [JsonProperty]
        private readonly int id;

        [JsonProperty]
        private readonly DateTime time;

        [JsonProperty]
        private readonly User user;

        [JsonProperty]
        private readonly RedWire redWire;

        public UserHistory(DateTime _time, User _user, RedWire _redWire) : this(0, _time, _user, _redWire) { }

        [JsonConstructor]
        public UserHistory(int id, DateTime time, User user, RedWire redWire) {

            this.id = id;
            this.time = time;
            this.user = user;
            this.redWire = redWire;
        }

        public DateTime Time => time;
        public string TimeFormated => Time.ToString("dd'-'MM'-'yyyy");
        public User User => user;
        public RedWire RedWire => redWire;

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}