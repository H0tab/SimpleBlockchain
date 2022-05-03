using Blockchain.Interfaces;
using System.Text.Json;

namespace Blockchain.Rules
{
    public class SignCheckRule<TBlock, TData> : IRule<TBlock> where TBlock : ISignedBlock<TData>
    {
        private readonly IEncryptor _encryptor;
        public SignCheckRule(IEncryptor encryptor)
        {
            _encryptor = encryptor;
        }

        public void Execute(IEnumerable<Block<TBlock>> builtBlocks, Block<TBlock> nextBlock)
        {
            var signed = nextBlock.Data;
            var dataThatShouldBeSigned = JsonSerializer.Serialize(nextBlock.Data);
            if (!_encryptor.VerifySign(signed.PublicKey, dataThatShouldBeSigned, signed.Sign))
                throw new ApplicationException("Block sign is incorrect.");
        }
    }
}
