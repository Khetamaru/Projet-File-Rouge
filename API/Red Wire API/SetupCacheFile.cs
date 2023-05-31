using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Wire_API
{
    /// <summary>
    /// Save last username used to connect on this laptop.
    /// </summary>
    public static class SetupCacheFile
    {
        private static string CompletePath() { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "RG File Rouge_SetupCache.json"); }
        private static bool Exist() { return File.Exists(CompletePath()); }

        internal static Setup Start()
        {
            Console.WriteLine("Lancement de la récupération du fichier de configuration.");
            if(!Exist())
            {
                CreateFromScratch();
            }
            return Read();
        }

        private static void CreateFromScratch()
        {
            Write(new Setup("http://192.168.1.79:",
                            "8086",
                            "root",
                            "root",
                            "redwiredb",
                            "192.168.1.195",
                            "sa",
                            "",
                            "DO53_0895452f-b7c1-4c00-a316-c6a6d0ea4bf4"));

            Console.WriteLine("En abscence d'un fichier de configuration existant, un fichier par défaut a été créé.");
            Console.WriteLine("Pour retrouver ce fichier, suivre le chemin " + CompletePath());
        }

        /// <summary>
        /// Search if there is a username saved
        /// </summary>
        /// <returns></returns>
        public static Setup Read()
        {
            string content = string.Empty;
            try
            {
                string completePath = CompletePath();

                StreamReader sr = new StreamReader(completePath);
                content = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Echec de la récupération.");
                CreateFromScratch();
                return Read();
            }

            if(content != null)
            {
                Console.WriteLine("Réussite de la récupération.");
                return JsonConvert.DeserializeObject<Setup>(content);
            }
            else
            {
                CreateFromScratch();
                return Read();
            }
        }

        /// <summary>
        /// Save a username on connection runtime
        /// </summary>
        /// <param name="content"></param>
        public static void Write(Setup content)
        {
            try
            {
                string completePath = CompletePath();

                StreamWriter sw = new StreamWriter(completePath);
                sw.Write(content.Jsonify());
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
