using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blockchain.Records;

namespace Blockchain.Services.Abstract
{
    public interface IBlockchainBuilderService
    {
        void AcceptBlock(BlockchainBlock block);
        BlockchainBlock BuildBlock(string data);
    }
}
