using Blockchain.Interfaces;
using Blockchain.Models;
using Blockchain.Records;
using Blockchain.Rules;
using System.Text.Json;
using Blockchain.Helpers;

namespace Blockchain.Apps
{
    public record Transaction(string From, string To, long Amount);
    public record TransactionBlock(Transaction Props, string Sign, int Nonce) : ISignedBlock<Transaction>, IProofOfWork
    {
        public string PublicKey => Props.From;
    }

    public class CoinApp
    {
        private readonly IEncryptor _encryptor;
        private readonly TypedBlockchain<TransactionBlock> _blockchain;
        private readonly IProofOfWorkService<TransactionBlock> _proofOfWorkservice;
        public CoinApp()
        {
            var hashFunction = new SHA256Hash();
            _encryptor = new Encryptor();

            _blockchain = new TypedBlockchain<TransactionBlock>(
                new Blockchain(hashFunction),
                hashFunction,
                new SignCheckRule<TransactionBlock, Transaction>(_encryptor),
                new AmountCheckRule(),
                new ProofOfWorkRule<TransactionBlock>());

            _proofOfWorkservice = new ProofOfWorkService<TransactionBlock>(_blockchain,
                x => x with { Nonce = x.Nonce + 1 },
                new ProofOfWorkRule<TransactionBlock>());
        }

        public KeyPair GenerateKeys()
        {
            return _encryptor.GenerateKeys();
        }

        public void AddTransaction(TransactionBlock transactionBlock)
        {
            var block = _blockchain.BuildBlock(transactionBlock);
            _blockchain.AcceptBlock(block);
        }

        public void PerformTransaction(KeyPair from, string toPublicKey, long amount)
        {
            var transaction = new Transaction(from.PublicKey, toPublicKey, amount);
            var transactionString = JsonSerializer.Serialize(transaction);
            var sign = _encryptor.Sign(transactionString, from.PrivateKey);
            var blockchainLength = _blockchain.Count();
            var transactionBlock = _proofOfWorkservice.Proof(blockchainLength ,new TransactionBlock(transaction, sign, 0));

            AddTransaction(transactionBlock);
        }
    }
}
