

using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.Object
{
    public class User : BDDObject
    {
        private readonly int? id;

        private string name;
        private string password;
        private AccessLevel accessLevel;

        public enum AccessLevel
        {
            Admin,
            SuperUser,
            User,
            Intern
        }

        public User(string _name, string _password, AccessLevel _accessLevel) : this(null, _name, _password, _accessLevel) { }

        public User(int? _id, string _name, string _password, AccessLevel _accessLevel)
        {
            id = _id;
            name = _name;
            password = _password;
            accessLevel = _accessLevel;
        }

        public int Id { get => id == null ? -1 : (int)id; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public AccessLevel UserLevel { get => accessLevel; set => accessLevel = value; }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + UserEnum.id + "\" : " + Id + "," +
                   "\"" + UserEnum.name + "\" : \"" + Name + "\"," +
                   "\"" + UserEnum.password + "\" : \"" + SHA256Cypher.Cyphing(Password) + "\"," +
                   "\"" + UserEnum.accessLevel + "\" : \"" + (int)UserLevel + "\"" +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "\"" + UserEnum.name + "\" : \"" + Name + "\"," +
                   "\"" + UserEnum.password + "\" : \"" + SHA256Cypher.Cyphing(Password) + "\"," +
                   "\"" + UserEnum.accessLevel + "\" : \"" + (int)UserLevel + "\"" +
                   "}";
        }
    }

    public enum UserEnum
    {
        id,
        name,
        password,
        accessLevel
    }
}
