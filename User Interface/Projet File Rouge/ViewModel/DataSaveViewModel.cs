using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Threading;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Purge Folder View
    /// </summary>
    public class DataSaveViewModel : ViewModelBase
    {
        private User actualUser;

        KeyValuePair<NavigationStore, CacheStore> Cache;

        FTPCenter client;
        User userFTP;

        public DataSaveViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            NavigateParameterMenuCommand = new NavigateParameterMenuCommand(navigationStore, cacheStore);

            // set up BDD objects
            actualUser = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));

            Cache = new KeyValuePair<NavigationStore, CacheStore>(navigationStore, cacheStore);

            (bool trigger, KeyValuePair<string, string> keyValuePair) = Cache.Value.GetFTPCache();

            if (trigger)
            {
                client = new FTPCenter(keyValuePair.Key, keyValuePair.Value);
                userFTP = new User(keyValuePair.Key, keyValuePair.Value, User.AccessLevel.Intern, "0.0.0");
            }
            else client = null;
        }

        /// <summary>
        /// Start purge process
        /// </summary>
        public void LaunchSave()
        {
            if (client != null && client.Connected && client.Authenticated)
            {
                (string[] output, HttpStatusCode statusCode) = RequestCenter.SaveDataBase();

                if (statusCode == HttpStatusCode.OK)
                {
                    string name = @"C:/dump_bdd.sql";
                    File.WriteAllLines(name, output);

                    FTPCenter FTPCenter = new FTPCenter(userFTP.Name, userFTP.Password);
                    DocumentFTP document = Tools.FTPCenter.DataPacking(
                            name,
                            @"" + new Settings().pathSaveBdd,
                            (@"dump_bdd " + DateTime.Now.ToString("yyyy'/'MM'/'dd' 'HH':'mm':'ss")).Replace(" ", "__").Replace("/", "_").Replace(":", "_"),
                            null);

                    if (FTPCenter.DataSending(name, document))
                    {
                        PopUpCenter.MessagePopup("Sauvegarde de la base de données terminée.");
                        RequestCenter.PostLog(new Log("Sauvegarde de la Base De Données effectuée.", DateTime.Now, Log.LogTypeEnum.BDD, ActualUser).Jsonify());
                    }
                    else PopUpCenter.MessagePopup("Sauvegarde échoué.\n\n" + output);

                    File.Delete(name);
                }
                else PopUpCenter.MessagePopup("Sauvegarde échoué.\n\n" + output);
            }
            else
            {
                OpenFTPPopUp();
            }
        }

        public bool fTPPopUpIsOpen;
        public bool FTPPopUpIsOpen
        {
            get => fTPPopUpIsOpen;
            set
            {
                fTPPopUpIsOpen = value;
                OnPropertyChanged(nameof(FTPPopUpIsOpen));
            }
        }
        public void OpenFTPPopUp() => FTPPopUpIsOpen = true;
        public void CloseFTPPopUp() => FTPPopUpIsOpen = false;

        public string fTPLoginField;
        public string FTPLoginField { get => fTPLoginField; set { fTPLoginField = value; OnPropertyChanged(nameof(FTPLoginField)); } }
        public string fTPPasswordField;
        public string FTPPasswordField { get => fTPPasswordField; set { fTPPasswordField = value; OnPropertyChanged(nameof(FTPPasswordField)); } }
        public void FTPYesButton()
        {
            if (FTPLoginField != null && FTPLoginField != string.Empty && FTPPasswordField != null && FTPPasswordField != string.Empty)
            {
                CloseFTPPopUp();
                client = new FTPCenter(FTPLoginField, FTPPasswordField);

                if (client.IsConnected() && client.IsAuthenticated())
                {
                    userFTP = new User(FTPLoginField, FTPPasswordField, User.AccessLevel.Intern, "0.0.0");
                    CacheMaj();
                    LaunchSave();
                }
                else OpenFTPPopUp();
            }
        }
        public void FTPNoButton()
        {
            CloseFTPPopUp();
        }

        public User ActualUser
        {
            get => actualUser;
        }

        private void CacheMaj()
        {
            Cache.Value.SetFTPCache(FTPLoginField, FTPPasswordField);

            NavigateParameterMenuCommand = new NavigateParameterMenuCommand(Cache.Key, Cache.Value);
        }

        /// <summary>
        /// Commands
        /// </summary>
        public NavigateParameterMenuCommand NavigateParameterMenuCommand { get; set; }
    }
}