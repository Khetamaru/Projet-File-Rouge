using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class UserHistory : BDDObject
    {
        private readonly int? id;

        private readonly DateTime time;

        private readonly User user;

        private readonly RedWire redWire;

        public UserHistory(DateTime _time, User _user, RedWire _redWire) : this(null, _time, _user, _redWire) { }

        public UserHistory(int? _id, DateTime _time, User _user, RedWire _redWire) {

            id = _id;
            time = _time;
            user = _user;
            redWire = _redWire;
        }

        public int Id { get => id != null ? (int)id : -1; }
        public DateTime Time { get => time; }
        public string TimeFormated { get => Time.ToString("dd'/'MM'/'yy' 'HH':'mm"); }
        public User User { get => user; }
        public RedWire RedWire { get => redWire; }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + UserHistoryEnum.id + "\" : " + Id + "," +
                   "\"" + UserHistoryEnum.time + "\" : \"" + Time.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + UserHistoryEnum.user + "\" : " + User.Id + "," +
                   "\"" + UserHistoryEnum.redWire + "\" : " + RedWire.Id +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "\"" + UserHistoryEnum.time + "\" : \"" + Time.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + UserHistoryEnum.user + "\" : " + User.Id + "," +
                   "\"" + UserHistoryEnum.redWire + "\" : " + RedWire.Id +
                   "}";
        }
    }

    public enum UserHistoryEnum
    {
        id,
        time,
        user,
        redWire
    }
}