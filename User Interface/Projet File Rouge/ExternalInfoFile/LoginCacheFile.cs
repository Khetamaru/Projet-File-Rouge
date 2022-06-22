using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_File_Rouge.ExternalInfoFile
{
    public static class LoginCacheFile
    {
        public static string Read()
        {
            string content = string.Empty;

            try
            {
                string completePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "RG File Rouge_loginCache");

                StreamReader sr = new StreamReader(completePath);
                content = sr.ReadLine();
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

        public static void Write(string content)
        {
            try
            {
                string completePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "RG File Rouge_loginCache");

                StreamWriter sw = new StreamWriter(completePath);
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
