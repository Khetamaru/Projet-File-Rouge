using System;
using System.Security.Cryptography;
using System.Text;

namespace Projet_File_Rouge.Tools
{
    public static class SHA256Cypher
    {
        public static string Cyphing(string str)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(str);
            Byte[] hashedBytes = new SHA256CryptoServiceProvider().ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}
