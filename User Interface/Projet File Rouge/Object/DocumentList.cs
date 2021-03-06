using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_File_Rouge.Object
{
    public class DocumentList : BDDObject
    {
        private readonly int id;
        private readonly string document;
        private readonly RedWire redWire;

        public DocumentList(string _document, RedWire _redWire)
        {
            document = _document;
            redWire = _redWire;
        }

        public DocumentList(int _id, string _document, RedWire _redWire) :
            this(_document, _redWire)
        {
            id = _id;
        }

        public int Id { get => id; }
        public string Document { get => document; }
        public RedWire RedWire { get => redWire; }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + DocumentListEnum.id + "\" : " + Id + "," +
                   "\"" + DocumentListEnum.document + "\" : \"" + Document + "\"," +
                   "\"" + DocumentListEnum.redWire + "\" : " + RedWire.Id +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "\"" + DocumentListEnum.document + "\" : \"" + Document + "\"," +
                   "\"" + DocumentListEnum.redWire + "\" : " + RedWire.Id +
                   "}";
        }
    }

    public enum DocumentListEnum
    {
        id,
        document,
        redWire
    }
}
