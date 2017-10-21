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
    public class Block
    {

        public UInt64? ID { get; set; }
        BlockData[] datas { get; set; }
        string previousHash = String.Empty;
        string hash = String.Empty;
        public string nonce = "0";

        public void SetNextID(UInt64? oldID)
        {
            if (oldID.HasValue)
                ID = oldID + 1;
            else
                ID = 0;
        }

        public Block(BlockData[] data)
        {
            this.datas = data;
        }

        public void GenerateHashWithDiffuculty(float difficulty)
        {

        }

        public string ComputeHash()
        {
            return GenerateHash();
        }

        public void SetHash()
        {
            Hash = GenerateHash();
        }

            public string GenerateHash()
        {
            string result = string.Empty;
            byte[] chain1 = null;
            SHA256Managed sha = new SHA256Managed();
            object obj = this.datas;
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                chain1 = ms.ToArray();
            }
            List<byte> toEncode = Encoding.Unicode.GetBytes(string.Join("", chain1)).ToList();

            byte[] chain2 = Encoding.UTF8.GetBytes(PreviousHash);            
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
            result = hex;
            return result;
        }

        public string PreviousHash
        {
            get
            {
                return previousHash;
            }
            set { previousHash = value; }
        }

        public BlockData[] Datas
        {
            get
            {
                return datas;
            }
        }

        public string Hash { get => hash; private set { hash = value; } }
    }

    [Serializable]
    public sealed class BlockData
    {
        public readonly DateTime Time =  DateTime.Now; // DateTime.MinValue; //
        public string message { get; set; }
    }
}
