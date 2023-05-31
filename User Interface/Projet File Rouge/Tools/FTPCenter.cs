using FluentFTP;
using Projet_File_Rouge.Object;
using System;
using System.Net;

namespace Projet_File_Rouge.Tools
{
    internal class FTPCenter
    {
        readonly FtpClient Client;

        public FTPCenter(string user, string password)
        {
            // Get the object used to communicate with the server.
            Client = new FtpClient();
            Settings Settings = new();
            Client.Host = Settings.ftpName;
            Client.Port = int.Parse(Settings.ftpPort);

            Client.Credentials = new NetworkCredential(user, password);
            try { Client.Connect(); }
            catch (Exception) { }
        }

        internal static DocumentFTP DataPacking(string actualPath, string documentName, RedWire redWire)
        {
            Guid name = Guid.NewGuid();

            string[] splitTab = actualPath.Split(".");
            string extentionType = splitTab[^1];

            string newPath = new Settings().ftpFile;
            newPath += name + "." + extentionType;

            return new DocumentFTP("\\\\RGDEPANNAGE\\" + newPath.Replace("/", "\\"), documentName, redWire);
        }

        internal static DocumentFTP DataPacking(string actualPath, string newPath, string documentName, RedWire redWire)
        {
            string[] splitTab = actualPath.Split(".");
            string extentionType = splitTab[^1];

            newPath += documentName + "." + extentionType;

            return new DocumentFTP("\\\\RGDEPANNAGE\\" + newPath.Replace("/", "\\"), documentName, redWire);
        }

        internal bool DataSending(string actualPath, DocumentFTP documentFTP)
        {
            if(IsConnected() && IsAuthenticated())
            {
                if (UploadFile(actualPath, documentFTP.UploadString())) return true;
            }
            return false;
        }

        internal bool Connected { get => Client.IsConnected; }
        internal bool IsConnected()
        {
            if(!Connected)
            {
                PopUpCenter.MessagePopup("Connection au service FTP échoué.\nProblème internet ou serveur.\nContacter le technicien.");
                return false;
            }
            return true;
        }
        internal bool Authenticated { get => Client.IsAuthenticated; }
        internal bool IsAuthenticated()
        {
            if(!Authenticated)
            {
                PopUpCenter.MessagePopup("Identification échouée.\nNom d'utilisateur ou mot de passe incorrect.");
                return false;
            }
            return true;
        }

        internal bool UploadFile(string actualPath, string newPath)
        {
            FtpStatus ftpStatus = Client.UploadFile(actualPath, newPath);

            if (ftpStatus == FtpStatus.Success)
            {
                PopUpCenter.MessagePopup("Envoi réussi.");
                return true;
            }
            PopUpCenter.MessagePopup("Envoi échoué.");
            return false;
        }

        internal bool DataDroping(string newPath)
        {
            Client.DeleteFile(newPath);

            bool trigger = Client.FileExists(newPath);

            if (!trigger)
            {
                return true;
            }
            PopUpCenter.MessagePopup("Suppression échoué.");
            return false;
        }
    }
}