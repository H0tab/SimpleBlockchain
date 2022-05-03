namespace Blockchain.Interfaces
{
    public interface ISignedBlock<T>
    {
        T Props { get; }
        string PublicKey { get; }
        string Sign { get; }
    }
}
