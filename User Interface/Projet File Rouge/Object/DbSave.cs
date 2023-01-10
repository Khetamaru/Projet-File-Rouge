using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class DbSave : BDDObject
    {
        [JsonIgnore]
        [JsonProperty]
        private readonly int id;

        [JsonProperty]
        internal readonly DateTime date;

        public DbSave(DateTime _date) : this(0, _date) { }

        [JsonConstructor]
        public DbSave(int id, DateTime date) {

            this.id = id;
            this.date = date;
        }

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}