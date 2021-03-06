using Blockchain.Interfaces;

namespace Blockchain.Rules
{
    public class ProofOfWorkRule<T> : IProofOfWorkRule<T> where T : IProofOfWork
    {
        public void Execute(IEnumerable<Block<T>> builtBlocks, Block<T> nextData)
        {
            var height = builtBlocks.Count();
            if (Execute(height, nextData.Hash))
            {
                throw new ApplicationException("Proof of work is incorrect for this block");
            }
        }

        public bool Execute(int height, string hash)
        {
            var complexity = (int) (Math.Log2(height + 1) + 1);
            for(int i = 0; i < complexity; i++)
            {
                if (hash[i] != '0')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
