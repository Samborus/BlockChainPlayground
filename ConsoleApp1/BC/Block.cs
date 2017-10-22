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
    [DataContract]
    public class Block
    {
        #region BitCoin
        [DataMember]
        public UInt16 Version = 0x001;
        public UInt64? ID { get; set; }
        public UInt32 Bits { get; set; }
        public UInt32 Time;
        public byte[] hashPrevBlock  { get; set; }
        public byte[] hashMerkleRoot { get; protected set; }
        public byte[] Hash { get; protected set; }
        public UInt32 Nonce = 0;
        #endregion 
        private BlockData[] transactions { get; set; }

        string hash = String.Empty;
        

        public void SetNextID(UInt64? oldID)
        {
            if (oldID.HasValue)
                ID = oldID + 1;
            else
                ID = 0;
        }

        public byte[] ComputeMerkleHash(bool setHash = true)
        {
            List<byte[]> lista = new List<byte[]>();
            foreach(var rec in transactions)
            {
                lista.Add(GenerateHash2(rec.ToString()));
                string str = HashToString(GenerateHash2(rec.ToString()));
            }

            byte[] result = CountHashes(lista).FirstOrDefault();
            if (setHash)
                this.hashMerkleRoot = result;
            string str1 = HashToString(hashMerkleRoot);
            return result;
        }

        private List<byte[]> CountHashes(List<byte[]> hashes)
        {            
            if (hashes.Count == 1)
                return hashes;
            List<byte[]> result = new List<byte[]>();
            for (int i = 0; i < hashes.Count(); i += 2)
            {
                result.Add(GenerateHash2(hashes[i], hashes[(i + 1 == hashes.Count() ? i : i+ 1)]));
                string str = HashToString(GenerateHash2(hashes[i], hashes[(i + 1 == hashes.Count() ? i : i + 1)]));
            }
            result = CountHashes(result);            
            return result;
        }

        public Block(BlockData[] data)
        {
            this.transactions = data;
        }

        /// <summary>
        /// na ilość zer
        /// </summary>
        /// <param name="difficulty"></param>
        public byte[] GenerateHashWithDiffuculty(bool setHash = true)
        {
            byte[] bs = new byte[32];
            string temp = string.Empty;
            do
            {
                string toHash = Version.ToString() + hashPrevBlock + HashToString(this.hashMerkleRoot) + Nonce;
                bs = GenerateHash(toHash);
                Nonce++;
            }
            while (!HashToString(bs).Substring(0, 3).Equals("000"));
            if (setHash)
                this.Hash = bs;
            Nonce--;
            Console.WriteLine($"ID: {ID} Nonce: {Nonce}");
            return bs;
        }

        private string HashToString(byte[] lista)
        {
            string hex = string.Empty;
            foreach (byte x in lista)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }
        /*
        public string GenerateHash()
        {
            string result = string.Empty;
            byte[] chain1 = null;
            SHA256Managed sha = new SHA256Managed();
            object obj = this.transactions;
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                chain1 = ms.ToArray();
            }
            List<byte> toEncode = Encoding.Unicode.GetBytes(string.Join("", chain1)).ToList();

            byte[] chain2 = Encoding.UTF8.GetBytes(PreviousHash);            
            toEncode.AddRange(chain2);
            byte[] chain3 = Encoding.UTF8.GetBytes(Nonce.ToString());
            toEncode.AddRange(chain3);
            byte[] chain4 = Encoding.UTF8.GetBytes(ID.ToString());
            toEncode.AddRange(chain4);
            byte[] chain5 = Encoding.UTF8.GetBytes(Version.ToString());
            toEncode.AddRange(chain5);
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
        */
        public byte[] GenerateHash(string toHash)
        {
            List<byte> toEncode = Encoding.UTF8.GetBytes(toHash).ToList();
            byte[] chain1 = null;
            SHA256Managed sha = new SHA256Managed();

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
            return chain1;
        }

        public byte[] GenerateHash2(string toHash)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(toHash));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return crypto;
        }

            public byte[] GenerateHash(byte[] toHash)
        {
            List<byte> toEncode = toHash.ToList();
            byte[] chain1 = null;
            SHA256Managed sha = new SHA256Managed();

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
            return chain1;
        }

        public byte[] GenerateHash2(byte[] toHash1, byte[] toHash2)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in toHash1)
                stringBuilder.AppendFormat("{0:X2}", b);
            foreach (byte b in toHash2)
                stringBuilder.AppendFormat("{0:X2}", b);

            //stringBuilder = stringBuilder.ToString().ToLower();
            byte[] chain1 = null;
            SHA256Managed sha = new SHA256Managed();

            using (var myStream = new System.IO.MemoryStream())
            {
                using (var sw = new System.IO.StreamWriter(myStream))
                {
                    sw.Write(stringBuilder.ToString().ToLower());
                }
                using (var readonlyStream = new MemoryStream(myStream.ToArray(), writable: false))
                {
                    chain1 = sha.ComputeHash(readonlyStream);
                }
            }
            return chain1;
        }

        public byte[] GenerateHash(byte[] toHash1, byte[] toHash2)
        {
            List<byte> toEncode = toHash1.ToList();
            toEncode.AddRange(toHash2.ToList());
            byte[] chain1 = null;
            SHA256Managed sha = new SHA256Managed();

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
            return chain1;
        }

        public BlockData[] Datas
        {
            get
            {
                return transactions;
            }
        }
    }

    [Serializable]
    public sealed class BlockData
    {
        public readonly DateTime Time =  DateTime.Now; // DateTime.MinValue; //
        public string message { get; set; }

        public override string ToString()
        {
            return  $"{message}";
        }
    }
}
