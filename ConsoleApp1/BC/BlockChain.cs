using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.BC
{
    [DataContract]
    public sealed class BlockChain
    {
        [DataMember]
        private List<Block> blocks = new List<Block>();

        [DataMember]
        public Byte[] lastHash { get; set; }
        /// <summary>
        /// Add block usng chain rules
        /// </summary>
        /// <param name="candidateBlock"></param>
        /// <returns>Computed hash</returns>
        public string Add(Block candidateBlock)
        {
            Block lastBlock = getLastBlock;
            UInt64? lastId = lastBlock?.ID;
            if (lastBlock == null)
                candidateBlock.hashPrevBlock = new byte[32];
            else
                candidateBlock.hashPrevBlock = lastBlock.Hash;

            //candidateBlock.Bits = ;
            long dif = candidateBlock.CalculateDifficulty();

            candidateBlock.SetNextID(lastId);
            candidateBlock.ComputeMerkleHash();
            candidateBlock.GenerateHashWithDiffuculty(); // SetHash();
            
            this.blocks.Add(candidateBlock);
            return "";
        }


        public bool Validate()
        {
            bool result = true;
            byte[] prev = new byte[32];
            for (UInt64 i = 0; (int)i < blocks.Count; i++)
            {
                
                Block b = blocks.Where(o => o.ID.HasValue && o.ID.Value == i).FirstOrDefault();
                Console.WriteLine($"Block: [{i}] PrevHash: { b.HashToString(b.hashPrevBlock) }");
                Console.WriteLine($"Block: [{i}] Hash    : {b.HashToString(b.Hash)}");
                Console.WriteLine($"Block: [{i}] Merkle  : {b.HashToString(b.hashMerkleRoot)}");
                if (!prev.SequenceEqual(b.hashPrevBlock))
                    return false;
                prev = b.Hash;
                byte[] test = b.GenerateHashWithDiffuculty(false);
                if (!test.SequenceEqual(b.Hash))
                    return false;
            }
            return result;
        }

        public Block getLastBlock
        {
            get
            {
                if (blocks.Count > 0)
                    return blocks.OrderByDescending(o => o.ID).FirstOrDefault();
                return null;
            }
        }

        public IReadOnlyList<Block> getBlockChain
        {
            get
            {
                return blocks.AsReadOnly();
            }
        }
}
}
