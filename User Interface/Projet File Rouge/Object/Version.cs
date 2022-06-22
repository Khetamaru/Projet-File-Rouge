

using System;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.Object
{
    public class Version : BDDObject
    {
        private readonly int? id;

        private string versionNumber;
        private DateTime date;

        public Version(string _versionNumber, DateTime _date) : this(null, _versionNumber, _date) { }

        public Version(int? _id, string _versionNumber, DateTime _date)
        {
            id = _id;
            versionNumber = _versionNumber;
            date = _date;
        }

        public int Id { get => id == null ? -1 : (int)id; }
        public string VersionNumber { get => versionNumber; set => versionNumber = value; }
        public DateTime Date { get => date; set => date = value; }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + VersionEnum.id + "\" : " + Id + "," +
                   "\"" + VersionEnum.versionNumber + "\" : \"" + VersionNumber + "\"," +
                   "\"" + VersionEnum.date + "\" : \"" + Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"" +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "\"" + VersionEnum.versionNumber + "\" : \"" + VersionNumber + "\"," +
                   "\"" + VersionEnum.date + "\" : \"" + Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"" +
                   "}";
        }
    }

    public enum VersionEnum
    {
        id,
        versionNumber,
        date
    }
}
