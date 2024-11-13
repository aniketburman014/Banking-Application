using System;
using System.Collections.Generic;

namespace BankingApp
{
    public class Account
    {
        private string accountNumber;
        private string holderName;
        private string accountType; // "savings" or "checking"
        private decimal balance;
        private DateTime createdDate;
        private DateTime lastInterestDate; 
        private List<Transaction> transactions = new List<Transaction>();

        public Account(string accountNumber, string holderName, string accountType, decimal initialDeposit)
        {
            this.accountNumber = accountNumber;
            this.holderName = holderName;
            this.accountType = accountType;
            this.balance = initialDeposit;
            this.createdDate = DateTime.Today;
            this.lastInterestDate = createdDate; 
        }

        public string GetAccountNumber()
        {
            return accountNumber;
        }

        public string GetHolderName()
        {
            return holderName;
        }

        public void SetHolderName(string holderName)
        {
            this.holderName = holderName;
        }

        public string GetAccountType()
        {
            return accountType;
        }

        public void SetAccountType(string accountType)
        {
            this.accountType = accountType;
        }

        public decimal GetBalance()
        {
            return balance;
        }

        public DateTime GetCreatedDate()
        {
            return createdDate;
        }

        public DateTime GetLastInterestDate()
        {
            return lastInterestDate;
        }

        public List<Transaction> GetTransactions()
        {
            return transactions;
        }

        public void Deposit(decimal amount)
        {
            balance += amount;
            transactions.Add(new Transaction("Deposit", amount));
        }

        public bool Withdraw(decimal amount)
        {
            if (amount > balance) return false;
            balance -= amount;
            transactions.Add(new Transaction("Withdrawal", amount));
            return true;
        }

        public void ApplyInterest()
        {
            decimal interestRate = 0.25m;
            decimal interest = balance * interestRate;
            balance += interest;
            transactions.Add(new Transaction("Interest", interest));
            lastInterestDate = DateTime.Today; 
        }
    }
}
