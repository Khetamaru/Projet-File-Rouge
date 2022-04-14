using System;

namespace Projet_File_Rouge.Object
{
    class Document : BDDObject
    {
        private readonly int? id;

        private readonly DocType docType;
        private readonly DateTime date;
        private readonly User author;
        private readonly Client client;

        public enum DocType
        {
            Facture,
            Devis,
            Prise_en_charge
        }

        public Document(int? _id, DocType _docType, DateTime _date, User _author, Client _client)
        {
            id = _id;

            docType = _docType;
            date = _date;
            author = _author;
            client = _client;
        }

        public int Id { get => id != null ? (int)id : -1; }
        public DocType DocumentType { get => docType; }
        public DateTime Date { get => date; }
        public User Author { get => author; }
        public Client Client { get => client; }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + DocumentEnum.id + "\" : " + Id + "," +
                   "\"" + DocumentEnum.documentType + "\" : " + ((int)DocumentType) + "," +
                   "\"" + DocumentEnum.date + "\" : \"" + Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + DocumentEnum.author + "\" : " + Author.Id + "," +
                   "\"" + DocumentEnum.client + "\" : " + Client.Id +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "\"" + DocumentEnum.documentType + "\" : " + ((int)DocumentType) + "," +
                   "\"" + DocumentEnum.date + "\" : \"" + Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + DocumentEnum.author + "\" : " + Author.Id + "," +
                   "\"" + DocumentEnum.client + "\" : " + Client.Id +
                   "}";
        }
    }

    public enum DocumentEnum
    {
        id,
        documentType,
        date,
        author,
        client
    }
}
