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
        internal static SaleDocument SaleDocumentUnjsoning(string json) => JsonConvert.DeserializeObject<SaleDocument>(json);
        internal static List<SaleDocument> SaleDocumentsUnjsoning(string json) => JsonConvert.DeserializeObject<List<SaleDocument>>(json);
        internal static SaleDocumentLine SaleDocumentLineUnjsoning(string json) => JsonConvert.DeserializeObject<SaleDocumentLine>(json);
        internal static List<SaleDocumentLine> SaleDocumentLinesUnjsoning(string json) => JsonConvert.DeserializeObject<List<SaleDocumentLine>>(json);
        internal static Evenement EventUnjsoning(string json) => JsonConvert.DeserializeObject<Evenement>(json);
        internal static List<Evenement> EventsUnjsoning(string json) => JsonConvert.DeserializeObject<List<Evenement>>(json);
        internal static RedWire RedWireUnjsoning(string json) => JsonConvert.DeserializeObject<RedWire>(json);
        internal static List<RedWire> RedWiresUnjsoning(string json) => JsonConvert.DeserializeObject<List<RedWire>>(json);
        internal static User UserUnjsoning(string json) => JsonConvert.DeserializeObject<User>(json);
        internal static List<User> UsersUnjsoning(string json) => JsonConvert.DeserializeObject<List<User>>(json);
        internal static UserHistory UserHistoryUnjsoning(string json) => JsonConvert.DeserializeObject<UserHistory>(json);
        internal static List<UserHistory> UserHistorysUnjsoning(string json) => JsonConvert.DeserializeObject<List<UserHistory>>(json);
        internal static DocumentList DocumentListUnjsoning(string json) => JsonConvert.DeserializeObject<DocumentList>(json);
        internal static List<DocumentList> DocumentListsUnjsoning(string json) => JsonConvert.DeserializeObject<List<DocumentList>>(json);
        internal static CommandList CommandListUnjsoning(string json) => JsonConvert.DeserializeObject<CommandList>(json);
        internal static List<CommandList> CommandListsUnjsoning(string json) => JsonConvert.DeserializeObject<List<CommandList>>(json);
        internal static Log LogUnjsoning(string json) => JsonConvert.DeserializeObject<Log>(json);
        internal static List<Log> LogsUnjsoning(string json) => JsonConvert.DeserializeObject<List<Log>>(json);
        internal static Object.Version VersionUnjsoning(string json) => JsonConvert.DeserializeObject<Object.Version>(json);
        internal static List<Object.Version> VersionsUnjsoning(string json) => JsonConvert.DeserializeObject<List<Object.Version>>(json);
        internal static MissingCall MissingCallUnjsoning(string json) => JsonConvert.DeserializeObject<MissingCall>(json);
        internal static List<MissingCall> MissingCallsUnjsoning(string json) => JsonConvert.DeserializeObject<List<MissingCall>>(json);
        internal static List<DocumentFTP> DocumentFTPsUnjsoning(string json) => JsonConvert.DeserializeObject< List<DocumentFTP>>(json);
        internal static DocumentFTP DocumentFTPUnjsoning(string json) => JsonConvert.DeserializeObject<DocumentFTP>(json);
        internal static DbSave DbSaveUnjsoning(string json) => JsonConvert.DeserializeObject<DbSave>(json);
    }
}
// Old Size => 1085