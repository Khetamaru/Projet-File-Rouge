using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    class UserHistory : BDDObject
    {
        private readonly int? id;

        private readonly DateTime time;

        private readonly User user;

        public UserHistory(DateTime _time, User _user) : this(null, _time, _user) { }

        public UserHistory(int? _id, DateTime _time, User _user) {

            id = _id;
            time = _time;
            user = _user;
        }

        public int Id { get => id != null ? (int)id : -1; }
        public DateTime Time { get => time; }
        public User User { get => user; }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + UserHistoryEnum.id + "\" : " + Id + "," +
                   "\"" + UserHistoryEnum.time + "\" : \"" + Time.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + UserHistoryEnum.user + "\" : " + User.Id +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "\"" + UserHistoryEnum.time + "\" : \"" + Time.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + UserHistoryEnum.user + "\" : " + User.Id +
                   "}";
        }
    }

    public enum UserHistoryEnum
    {
        id,
        time,
        user
    }
}