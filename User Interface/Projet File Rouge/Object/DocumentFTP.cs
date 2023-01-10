using Newtonsoft.Json;

namespace Projet_File_Rouge.Object
{
    public class DocumentFTP : BDDObject
    {
        [JsonIgnore]
        [JsonProperty]
        private readonly int id;
        [JsonProperty]
        private readonly string documentRemotePath;
        [JsonProperty]
        private readonly string documentName;
        [JsonProperty]
        private readonly RedWire redWire;

        public DocumentFTP(string _documentRemotePath, string _documentName, RedWire _redWire)
        {
            documentRemotePath = _documentRemotePath;
            documentName = _documentName;
            redWire = _redWire;
        }

        [JsonConstructor]
        public DocumentFTP(int id, string documentRemotePath, string documentName, RedWire redWire) :
            this(documentRemotePath, documentName, redWire)
        {
            this.id = id;
        }

        public int Id { get => id; }
        public string DocumentRemotePath { get => documentRemotePath; }
        public string DocumentName { get => documentName; }
        public RedWire RedWire { get => redWire; }

        public string UploadString()
        {
            string[] splitTab = DocumentRemotePath.Split("\\");
            string result = "";

            for(int i = 3; i < splitTab.Length; i++)
            {
                result += splitTab[i];
                if (i < splitTab.Length - 1) result += "/";
            }
            return result;
        }

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
