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

        internal static List<RedWire> GetRedWirePage(int pageNumber)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/page/" + pageNumber);
            return Unjsonify.RedWiresUnjsoning(result);
        }

        internal static int GetRedWireNumber()
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + "/total");
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
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.user.ToString() + "/SignIn", user.Jsonify());

            if (statusCode != HttpStatusCode.OK)
            {
                return false;
            }
            return true;
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

        internal static void PostEvent(string json)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().PostRequest(BDDTabName.evenement.ToString(), json);
        }

        internal static RedWire GetRedWire(int id)
        {
            (string result, HttpStatusCode statusCode) = new RequestLauncher().GetRequest(BDDTabName.redWire.ToString() + id);
            return Unjsonify.RedWireUnjsoning(result);
        }
    }

    enum BDDTabName
    {
        user,
        userHistoryList,
        redWire,
        evenement,
        documentList,

        saleDocument,
        saleDocumentLine
    }
}
