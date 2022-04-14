using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet_File_Rouge.Object;

namespace Projet_File_Rouge.Tools
{
    static class RequestCenter
    {
        public static User GetUser(int userId)
        {
            return Unjsonify.UserUnjsoning(new RequestLauncher().GetRequest(BDDTabName.user.ToString() + "/" + userId));
        }
        public static List<User> GetUsers()
        {
            return Unjsonify.UsersUnjsoning(new RequestLauncher().GetRequest(BDDTabName.user.ToString()));
        }
    }

    enum BDDTabName
    {
        user,
        userHistoryList,
        redWire,
        eventList,
        evenement,
        documentList,
        equipment
    }
}
