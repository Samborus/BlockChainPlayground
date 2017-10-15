using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.BC
{
    public class SHA256Helper
    {
        public static string generateHash(string str1, object arg)
        {
            byte[] chain1 = null;
            SHA256Managed sha = new SHA256Managed();
            object obj = arg;
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                chain1 = ms.ToArray();
            }

            byte[] chain2 = Encoding.UTF8.GetBytes(str1);

            List<byte> toEncode = Encoding.Unicode.GetBytes(string.Join("", chain1)).ToList();
            toEncode.AddRange(chain2);
            using (var myStream = new System.IO.MemoryStream())
            {
                using (var sw = new System.IO.StreamWriter(myStream))
                {
                    sw.Write(Convert.ToBase64String(toEncode.ToArray()));
                }
                using (var readonlyStream = new MemoryStream(myStream.ToArray(), writable: false))
                {
                    chain1 = sha.ComputeHash(readonlyStream);
                }
            }
            string hex = string.Empty;
            foreach (byte x in chain1)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }
    }
}
