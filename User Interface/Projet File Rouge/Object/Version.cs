using System;
using Newtonsoft.Json;

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
        [JsonProperty]
        private bool urgent;

        public Version(string versionNumber, DateTime date, bool urgent) : this(0, versionNumber, date, urgent) { }

        [JsonConstructor]
        public Version(int id, string versionNumber, DateTime date, bool urgent)
        {
            this.id = id;
            this.versionNumber = versionNumber;
            this.date = date;
            this.urgent = urgent;
        }

        public int Id { get => id; }
        public string VersionNumber { get => versionNumber; set => versionNumber = value; }
        public DateTime Date { get => date; set => date = value; }
        public bool Ugent { get => urgent; set => urgent = value; }

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
