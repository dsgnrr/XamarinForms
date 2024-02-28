using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace XamApp.Classes
{
    public class Operation
    {
        public DateTime DateTime { get; set; }
        public double Amount { get; set; }
        public OperationStatus status { get; set; }

        public Operation(DateTime dateTime, double amount, OperationStatus status)
        {
            DateTime = dateTime;
            Amount = amount;
            this.status = status;
        }
    }
}
