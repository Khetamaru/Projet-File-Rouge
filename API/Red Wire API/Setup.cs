using Newtonsoft.Json;

namespace Red_Wire_API
{
    public class Setup
    {
        [JsonProperty]
        internal string HTTP_URL;
        [JsonProperty]
        internal string HTTP_PORT;

        [JsonProperty]
        internal string USER_NAME_LOCAL;
        [JsonProperty]
        internal string PASSWORD_LOCAL;
        [JsonProperty]
        internal string BDD_NAME_LOCAL;

        [JsonProperty]
        internal string IP_EBP;
        [JsonProperty]
        internal string USER_NAME_EBP;
        [JsonProperty]
        internal string PASSWORD_EBP;
        [JsonProperty]
        internal string BDD_NAME_EBP;

        [JsonConstructor]
        public Setup(string _HTTP_URL, string _HTTP_PORT, string _USER_NAME_LOCAL, string _PASSWORD_LOCAL, string _BDD_NAME_LOCAL, string _IP_EBP, string _USER_NAME_EBP, string _PASSWORD_EBP, string _BDD_NAME_EBP)
        {
            HTTP_URL = _HTTP_URL;
            HTTP_PORT = _HTTP_PORT;

            USER_NAME_LOCAL = _USER_NAME_LOCAL;
            PASSWORD_LOCAL = _PASSWORD_LOCAL;
            BDD_NAME_LOCAL = _BDD_NAME_LOCAL;

            IP_EBP = _IP_EBP;
            USER_NAME_EBP = _USER_NAME_EBP;
            PASSWORD_EBP = _PASSWORD_EBP;
            BDD_NAME_EBP = _BDD_NAME_EBP;
        }

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}