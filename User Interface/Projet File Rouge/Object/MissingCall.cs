using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_File_Rouge.Object
{
    public class MissingCall : BDDObject
    {
        private readonly int id;
        private User author;
        private User recipient;
        private string caller;
        private string message;
        private DateTime date;
        private bool read;

        public MissingCall(User _author, User _recipient, string _caller, string _message, bool _read)
        {
            id = 42;
            author = _author;
            recipient = _recipient;
            caller = _caller;
            message = _message;
            date = DateTime.Now;
            read = _read;
        }

        public MissingCall(int _id, User _author, User _recipient, string _caller, string _message, DateTime _date, bool _read)
            : this (_author, _recipient, _caller, _message, _read)
        {
            id = _id;
            date = _date;
        }

        public int Id { get => id; }
        public User Author { get => author; }
        public User Recipient { get => recipient; }
        public string Caller { get => caller; set => caller = value; }
        public string Message { get => message; set => message = value; }
        public DateTime Date { get => date; set => date = value; }
        public bool Read { get => read; set => read = value; }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + MissingCallEnum.id + "\" : " + Id + "," +
                   "\"" + MissingCallEnum.author + "\" : " + Author.Id + "," +
                   "\"" + MissingCallEnum.recipient + "\" : " + Recipient.Id + "," +
                   "\"" + MissingCallEnum.caller + "\" : \"" + Caller + "\"," +
                   "\"" + MissingCallEnum.message + "\" : \"" + Message + "\"," +
                   "\"" + MissingCallEnum.date + "\" : \"" + Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + MissingCallEnum.read + "\" : " + Read.ToString().ToLower() +
                   "}";
        }
        public string Jsonify()
        {
            return "{" +
                   "\"" + MissingCallEnum.author + "\" : " + Author.Id + "," +
                   "\"" + MissingCallEnum.recipient + "\" : " + Recipient.Id + "," +
                   "\"" + MissingCallEnum.caller + "\" : \"" + Caller + "\"," +
                   "\"" + MissingCallEnum.message + "\" : \"" + Message + "\"," +
                   "\"" + MissingCallEnum.date + "\" : \"" + Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + MissingCallEnum.read + "\" : " + Read.ToString().ToLower() +
                   "}";
        }
    }

    public enum MissingCallEnum
    {
        id,
        author,
        recipient,
        caller,
        message,
        date,
        read
    }
}
