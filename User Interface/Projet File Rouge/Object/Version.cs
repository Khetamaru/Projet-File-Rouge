

using System;
using Newtonsoft.Json;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.Object
{
    public class Version : BDDObject
    {
        [JsonProperty]
        private readonly int id;
        [JsonProperty]
        private string versionNumber;
        [JsonProperty]
        private DateTime date;

        public Version(string versionNumber, DateTime date) : this(0, versionNumber, date) { }

        [JsonConstructor]
        public Version(int id, string versionNumber, DateTime date)
        {
            this.id = id;
            this.versionNumber = versionNumber;
            this.date = date;
        }

        public string VersionNumber { get => versionNumber; set => versionNumber = value; }
        public DateTime Date { get => date; set => date = value; }

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
