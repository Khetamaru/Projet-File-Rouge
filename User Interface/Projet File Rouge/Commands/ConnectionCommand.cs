using Projet_File_Rouge.ExternalInfoFile;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Commands
{
    public class ConnectionCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        private string UserNameField;
        private string PasswordField;

        public ConnectionCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        public ConnectionCommand ChargeParameters(string userNameField, string passwordField)
        {
            UserNameField = userNameField;
            PasswordField = passwordField;

            return this;
        }

        public override void Execute(object parameter)
        {
            if (RequestCenter.SignInRequest(UserNameField, PasswordField))
            {
                LoginCacheFile.Write(UserNameField);
                cacheStore.SetCache(CacheStoreEnum.ActualUser, RequestCenter.GetUserByName(UserNameField));
                navigationStore.CurrentViewModel = new MainMenuViewModel(navigationStore, cacheStore);
            }
            else
            {
                // ERROR MESSAGE
            }
        }
    }
}
