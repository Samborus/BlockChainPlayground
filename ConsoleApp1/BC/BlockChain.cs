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

        public void Add(Block candidateBlock)
        {
            this.blocks.Add(candidateBlock);
        }

        public string getLastHash
        {
            get
            {
                if (blocks.Count > 0)
                    return blocks.Last().Hash;
                return string.Empty;
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
