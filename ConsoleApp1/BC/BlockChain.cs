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
            this.blocks.Add(candidateBlock);

            return "";
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
