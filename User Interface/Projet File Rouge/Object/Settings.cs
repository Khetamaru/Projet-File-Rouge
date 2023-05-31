using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;

namespace Projet_File_Rouge.Object
{
    public class Settings
    {
        [JsonProperty]
        internal string httpUrl;
        [JsonProperty]
        internal string httpPort;
        [JsonProperty]
        internal bool testMode;

        [JsonProperty]
        internal string ftpName;
        [JsonProperty]
        internal string ftpPort;
        [JsonProperty]
        internal string ftpFile;

        [JsonProperty]
        internal string pathSaveBdd;

        [JsonConstructor]
        public Settings(string httpUrl, string httpPort, bool testMode, string ftpName, string ftpPort, string ftpFile, string pathSaveBdd)
        {
            this.httpUrl = httpUrl;
            this.httpPort = httpPort;
            this.testMode = testMode;

            this.ftpName = ftpName;
            this.ftpPort = ftpPort;
            this.ftpFile = ftpFile;

            this.pathSaveBdd = pathSaveBdd;
        }

        public Settings() { LoadSettings(); }

        private void LoadSettings()
        {
            try
            {
                Settings settings;

                using (StreamReader readStream = new("settings.json"))
                {
                    string json = readStream.ReadToEnd();
                    settings = JsonConvert.DeserializeObject<Settings>(json);

                    readStream.Close();
                }
                httpUrl = settings.httpUrl;
                httpPort = settings.httpPort;
                testMode = settings.testMode;

                ftpName = settings.ftpName;
                ftpPort = settings.ftpPort;
                ftpFile = settings.ftpFile;

                pathSaveBdd = settings.pathSaveBdd;
            }
            catch (Exception)
            {
                httpUrl = ConfigurationManager.AppSettings["http"];
                httpPort = ConfigurationManager.AppSettings["port"];
                testMode = false;

                ftpName = ConfigurationManager.AppSettings["ftp_host"];
                ftpPort = ConfigurationManager.AppSettings["ftp_port"];
                ftpFile = ConfigurationManager.AppSettings["ftp_folder"];

                pathSaveBdd = ConfigurationManager.AppSettings["pathSaveBdd"];
            }
        }

        internal void WriteSettings()
        {
            using StreamWriter writeStream = new("settings.json");
            writeStream.WriteLine(Jsonify());

            writeStream.Close();
        }

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
