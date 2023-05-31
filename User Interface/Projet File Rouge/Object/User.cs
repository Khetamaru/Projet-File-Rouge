

using Projet_File_Rouge.Tools;
using Newtonsoft.Json;

namespace Projet_File_Rouge.Object
{
    public class User : IBDDObject
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
        [JsonProperty]
        private string versionSynced;

        public enum AccessLevel
        {
            Intern,
            User,
            SuperUser,
            Admin
        }

        public User(string name, string password, AccessLevel accessLevel, string versionSynced) : this(0, name, password, accessLevel, true, versionSynced) { }

        [JsonConstructor]
        public User(int id, string name, string password, int accessLevel, bool activated, string versionSynced)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.accessLevel = (AccessLevel)accessLevel;
            this.activated = activated;
            this.versionSynced = versionSynced;
        }

        public User(int id, string name, string password, AccessLevel accessLevel, bool activated, string versionSynced)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.accessLevel = accessLevel;
            this.activated = activated;
            this.versionSynced = versionSynced;
        }

        public int Id { get => id; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public AccessLevel UserLevel { get => accessLevel; set => accessLevel = value; }
        public bool Activated { get => activated; set => activated = value; }
        public string VersionSynced { get => versionSynced; set => versionSynced = value; }

        public string JsonifyLogIn()
        {
            return "{" +
                   "\"" + UserEnum.name + "\" : \"" + Name + "\"," +
                   "\"" + UserEnum.password + "\" : \"" + SHA256Cypher.Cyphing(Password) + "\"," +
                   "\"" + UserEnum.accessLevel + "\" : \"" + (int)UserLevel + "\"," +
                   "\"" + UserEnum.activated + "\" : " + Activated.ToString().ToLower() + "," +
                   "\"" + UserEnum.versionSynced + "\" : \"" + VersionSynced + "\"" +
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
        activated,
        versionSynced
    }
}
