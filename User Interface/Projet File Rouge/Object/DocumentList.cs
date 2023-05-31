using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_File_Rouge.Object
{
    public class DocumentList : IBDDObject
    {
        [JsonIgnore]
        [JsonProperty]
        private readonly int id;
        [JsonProperty]
        private readonly string document;
        [JsonProperty]
        private readonly RedWire redWire;

        public DocumentList(string _document, RedWire _redWire)
        {
            document = _document;
            redWire = _redWire;
        }

        [JsonConstructor]
        public DocumentList(int id, string document, RedWire redWire) :
            this(document, redWire)
        {
            this.id = id;
        }

        public int Id { get => id; }
        public string Document { get => document; }
        public RedWire RedWire { get => redWire; }

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
