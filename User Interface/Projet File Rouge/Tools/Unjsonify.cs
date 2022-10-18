using System.Collections.Generic;
using Newtonsoft.Json;
using Projet_File_Rouge.EBPObject;
using Projet_File_Rouge.Object;

namespace Projet_File_Rouge.Tools
{
    /// <summary>
    /// Get string send by the server and transform into BDD Object(s)
    /// </summary>
    static class Unjsonify
    {
        public static SaleDocument SaleDocumentUnjsoning(string json) => JsonConvert.DeserializeObject<SaleDocument>(json);
        public static List<SaleDocument> SaleDocumentsUnjsoning(string json) => JsonConvert.DeserializeObject<List<SaleDocument>>(json);
        public static SaleDocumentLine SaleDocumentLineUnjsoning(string json) => JsonConvert.DeserializeObject<SaleDocumentLine>(json);
        public static List<SaleDocumentLine> SaleDocumentLinesUnjsoning(string json) => JsonConvert.DeserializeObject<List<SaleDocumentLine>>(json);
        public static Evenement EventUnjsoning(string json) => JsonConvert.DeserializeObject<Evenement>(json);
        public static List<Evenement> EventsUnjsoning(string json) => JsonConvert.DeserializeObject<List<Evenement>>(json);
        public static RedWire RedWireUnjsoning(string json) => JsonConvert.DeserializeObject<RedWire>(json);
        public static List<RedWire> RedWiresUnjsoning(string json) => JsonConvert.DeserializeObject<List<RedWire>>(json);
        public static User UserUnjsoning(string json) => JsonConvert.DeserializeObject<User>(json);
        public static List<User> UsersUnjsoning(string json) => JsonConvert.DeserializeObject<List<User>>(json);
        public static UserHistory UserHistoryUnjsoning(string json) => JsonConvert.DeserializeObject<UserHistory>(json);
        public static List<UserHistory> UserHistorysUnjsoning(string json) => JsonConvert.DeserializeObject<List<UserHistory>>(json);
        public static DocumentList DocumentListUnjsoning(string json) => JsonConvert.DeserializeObject<DocumentList>(json);
        public static List<DocumentList> DocumentListsUnjsoning(string json) => JsonConvert.DeserializeObject<List<DocumentList>>(json);
        public static CommandList CommandListUnjsoning(string json) => JsonConvert.DeserializeObject<CommandList>(json);
        public static List<CommandList> CommandListsUnjsoning(string json) => JsonConvert.DeserializeObject<List<CommandList>>(json);
        public static Log LogUnjsoning(string json) => JsonConvert.DeserializeObject<Log>(json);
        public static List<Log> LogsUnjsoning(string json) => JsonConvert.DeserializeObject<List<Log>>(json);
        public static Object.Version VersionUnjsoning(string json) => JsonConvert.DeserializeObject<Object.Version>(json);
        public static List<Object.Version> VersionsUnjsoning(string json) => JsonConvert.DeserializeObject<List<Object.Version>>(json);
        public static MissingCall MissingCallUnjsoning(string json) => JsonConvert.DeserializeObject<MissingCall>(json);
        public static List<MissingCall> MissingCallsUnjsoning(string json) => JsonConvert.DeserializeObject<List<MissingCall>>(json);
    }
}
// Old Size => 1085