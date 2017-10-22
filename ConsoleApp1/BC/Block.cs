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
            transactions = data;
            int d = 0x1800eb30;
            Bits = 0x0404cb;
            var x = 0x1d00ffff;
            Bits = 0x0404cb;
            double d1 = 0x00ffff;// * 2 * Math.Pow(2, 8 * (0x1d - 3));
            double d3 = 0x0404cb* 2 * Math.Pow(2, 8 * (0x1d - 3));
            double dr = d1 / d3;
            decimal dec = CalculateDifficulty();
            decimal d5;
            //Bits = Convert.ToUInt32();
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
                string toHash = Version.ToString() + HashToString(hashPrevBlock) + HashToString(this.hashMerkleRoot) + Nonce;
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

        internal string HashToString(byte[] lista)
        {
            string hex = string.Empty;
            foreach (byte x in lista)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

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
        public long CalculateDifficulty()
        {
            //is 0x1b0404cb, the hexadecimal target is
            //0x0404cb * 2**(8*(0x1b - 3)) = 0x00000000000404CB000000000000000000000000000000000000000000000000
            uint p = Bits & 0x00FFFFFF;
            uint e = (Bits & 0xFF000000) >> 24;
            var dif = p * Math.Pow(2, (8 * (e - 3)));
            return (long)dif;
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
    }
}
