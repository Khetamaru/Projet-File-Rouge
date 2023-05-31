using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Projet_File_Rouge.EBPObject;
using Projet_File_Rouge.Object;

namespace Projet_File_Rouge.Tools
{
    /// <summary>
    /// Generate HTTP sentences for server
    /// </summary>
    static class RequestCenter
    {
        private static void HttpStatusCodeCheck(HttpStatusCode statusCode)
        {
            if (statusCode == 0) { PopUpCenter.MessagePopup("Il y a eu un problème de communication avec le serveur.\nCe problème peut venir du serveur en lui même ou de votre configuration."); }
        }

        internal static RedWire GetRedWire(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/" + id);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.RedWireUnjsoning(result);
        }
        internal static List<RedWire> GetRedWirePage(int pageNumber, DateTime date, int step, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/page/" + pageNumber + "/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + step + "/" + userId + "/" + clientName);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.RedWiresUnjsoning(result);
        }
        internal static List<RedWire> GetOldRedWirePage(int pageNumber, DateTime date, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/page/old/" + pageNumber + "/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + userId + "/" + clientName);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.RedWiresUnjsoning(result);
        }
        internal static List<RedWire> GetReturnRedWirePage(int pageNumber, DateTime date, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/page/return/" + pageNumber + "/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + userId + "/" + clientName);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.RedWiresUnjsoning(result);
        }


        internal static List<RedWire> GetRedWirePageWithoutOld(int pageNumber, DateTime date, int step, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/page/noOld/" + pageNumber + "/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + step + "/" + userId + "/" + clientName);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.RedWiresUnjsoning(result);
        }
        internal static List<RedWire> GetRedWirePagePerso(int pageNumber, DateTime date, int step, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/page/Perso/" + pageNumber + "/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + step + "/" + userId + "/" + clientName);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.RedWiresUnjsoning(result);
        }
        internal static List<RedWire> GetRedWirePurge()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/purge");
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.RedWiresUnjsoning(result);
        }
        internal static List<RedWire> GetRedWireNotif(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notif/" + userId);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.RedWiresUnjsoning(result);
        }


        internal static List<RedWire> GetRedWireNotifAdmin()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notifAdmin");
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.RedWiresUnjsoning(result);
        }
        internal static int GetRedWireNumber(DateTime date, int step, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/total" + "/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + step + "/" + userId + "/" + clientName);
            HttpStatusCodeCheck(statusCode);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetRedWirePurgeNumber()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/purgeNumber");
            HttpStatusCodeCheck(statusCode);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetRedWireNumberNoOld(DateTime date, int step, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/total/NoOld/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + step + "/" + userId + "/" + clientName);
            HttpStatusCodeCheck(statusCode);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetRedWireNumberPerso(DateTime date, int step, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }

            (string result, _) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/total/Perso/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + step + "/" + userId + "/" + clientName);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetOldRedWireNumber(DateTime date, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/total/old/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + userId + "/" + clientName);
            HttpStatusCodeCheck(statusCode);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetReturnRedWireNumber(DateTime date, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/total/return/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + userId + "/" + clientName);
            HttpStatusCodeCheck(statusCode);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetRedWireNotifNumber(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notifNumber" + "/" + userId);
            HttpStatusCodeCheck(statusCode);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetRedWireNotifAdminNumber()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notifAdminNumber");
            HttpStatusCodeCheck(statusCode);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetPrividerWaitingNotifNumberAdmin()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notifPrividerWaitingNumberAdmin");
            HttpStatusCodeCheck(statusCode);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static List<RedWire> GetPrividerWaitingNotifAdmin()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notifPrividerWaitingAdmin");
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.RedWiresUnjsoning(result);
        }
        internal static int GetPrividerWaitingNotifNumber(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notifPrividerWaitingNumber/" + userId);
            HttpStatusCodeCheck(statusCode);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static List<RedWire> GetPrividerWaitingNotif(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notifPrividerWaiting/" + userId);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.RedWiresUnjsoning(result);
        }
        internal static RedWire PostRedWire(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequestAsync(BDDTabName.redWire.ToString(), json);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.RedWireUnjsoning(result);
        }
        internal static void PutRedWire(int id, string redWire)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().PutRequestAsync(BDDTabName.redWire.ToString() + "/" + id, redWire);
            HttpStatusCodeCheck(statusCode);
        }
        internal static void DeleteRedWire(int id)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.redWire.ToString() + "/" + id);
            HttpStatusCodeCheck(statusCode);
        }


        internal static CommandList GetCommandList(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/" + id);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.CommandListUnjsoning(result);
        }
        internal static List<CommandList> GetCommandListPage(int pageNumber)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/page/" + pageNumber);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.CommandListsUnjsoning(result);
        }
        internal static List<CommandList> GetCommandNotif()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/notif");
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.CommandListsUnjsoning(result);
        }
        internal static bool GetCommandListRedWireNumber(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/redWireNumber/" + id);
            HttpStatusCodeCheck(statusCode);
            return Convert.ToBoolean(result);
        }
        internal static int GetCommandListNumber()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/number");
            HttpStatusCodeCheck(statusCode);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetCommandListNotifNumber()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/notifNumber");
            HttpStatusCodeCheck(statusCode);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static void PostCommand(string json)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().PostRequestAsync(BDDTabName.commandList.ToString(), json);
            HttpStatusCodeCheck(statusCode);
        }
        internal static void PutCommand(int id, string json)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().PutRequestAsync(BDDTabName.commandList.ToString() + "/" + id, json);
            HttpStatusCodeCheck(statusCode);
        }
        internal static void DeleteCommandListByRedWire(int id)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.commandList.ToString() + "/redWire/" + id);
            HttpStatusCodeCheck(statusCode);
        }


        internal static MissingCall GetMissingCall(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.missingCall.ToString() + "/" + id);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.MissingCallUnjsoning(result);
        }
        internal static List<MissingCall> GetMissingCallsByUser(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.missingCall.ToString() + "/user/" + userId);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.MissingCallsUnjsoning(result);
        }
        internal static List<MissingCall> GetMissingCallsByUserSend(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.missingCall.ToString() + "/user/send/" + userId);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.MissingCallsUnjsoning(result);
        }
        internal static List<MissingCall> GetMissingCallUnread(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.missingCall.ToString() + "/unread/" + userId);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.MissingCallsUnjsoning(result);
        }
        internal static int GetMissingCallUnreadNumber(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.missingCall.ToString() + "/unreadNumber/" + id);
            HttpStatusCodeCheck(statusCode);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static void PostMissingCall(string json)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().PostRequestAsync(BDDTabName.missingCall.ToString(), json);
            HttpStatusCodeCheck(statusCode);
        }
        internal static void PutMissingCall(int id, string json)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().PutRequestAsync(BDDTabName.missingCall.ToString() + "/" + id, json);
            HttpStatusCodeCheck(statusCode);
        }
        internal static void DeleteMissingCall(int id)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.missingCall.ToString() + "/" + id);
            HttpStatusCodeCheck(statusCode);
        }


        internal static User GetUser(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.user.ToString() + "/" + userId);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.UserUnjsoning(result);
        }
        internal static User GetUserByName(string userName)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.user.ToString() + "/Name/" + userName);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.UserUnjsoning(result);
        }
        internal static List<User> GetUsers()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.user.ToString());
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.UsersUnjsoning(result);
        }
        internal static List<User> GetActiveUsers()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.user.ToString() + "/active");
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.UsersUnjsoning(result);
        }
        internal static bool SignInRequest(string name, string password)
        {
            User user = new(name, password, User.AccessLevel.Intern, "");
            (_, HttpStatusCode statusCode) = new RequestLauncher().PostRequestAsync(BDDTabName.user.ToString() + "/SignIn", user.JsonifyLogIn());
            HttpStatusCodeCheck(statusCode);

            if (statusCode != HttpStatusCode.OK)
            {
                return false;
            }
            return true;
        }
        internal static void PostUser(string json)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().PostRequestAsync(BDDTabName.user.ToString(), json);
            HttpStatusCodeCheck(statusCode);
        }
        internal static void PutUser(int id, string json)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().PutRequestAsync(BDDTabName.user.ToString() + "/" + id, json); 
            HttpStatusCodeCheck(statusCode);
        }
        internal static void DeleteUserHistoryListByRedWire(int id)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.userHistoryList.ToString() + "/redWire/" + id);
            HttpStatusCodeCheck(statusCode);
        }


        internal static List<DocumentList> GetDocumentLists(int redWireId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.documentList.ToString() + "/redWire/" + redWireId);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.DocumentListsUnjsoning(result);
        }
        internal static List<DocumentList> GetDocumentList(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.documentList.ToString() + "/redwire/" + id);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.DocumentListsUnjsoning(result);
        }
        internal static DocumentList GetDocumentListById(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.documentList.ToString() + "/" + id);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.DocumentListUnjsoning(result);
        }
        internal static void PostDocument(string json)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().PostRequestAsync(BDDTabName.documentList.ToString(), json);
            HttpStatusCodeCheck(statusCode);
        }
        internal static void DeleteDocumentList(int id)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.documentList.ToString() + "/" + id);
            HttpStatusCodeCheck(statusCode);
        }
        internal static void DeleteDocumentListByRedWire(int id)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.documentList.ToString() + "/redWire/" + id); 
            HttpStatusCodeCheck(statusCode);
        }


        internal static DocumentFTP GetDocumentFTP(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.documentFTP.ToString() + "/" + id);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.DocumentFTPUnjsoning(result);
        }
        internal static List<DocumentFTP> GetDocumentFTPByRedWire(int redWireId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.documentFTP.ToString() + "/redWire/" + redWireId);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.DocumentFTPsUnjsoning(result);
        }
        internal static void PostDocumentFTP(string json)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().PostRequestAsync(BDDTabName.documentFTP.ToString(), json);
            HttpStatusCodeCheck(statusCode);
        }
        internal static void DeleteDocumentFTP(int id)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.documentFTP.ToString() + "/" + id);
            HttpStatusCodeCheck(statusCode);
        }
        internal static void DeleteDocumentFTPByRedWire(int id)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.documentFTP.ToString() + "/redWire/" + id);
            HttpStatusCodeCheck(statusCode);
        }


        internal static List<Log> GetLogFiltered(int pageNumber, DateTime date, int type, int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.log.ToString() + "/page/" + pageNumber + "/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + type + "/" + userId);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.LogsUnjsoning(result);
        }
        internal static int GetLogNumber(DateTime date, int type, int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.log.ToString() + "/total/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + type + "/" + userId);
            HttpStatusCodeCheck(statusCode);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static void PostLog(string json)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().PostRequestAsync(BDDTabName.log.ToString(), json);
            HttpStatusCodeCheck(statusCode);
        }


        internal static List<Evenement> GetEvents(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.evenement.ToString() + "/redwire/" + id);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.EventsUnjsoning(result);
        }
        internal static void PostEvent(string json)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().PostRequestAsync(BDDTabName.evenement.ToString(), json);
            HttpStatusCodeCheck(statusCode);
        }
        internal static void DeleteEvenementByRedWire(int id)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.evenement.ToString() + "/redWire/" + id);
            HttpStatusCodeCheck(statusCode);
        }


        internal static (Object.Version, HttpStatusCode) GetVersion()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.version.ToString() + "/last");
            HttpStatusCodeCheck(statusCode);
            return (Unjsonify.VersionUnjsoning(result), statusCode);
        }
        internal static void PostVersion(string json)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().PostRequestAsync(BDDTabName.version.ToString(), json);
            HttpStatusCodeCheck(statusCode);
        }


        internal static List<UserHistory> GetUserHistoryByRedWire(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.userHistoryList.ToString() + "/redwire/" + id);
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.UserHistorysUnjsoning(result);
        }
        internal static void PostUserHistory(string json)
        {
            (_, HttpStatusCode statusCode) = new RequestLauncher().PostRequestAsync(BDDTabName.userHistoryList.ToString(), json);
            HttpStatusCodeCheck(statusCode);
        }


        internal static DbSave GetDbSaveLast()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.dbSave.ToString() + "/last");
            HttpStatusCodeCheck(statusCode);
            return Unjsonify.DbSaveUnjsoning(result);
        }
        internal static (string[], HttpStatusCode) SaveDataBase()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.dbSave.ToString() + "/save");
            HttpStatusCodeCheck(statusCode);

            if (statusCode == HttpStatusCode.OK && result != "\"[]\"")
            {
                result = result.Replace(",\\\"", ",\"").Replace("\\\",", "\",");
                result = result.Remove(0, 1);
                result = result.Remove(1, 1);
                result = result.Remove(result.Length - 1, 1);
                result = result.Remove(result.Length - 3, 1);
            }

            return (JsonConvert.DeserializeObject<string[]>(result), statusCode);
        }


        internal static SaleDocument GetSaleDocument(string DocumentNumber)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.saleDocument.ToString() + "/" + DocumentNumber);
            HttpStatusCodeCheck(statusCode);

            if (statusCode == HttpStatusCode.OK)
            {
                return Unjsonify.SaleDocumentUnjsoning(result);
            }
            else
            {
                return null;
            }
        }


        internal static List<SaleDocumentLine> GetSaleLines(Guid saleDocumentId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.saleDocumentLine.ToString() + "/" + saleDocumentId.ToString());
            HttpStatusCodeCheck(statusCode);

            if (statusCode == HttpStatusCode.OK)
            {
                return Unjsonify.SaleDocumentLinesUnjsoning(result);
            }
            else
            {
                return new List<SaleDocumentLine>();
            }
        }
    }

    enum BDDTabName
    {
        user,
        userHistoryList,
        redWire,
        evenement,
        documentList,
        commandList,

        saleDocument,
        saleDocumentLine,
        log,
        version,
        missingCall,
        documentFTP,
        dbSave
    }
}
