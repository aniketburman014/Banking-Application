using System;

namespace BankingApp
{
    public class Transaction
    {
        private string transactionId;
        private DateTime date;
        private string type;
        private decimal amount;

        public Transaction(string type, decimal amount)
        {
            this.transactionId = Guid.NewGuid().ToString();
            this.date = DateTime.Now;
            this.type = type;
            this.amount = amount;
        }

        public string GetTransactionId()
        {
            return transactionId;
        }

        public DateTime GetDate()
        {
            return date;
        }

        public string GetAccType()
        {
            return type;
        }

        public decimal GetAmount()
        {
            return amount;
        }
    }
}
