

using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.Object
{
    public class User : BDDObject
    {
        private readonly int? id;

        private string name;
        private string password;
        private AccessLevel accessLevel;
        private bool activated;

        public enum AccessLevel
        {
            Intern,
            User,
            SuperUser,
            Admin
        }

        public User(string _name, string _password, AccessLevel _accessLevel) : this(null, _name, _password, _accessLevel, true) { }

        public User(int? _id, string _name, string _password, AccessLevel _accessLevel, bool _activated)
        {
            id = _id;
            name = _name;
            password = _password;
            accessLevel = _accessLevel;
            activated = _activated;
        }

        public int Id { get => id == null ? -1 : (int)id; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public AccessLevel UserLevel { get => accessLevel; set => accessLevel = value; }
        public bool Activated { get => activated; set => activated = value; }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + UserEnum.id + "\" : " + Id + "," +
                   "\"" + UserEnum.name + "\" : \"" + Name + "\"," +
                   "\"" + UserEnum.password + "\" : \"" + Password + "\"," +
                   "\"" + UserEnum.accessLevel + "\" : \"" + (int)UserLevel + "\"," +
                   "\"" + UserEnum.activated + "\" : " + Activated.ToString().ToLower() +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "\"" + UserEnum.name + "\" : \"" + Name + "\"," +
                   "\"" + UserEnum.password + "\" : \"" + Password + "\"," +
                   "\"" + UserEnum.accessLevel + "\" : \"" + (int)UserLevel + "\"," +
                   "\"" + UserEnum.activated + "\" : " + Activated.ToString().ToLower() +
                   "}";
        }

        public string JsonifyLogIn()
        {
            return "{" +
                   "\"" + UserEnum.name + "\" : \"" + Name + "\"," +
                   "\"" + UserEnum.password + "\" : \"" + SHA256Cypher.Cyphing(Password) + "\"," +
                   "\"" + UserEnum.accessLevel + "\" : \"" + (int)UserLevel + "\"," +
                   "\"" + UserEnum.activated + "\" : " + Activated.ToString().ToLower() +
                   "}";
        }
    }

    public enum UserEnum
    {
        id,
        name,
        password,
        accessLevel,
        activated
    }
}
