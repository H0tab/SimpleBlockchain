using Blockchain.Apps;

namespace Blockchain.Rules
{
    public class AmountCheckRule : IRule<TransactionBlock>
    {
        public void Execute(IEnumerable<Block<TransactionBlock>> builtBlocks, Block<TransactionBlock> newData)
        {
            long balance = 100;
            var transaction = newData.Data;
            var from = transaction.Props.From;
            foreach (var block in builtBlocks)
            {
                var signedTransaction = block.Data;
                if (signedTransaction.Props.From == from)
                    balance -= signedTransaction.Props.Amount;
                else
                    balance += signedTransaction.Props.Amount;
            }

            if (balance < transaction.Props.Amount)
                throw new ApplicationException(
                    $"User {transaction.PublicKey} does not have {transaction.Props.Amount} coins. It has only {balance} coins.");
        }
    }
}
