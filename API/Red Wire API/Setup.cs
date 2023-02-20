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
        internal string IP_EBP;
        [JsonProperty]
        internal string USER_NAME;
        [JsonProperty]
        internal string PASSWORD;
        [JsonProperty]
        internal string BDD_NAME;

        [JsonConstructor]
        public Setup(string _HTTP_URL, string _HTTP_PORT, string _IP_EBP, string _USER_NAME, string _PASSWORD, string _BDD_NAME)
        {
            HTTP_URL = _HTTP_URL;
            HTTP_PORT = _HTTP_PORT;

            IP_EBP = _IP_EBP;
            USER_NAME = _USER_NAME;
            PASSWORD = _PASSWORD;
            BDD_NAME = _BDD_NAME;
        }

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}