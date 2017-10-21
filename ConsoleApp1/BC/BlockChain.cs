using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.BC
{
    public sealed class BlockChain
    {
        private List<Block> blocks = new List<Block>();
        
        /// <summary>
        /// Add block usng chain rules
        /// </summary>
        /// <param name="candidateBlock"></param>
        /// <returns>Computed hash</returns>
        public string Add(Block candidateBlock)
        {
            Block lastBlock = getLastBlock;
            string last = lastBlock?.Hash;
            UInt64? lastId = lastBlock?.ID;
            if (string.IsNullOrEmpty(last))
            {
                last = "genesis";
                
            }
            candidateBlock.SetNextID(lastId);
            candidateBlock.PreviousHash = last;
            candidateBlock.GenerateHashWithDiffuculty(0); // SetHash();
            
            this.blocks.Add(candidateBlock);

            return "";
        }


        public bool Validate()
        {
            bool result = true;
            string prev = "genesis", curr = "";
            for (UInt64 i = 0; (int)i < blocks.Count; i++)
            {
                
                Block b = blocks.Where(o => o.ID.HasValue && o.ID.Value == i).FirstOrDefault();
                Console.WriteLine($"PrevHash: {b.PreviousHash}");
                Console.WriteLine($"Hash: {b.Hash}");
                if (prev != b.PreviousHash)
                    return false;
                prev = b.Hash;
                string test = b.GenerateHash();
                if (test != b.Hash)
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
