namespace Blockchain.Interfaces
{
    public interface IProofOfWorkService<T> where T : IProofOfWork
    {
        T Proof(int height, T block);
    }
}
