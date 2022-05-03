using Blockchain.Interfaces;

namespace Blockchain.Records
{
    public record KeyPair(string PublicKey, string PrivateKey);

    public record BlockchainBlock(string ParentHash, string Data, string Hash)
    {
        public static string CalculateHash(IHashFunction hashFunction, string data, string parentHash)
        {
            return hashFunction.GetHash(parentHash + data);
        }
    }
    
}
