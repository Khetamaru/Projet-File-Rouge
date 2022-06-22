using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_File_Rouge.Object
{
    public class Log : BDDObject
    {
        private readonly int id;
        private string text;
        private DateTime date;
        private LogTypeEnum type;
        private readonly User user;

        public enum LogTypeEnum
        {
            User,
            RedWire,
            CommandList,
            Version
        }

        public Log(int _id, string _text, DateTime _date, LogTypeEnum _type, User _user)
        {
            id = _id;
            text = _text;
            date = _date;
            type = _type;
            user = _user;
        }

        public Log(string _text, DateTime _date, LogTypeEnum _type, User _user)
            : this(42, _text, _date, _type, _user)
        {
        }

        public int Id => id;
        public string Text => text;
        public DateTime Date => date;
        public string DateFormated => Date.ToString("yyyy'-'MM'-'dd");
        public LogTypeEnum Type => type;
        public User User => user;

        public string JsonifyId()
        {
            return "{" +
                   "\"" + LogEnum.id + "\" : " + Id + "," +
                   "\"" + LogEnum.text + "\" : \"" + Text + "\"," +
                   "\"" + LogEnum.date + "\" : \"" + Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + LogEnum.type + "\" : " + (int)Type + "," +
                   "\"" + LogEnum.user + "\" : " + User.Id +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "\"" + LogEnum.text + "\" : \"" + Text + "\"," +
                   "\"" + LogEnum.date + "\" : \"" + Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + LogEnum.type + "\" : " + (int)Type + "," +
                   "\"" + LogEnum.user + "\" : " + User.Id +
                   "}";
        }
    }

    public enum LogEnum
    {
        id,
        text,
        date,
        type,
        user
    }
}
