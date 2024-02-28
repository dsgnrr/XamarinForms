using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XamApp.Classes
{
    public static class MoneyTransfer
    {
        public static void Transfer(Account sourceAccount, Account destinationAccount, double amount, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            lock (sourceAccount)
            {
                lock (destinationAccount)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (sourceAccount.Withdraw(amount))
                    {
                        destinationAccount.Deposit(amount);
                    }
                    else
                    {
                        throw new OperationCanceledException("Insufficient funds");
                    }
                }
            }
        }
    }
}
