using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Projet_File_Rouge.EBPObject;
using Projet_File_Rouge.Object;

namespace Projet_File_Rouge.Tools
{
    static class RequestCenter
    {
        public static User GetUser(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.user.ToString() + "/" + userId);
            return Unjsonify.UserUnjsoning(result);
        }

        internal static List<RedWire> GetRedWirePage(int pageNumber, DateTime date, int step, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/page/" + pageNumber + "/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + step + "/" + userId + "/" + clientName);
            return Unjsonify.RedWiresUnjsoning(result);
        }

        internal static void PutCommand(int id, string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PutRequest(BDDTabName.commandList.ToString() + "/" + id, json);
        }

        internal static bool GetCommandListRedWireNumber(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/redWireNumber/" + id);

            return Convert.ToBoolean(result);
        }

        internal static CommandList GetCommandList(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/" + id);
            return Unjsonify.CommandListUnjsoning(result);
        }

        internal static int GetCommandListNumber()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/number");
            return result == "" ? 0 : Int32.Parse(result);
        }

        internal static List<CommandList> GetCommandListPage(int pageNumber)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/page/" + pageNumber);
            return Unjsonify.CommandListsUnjsoning(result);
        }

        internal static List<Evenement> GetEvents(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.evenement.ToString() + "/redwire/" + id);
            return Unjsonify.EventsUnjsoning(result);
        }

        internal static List<RedWire> GetRedWireNotif(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notif/" + userId);
            return Unjsonify.RedWiresUnjsoning(result);
        }

        internal static List<DocumentList> GetDocumentList(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.documentList.ToString() + "/redwire/" + id);
            return Unjsonify.DocumentListsUnjsoning(result);
        }

        internal static List<CommandList> GetCommandNotif()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/notif");
            return Unjsonify.CommandListsUnjsoning(result);
        }

        internal static int GetRedWireNumber(DateTime date, int step, int userId, string clientName)
        {
            if (clientName == string.Empty) { clientName = "*"; }
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/total" + "/" + date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "/" + step + "/" + userId + "/" + clientName);
            return result == "" ? 0 : Int32.Parse(result);
        }

        internal static int GetRedWireNotifNumber(int userId)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/notifNumber" + "/" + userId);
            return result == "" ? 0 : Int32.Parse(result);
        }

        internal static int GetCommandListNotifNumber()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.commandList.ToString() + "/notifNumber");
            return result == "" ? 0 : Int32.Parse(result);
        }

        public static List<User> GetUsers()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.user.ToString());
            return Unjsonify.UsersUnjsoning(result);
        }

        public static User GetUserByName(string userName)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.user.ToString() + "/Name/" + userName);
            return Unjsonify.UserUnjsoning(result);
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

        internal static void PutRedWire(int id, string redWire)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PutRequest(BDDTabName.redWire.ToString() + "/" + id, redWire);
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

        public static RedWire PostRedWire(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.redWire.ToString(), json);
            return Unjsonify.RedWireUnjsoning(result);
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

        internal static void PostUserHistory(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.userHistoryList.ToString(), json);
        }

        internal static void PostEvent(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.evenement.ToString(), json);
        }

        internal static RedWire GetRedWire(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/" + id);
            return Unjsonify.RedWireUnjsoning(result);
        }

        internal static void PostDocument(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.documentList.ToString(), json);
        }

        internal static void PostCommand(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.commandList.ToString(), json);
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
        saleDocumentLine
    }
}
