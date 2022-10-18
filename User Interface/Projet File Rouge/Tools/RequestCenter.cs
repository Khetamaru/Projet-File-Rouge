using System;
using System.Collections.Generic;
using System.Net;
using Projet_File_Rouge.EBPObject;
using Projet_File_Rouge.Object;

namespace Projet_File_Rouge.Tools
{
    /// <summary>
    /// Generate HTTP sentences for server
    /// </summary>
    static class RequestCenter
    {
        internal static RedWire GetRedWire(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/" + id);
            return Unjsonify.RedWireUnjsoning(result);
        }
        internal static List<RedWire> GetRedWirePage(int pageNumber, DateTime date, int step, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/page/" + pageNumber + "/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + step + "/" + userId + "/" + clientName);
            return Unjsonify.RedWiresUnjsoning(result);
        }
        internal static List<RedWire> GetOldRedWirePage(int pageNumber, DateTime date, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/page/old/" + pageNumber + "/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + userId + "/" + clientName);
            return Unjsonify.RedWiresUnjsoning(result);
        }
        internal static List<RedWire> GetRedWirePageWithoutOld(int pageNumber, DateTime date, int step, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/page/noOld/" + pageNumber + "/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + step + "/" + userId + "/" + clientName);
            return Unjsonify.RedWiresUnjsoning(result);
        }
        internal static List<RedWire> GetRedWirePurge()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/purge");
            return Unjsonify.RedWiresUnjsoning(result);
        }
        internal static List<RedWire> GetRedWireNotif(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notif/" + userId);
            return Unjsonify.RedWiresUnjsoning(result);
        }
        internal static List<RedWire> GetRedWireNotifAdmin()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notifAdmin");
            return Unjsonify.RedWiresUnjsoning(result);
        }
        internal static int GetRedWireNumber(DateTime date, int step, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/total" + "/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + step + "/" + userId + "/" + clientName);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetRedWirePurgeNumber()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/purgeNumber");
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetRedWireNumberNoOld(DateTime date, int step, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/total/NoOld/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + step + "/" + userId + "/" + clientName);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetOldRedWireNumber(DateTime date, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/total/old/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + userId + "/" + clientName);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetRedWireNotifNumber(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notifNumber" + "/" + userId);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetRedWireNotifAdminNumber()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notifAdminNumber");
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetPrividerWaitingNotifNumber()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notifPrividerWaitingNumber");
            return result == "" ? 0 : Int32.Parse(result);
        }
        public static RedWire PostRedWire(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.redWire.ToString(), json);
            return Unjsonify.RedWireUnjsoning(result);
        }
        internal static void PutRedWire(int id, string redWire)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PutRequest(BDDTabName.redWire.ToString() + "/" + id, redWire);
        }
        internal static void DeleteRedWire(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.redWire.ToString() + "/" + id);
        }


        internal static CommandList GetCommandList(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/" + id);
            return Unjsonify.CommandListUnjsoning(result);
        }
        internal static List<CommandList> GetCommandListPage(int pageNumber)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/page/" + pageNumber);
            return Unjsonify.CommandListsUnjsoning(result);
        }
        internal static List<CommandList> GetCommandNotif()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/notif");
            return Unjsonify.CommandListsUnjsoning(result);
        }
        internal static bool GetCommandListRedWireNumber(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/redWireNumber/" + id);

            return Convert.ToBoolean(result);
        }
        internal static int GetCommandListNumber()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/number");
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static int GetCommandListNotifNumber()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/notifNumber");
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static void PostCommand(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.commandList.ToString(), json);
        }
        internal static void PutCommand(int id, string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PutRequest(BDDTabName.commandList.ToString() + "/" + id, json);
        }
        internal static void DeleteCommandListByRedWire(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.commandList.ToString() + "/redWire/" + id);
        }


        internal static MissingCall GetMissingCall(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.missingCall.ToString() + "/" + id);
            return Unjsonify.MissingCallUnjsoning(result);
        }
        internal static List<MissingCall> GetMissingCallsByUser(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.missingCall.ToString() + "/user/" + userId);
            return Unjsonify.MissingCallsUnjsoning(result);
        }
        internal static List<MissingCall> GetMissingCallsByUserSend(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.missingCall.ToString() + "/user/send/" + userId);
            return Unjsonify.MissingCallsUnjsoning(result);
        }
        internal static List<MissingCall> GetMissingCallUnread(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.missingCall.ToString() + "/unread/" + userId);
            return Unjsonify.MissingCallsUnjsoning(result);
        }
        internal static int GetMissingCallUnreadNumber(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.missingCall.ToString() + "/unreadNumber/" + id);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static void PostMissingCall(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.missingCall.ToString(), json);
        }
        internal static void PutMissingCall(int id, string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PutRequest(BDDTabName.missingCall.ToString() + "/" + id, json);
        }
        internal static void DeleteMissingCall(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.missingCall.ToString() + "/" + id);
        }


        public static User GetUser(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.user.ToString() + "/" + userId);
            return Unjsonify.UserUnjsoning(result);
        }
        public static User GetUserByName(string userName)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.user.ToString() + "/Name/" + userName);
            return Unjsonify.UserUnjsoning(result);
        }
        public static List<User> GetUsers()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.user.ToString());
            return Unjsonify.UsersUnjsoning(result);
        }
        public static List<User> GetActiveUsers()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.user.ToString() + "/active");
            return Unjsonify.UsersUnjsoning(result);
        }
        public static bool SignInRequest(string name, string password)
        {
            User user = new User(name, password, User.AccessLevel.Intern);
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.user.ToString() + "/SignIn", user.JsonifyLogIn());

            if (statusCode != HttpStatusCode.OK)
            {
                return false;
            }
            return true;
        }
        internal static void PostUser(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.user.ToString(), json);
        }
        internal static void PutUser(int id, string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PutRequest(BDDTabName.user.ToString() + "/" + id, json);
        }
        internal static void DeleteUserHistoryListByRedWire(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.userHistoryList.ToString() + "/redWire/" + id);
        }


        internal static List<DocumentList> GetDocumentLists(int redWireId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.documentList.ToString() + "/redWire/" + redWireId);
            return Unjsonify.DocumentListsUnjsoning(result);
        }
        internal static List<DocumentList> GetDocumentList(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.documentList.ToString() + "/redwire/" + id);
            return Unjsonify.DocumentListsUnjsoning(result);
        }
        internal static void PostDocument(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.documentList.ToString(), json);
        }
        internal static void DeleteDocumentListByRedWire(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.documentList.ToString() + "/redWire/" + id);
        }


        internal static List<Log> GetLogFiltered(int pageNumber, DateTime date, int type, int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.log.ToString() + "/page/" + pageNumber + "/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + type + "/" + userId);
            return Unjsonify.LogsUnjsoning(result);
        }
        internal static int GetLogNumber(DateTime date, int type, int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.log.ToString() + "/total/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + type + "/" + userId);
            return result == "" ? 0 : Int32.Parse(result);
        }
        internal static void PostLog(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.log.ToString(), json);
        }


        internal static List<Evenement> GetEvents(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.evenement.ToString() + "/redwire/" + id);
            return Unjsonify.EventsUnjsoning(result);
        }
        internal static void PostEvent(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.evenement.ToString(), json);
        }
        internal static void DeleteEvenementByRedWire(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().DeleteRequest(BDDTabName.evenement.ToString() + "/redWire/" + id);
        }


        internal static List<UserHistory> GetUserHistoryByRedWire(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.userHistoryList.ToString() + "/redwire/" + id);
            return Unjsonify.UserHistorysUnjsoning(result);
        }
        internal static void PostUserHistory(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.userHistoryList.ToString(), json);
        }


        internal static Object.Version GetVersion()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.version.ToString() + "/last");
            return Unjsonify.VersionUnjsoning(result);
        }
        internal static void PostVersion(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.version.ToString(), json);
        }


        public static SaleDocument GetSaleDocument(string DocumentNumber)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.saleDocument.ToString() + "/" + DocumentNumber);

            if (statusCode == HttpStatusCode.OK)
            {
                return Unjsonify.SaleDocumentUnjsoning(result);
            }
            else
            {
                return null;
            }
        }


        public static List<SaleDocumentLine> GetSaleLines(Guid saleDocumentId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.saleDocumentLine.ToString() + "/" + saleDocumentId.ToString());

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
        missingCall
    }
}
