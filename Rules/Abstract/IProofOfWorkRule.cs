using Blockchain.Rules;

namespace Blockchain.Interfaces
{
    public interface IProofOfWorkRule<T> : IRule<T> where T : IProofOfWork
    {
        bool Execute(int height, string hash);
    }
}
