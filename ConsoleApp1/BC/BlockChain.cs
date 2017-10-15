using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.BC
{
    public sealed class Block
    {
        BlockData[] datas { get; set; }
        string previousHash = String.Empty;
        string hash = String.Empty;

        public Block(string previousHash, BlockData[] data)
        {
            this.previousHash = previousHash;
            this.datas = data;
            hash = generateHash();
        }   
        
        private string generateHash()
        {
            byte[] chain1 = null;
            SHA256Managed sha = new SHA256Managed();
            object obj = this.datas;
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                chain1 = ms.ToArray();
            }
            byte[] chain2 = Encoding.UTF8.GetBytes(previousHash);

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

        public string GetPreviousHash
        {
            get
            {
                return previousHash;
            }
        }

        public BlockData[] Datas
        {
            get
            {
                return datas;
            }
        }

        public string Hash { get => hash;  }
    }

    [Serializable]
    public sealed class BlockData
    {
        public readonly DateTime Time =  DateTime.Now; // DateTime.MinValue; //
        public string message { get; set; }
    }
}
