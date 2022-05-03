using Blockchain.Records;

namespace Blockchain.Interfaces
{
    public interface IBlockchain : IEnumerable<BlockchainBlock>
    {
        void AddBlock(BlockchainBlock data);
    }
}
