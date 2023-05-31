using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_File_Rouge.ExternalInfoFile
{
    /// <summary>
    /// Save last username used to connect on this laptop.
    /// </summary>
    public static class LoginCacheFile
    {
        private static string CompletePath() { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "RG File Rouge_loginCache.json"); }

        /// <summary>
        /// Search if there is a username saved
        /// </summary>
        /// <returns></returns>
        public static string Read()
        {
            string content = string.Empty;

            try
            {
                string completePath = CompletePath();

                StreamReader sr = new(completePath);
                content = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

            return content;
        }

        /// <summary>
        /// Save a username on connection runtime
        /// </summary>
        /// <param name="content"></param>
        public static void Write(string content)
        {
            try
            {
                string completePath = CompletePath();

                StreamWriter sw = new(completePath);
                sw.Write(content);
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