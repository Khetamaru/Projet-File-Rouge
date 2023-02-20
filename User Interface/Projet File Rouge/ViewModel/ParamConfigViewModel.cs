using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// New File View
    /// </summary>
    public class ParamConfigViewModel : ViewModelBase
    {
        private string httpUrl;
        private string httpPort;
        private bool testMode;

        private string ftpName;
        private string ftpPort;
        private string ftpFile;
        private string pathSaveBdd;

        private Settings Settings;

        public ParamConfigViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            LogOutCommand = new LogOutCommand(navigationStore, cacheStore);

            // set up view objects
            Settings = new Settings();

            httpUrl = Settings.httpUrl;
            httpPort = Settings.httpPort;
            testMode = Settings.testMode;

            ftpName = Settings.ftpName;
            ftpPort = Settings.ftpPort;
            ftpFile = Settings.ftpFile;
            pathSaveBdd = Settings.pathSaveBdd;
        }

        /// <summary>
        /// Launch Settings Save
        /// </summary>
        public void SaveSettings()
        {
            if(FieldsVerification())
            {
                Settings = new Settings(httpUrl, httpPort, testMode, ftpName, ftpPort, ftpFile, pathSaveBdd);
                Settings.WriteSettings();
                LogOutCommand.Execute(new object());
            }
        }

        private bool FieldsVerification()
        {
            if (httpUrl == null || httpUrl == string.Empty)
            {
                PopUpCenter.MessagePopup("Erreur : URL HTTP SERVEUR vide.");
                return false;
            }
            if (httpPort == null || httpPort == string.Empty)
            {
                PopUpCenter.MessagePopup("Erreur : PORT HTTP SERVEUR vide.");
                return false;
            }
            if (ftpName == null || ftpName == string.Empty)
            {
                PopUpCenter.MessagePopup("Erreur : NOM FTP SERVEUR vide.");
                return false;
            }
            if (ftpPort == null || ftpPort == string.Empty)
            {
                PopUpCenter.MessagePopup("Erreur : PORT FTP vide.");
                return false;
            }
            if (ftpFile == null || ftpFile == string.Empty)
            {
                PopUpCenter.MessagePopup("Erreur : NOM DOSSIER FTP vide.");
                return false;
            }
            if (pathSaveBdd == null || pathSaveBdd == string.Empty)
            {
                PopUpCenter.MessagePopup("Erreur : CHEMIN DOSSIER SAUVEGARDE BDD vide.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check boxs
        /// </summary>
        /// <param name="boxName"></param>
        public void Box_checked()
        {
            testMode = true;
        }

        /// <summary>
        /// Uncheck Boxs
        /// </summary>
        /// <param name="boxName"></param>
        public void Box_unchecked()
        {
            testMode = false;
        }

        public string HttpUrl
        {
            get { return httpUrl; }
            set 
            {
                httpUrl = value;
                OnPropertyChanged(nameof(HttpUrlVisibility));
            }
        }
        public bool HttpUrlVisibility { get => HttpUrl != null && HttpUrl.Length > 0; }

        public string HttpPort
        {
            get { return httpPort; }
            set 
            {
                httpPort = value;
                OnPropertyChanged(nameof(HttpPortVisibility));
            }
        }
        public bool HttpPortVisibility { get => HttpPort != null && HttpPort.Length > 0; }

        public bool TestMode
        {
            get { return testMode; }
            set
            {
                testMode = value;
            }
        }

        public string FtpName
        {
            get { return ftpName; }
            set
            {
                ftpName = value;
                OnPropertyChanged(nameof(FtpNameVisibility));
            }
        }
        public bool FtpNameVisibility { get => FtpName != null && FtpName.Length > 0; }

        public string FtpPort
        {
            get { return ftpPort; }
            set
            {
                ftpPort = value;
                OnPropertyChanged(nameof(FtpPortVisibility));
            }
        }
        public bool FtpPortVisibility { get => FtpPort != null && FtpPort.Length > 0; }

        public string FtpFile
        {
            get { return ftpFile; }
            set
            {
                ftpFile = value;
                OnPropertyChanged(nameof(FtpFileVisibility));
            }
        }
        public bool FtpFileVisibility { get => FtpFile != null && FtpFile.Length > 0; }

        public string PathSaveBdd
        {
            get { return pathSaveBdd; }
            set
            {
                pathSaveBdd = value;
                OnPropertyChanged(nameof(PathSaveBddVisibility));
            }
        }
        public bool PathSaveBddVisibility { get => PathSaveBdd != null && PathSaveBdd.Length > 0; }

        /// <summary>
        /// Commands
        /// </summary>
        public LogOutCommand LogOutCommand { get; }
    }
}
