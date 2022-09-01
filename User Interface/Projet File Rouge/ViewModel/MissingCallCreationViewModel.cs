using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;
using System.Collections.Generic;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Missing Call Creation View
    /// </summary>
    public class MissingCallCreationViewModel : ViewModelBase
    {
        private User actualUser;
        private List<User> users;
        private List<string> userList;
        private string selectedUser;
        private User recipient;
        private string caller;
        private string message;

        public MissingCallCreationViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);
            NavigateMissingCallMenuCommand = new NavigateMissingCallMenuCommand(navigationStore, cacheStore);

            // set up view objects
            ActualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));
            Users = RequestCenter.GetUsers();
            UserList = new List<string>();
            foreach(User user in Users) UserList.Add(user.Name);
        }

        public void MissingCallCreation()
        {
            if (FieldCheck())
            {
                RequestCenter.PostMissingCall((new MissingCall(ActualUser, Recipient, Caller, Message, false)).Jsonify());
                NavigateMainMenuCommand.Execute(null);
            }
        }

        private bool FieldCheck()
        {
            Recipient = RequestCenter.GetUserByName(UserSelected);

            if (Recipient != null)
            {
                if (Caller != null && Caller != string.Empty)
                {
                    if (Caller.Length < 45)
                    {
                        if (Message != null && Message != string.Empty)
                        {
                            if (Message.Length < 300)
                            {
                                return true;
                            }
                            else
                            {
                                PopUpCenter.MessagePopup("Message trop long (Taille Max 300)");
                            }
                        }
                    }
                    else
                    {
                        PopUpCenter.MessagePopup("Nom de l'auteur trop long (Taille Max 45)");
                    }
                }
            }
            return false;
        }

        public User ActualUser { get { return actualUser; } set { actualUser = value; } }
        public List<User> Users { get { return users; } set { users = value; } }
        public List<string> UserList { get { return userList; } set { userList = value; } }
        public string UserSelected { get { return selectedUser; } set { selectedUser = value; } }
        public User Recipient { get { return recipient; } set { recipient = value; } }
        public string Caller { get { return caller; } set { caller = value; } }
        public string Message { get { return message; } set { message = value; } }

        /// <summary>
        ///  commands
        /// </summary>
        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
        public NavigateMissingCallMenuCommand NavigateMissingCallMenuCommand { get; }
    }
}