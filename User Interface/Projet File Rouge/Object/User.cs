

using Projet_File_Rouge.Tools;
using Newtonsoft.Json;

namespace Projet_File_Rouge.Object
{
    public class User : BDDObject
    {
        [JsonProperty]
        private readonly int id;

        [JsonProperty]
        private string name;
        [JsonProperty]
        private string password;
        [JsonProperty]
        private AccessLevel accessLevel;
        [JsonProperty]
        private bool activated;

        public enum AccessLevel
        {
            Intern,
            User,
            SuperUser,
            Admin
        }

        public User(string name, string password, AccessLevel accessLevel) : this(0, name, password, accessLevel, true) { }

        [JsonConstructor]
        public User(int id, string name, string password, int accessLevel, bool activated)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.accessLevel = (AccessLevel)accessLevel;
            this.activated = activated;
        }

        public User(int id, string name, string password, AccessLevel accessLevel, bool activated)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.accessLevel = accessLevel;
            this.activated = activated;
        }

        public int Id { get => id; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public AccessLevel UserLevel { get => accessLevel; set => accessLevel = value; }
        public bool Activated { get => activated; set => activated = value; }

        public string JsonifyLogIn()
        {
            return "{" +
                   "\"" + UserEnum.name + "\" : \"" + Name + "\"," +
                   "\"" + UserEnum.password + "\" : \"" + SHA256Cypher.Cyphing(Password) + "\"," +
                   "\"" + UserEnum.accessLevel + "\" : \"" + (int)UserLevel + "\"," +
                   "\"" + UserEnum.activated + "\" : " + Activated.ToString().ToLower() +
                   "}";
        }

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
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
