using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XamApp.Classes
{
    public class Account
    {
        public AccountHolder Owner { get; set; }
        public DateTime OpenDateTime { get; set; }
        public double Balance { get; set; }
        public List<Operation> Operations { get; set; }

        public Account(AccountHolder owner)
        {
            Owner = owner;
            OpenDateTime = DateTime.Now;
            Balance = 0;
            Operations = new List<Operation>();
        }

        public void Deposit(double amount)
        {
            Balance += amount;
            Operations.Add(new Operation( DateTime.Now, amount,OperationStatus.Completed));
        }

        public bool Withdraw(double amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                Operations.Add(new Operation(DateTime.Now, amount,OperationStatus.Completed));
                return true;
            }
            else
            {
                Operations.Add(new Operation(DateTime.Now, amount, OperationStatus.Cancelled));
                return false;
            }
        }
        public override string ToString()
        {
            return $"{Owner.FirstName} {Owner.LastName}";
        }
    }
}
