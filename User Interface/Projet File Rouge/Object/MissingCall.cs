using Newtonsoft.Json;
using System;

namespace Projet_File_Rouge.Object
{
    public class MissingCall : BDDObject
    {
        [JsonIgnore]
        [JsonProperty]
        private readonly int id;
        private User author;
        private User recipient;
        private string caller;
        private string message;
        private DateTime date;
        private bool read;

        public MissingCall(User _author, User _recipient, string _caller, string _message, bool _read)
        {
            author = _author;
            recipient = _recipient;
            caller = _caller;
            message = _message;
            date = DateTime.Now;
            read = _read;
        }

        [JsonConstructor]
        public MissingCall(int id, User author, User recipient, string caller, string message, DateTime date, bool read)
            : this (author, recipient, caller, message, read)
        {
            this.id = id;
            this.date = date;
        }

        public int Id { get => id; }
        public User Author { get => author; }
        public User Recipient { get => recipient; }
        public string Caller { get => caller; set => caller = value; }
        public string Message { get => message; set => message = value; }
        public DateTime Date { get => date; set => date = value; }
        public bool Read { get => read; set => read = value; }
        public string Seen { get => Read ? "Oui" : "Non"; }

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
