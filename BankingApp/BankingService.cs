using Bankingapp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp
{
    public class BankingService
    {
        private List<User> users = new List<User>();
        private User? loggedInUser;

        // Register a new user
        public void RegisterUser(string username, string password)
        {
            if (users.Any(u => u.GetUserName() == username))
            {
                Console.WriteLine("Username already exists.");
            }
            else
            {
                users.Add(new User(username, password));
                Console.WriteLine("Registration successful.");
            }
        }

        // Login a user
        public bool LoginUser(string username, string password)
        {
            loggedInUser = users.FirstOrDefault(u => u.GetUserName() == username && u.GetPassword() == password);
            if (loggedInUser == null)
            {
                Console.WriteLine("Invalid credentials.");
                return false;
            }
            Console.WriteLine("Login successful.");
            return true;
        }

        // Logout the current user
        public void LogoutUser()
        {
            loggedInUser = null;
            Console.WriteLine("Logged out successfully.");
        }

        // Open a new bank account
        public void OpenAccount(string holderName, string accountType, decimal initialDeposit)
        {
            if (loggedInUser == null)
            {
                Console.WriteLine("Please log in first.");
                return;
            }

            string accountNumber = "AC" + new Random().Next(10000, 99999);
            Account account = new Account(accountNumber, holderName, accountType, initialDeposit);
            loggedInUser.AddAccount(account);
            Console.WriteLine($"Account opened successfully. Account Number: {account.GetAccountNumber()}");
        }

        // Retrieve an account by account number
        private Account GetAccountByNumber(string accountNumber)
        {
            return loggedInUser?.GetAccounts().FirstOrDefault(a => a.GetAccountNumber() == accountNumber);
        }

        // Deposit into a  account
        public void DepositToAccount(string accountNumber, decimal amount)
        {
            Account account = GetAccountByNumber(accountNumber);
            if (account != null)
            {
                account.Deposit(amount);
                Console.WriteLine("Deposit successful.");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        // Withdraw from a  account
        public void WithdrawFromAccount(string accountNumber, decimal amount)
        {
            Account account = GetAccountByNumber(accountNumber);
            if (account != null)
            {
                bool success = account.Withdraw(amount);
                if (success)
                {
                    Console.WriteLine("Withdrawal successful.");
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        // Display account statement
        public void DisplayStatement(string accountNumber)
        {
            Account account = GetAccountByNumber(accountNumber);
            if (account != null)
            {
                Console.WriteLine($"\nStatement for Account: {account.GetAccountNumber()}");
                foreach (var transaction in account.GetTransactions())
                {
                    Console.WriteLine($"{transaction.GetDate()} - {transaction.GetType()}: {transaction.GetAmount()}");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        // Check the balance of a  account
        public decimal CheckBalance(string accountNumber)
        {
            Account account = GetAccountByNumber(accountNumber);
            if (account != null)
            {
                decimal balance = account.GetBalance();
                Console.WriteLine($"Current balance: {balance}");
                return balance;
            }
            else
            {
                Console.WriteLine("Account not found.");
                return 0;
            }
        }

        // Apply monthly interest to all savings accounts if 30 days have passed since the last interest application

        public bool IsUserLoggedIn() {
            if (loggedInUser == null) return false;
            return true; 
        }
        public void ApplyMonthlyInterest()
        {
            if (loggedInUser == null)
            {
                Console.WriteLine("Please log in first.");
                return;
            }

            foreach (var account in loggedInUser.GetAccounts())
            {
                if (account.GetAccountType() == "savings")
                {
                    DateTime lastInterestDate = account.GetLastInterestDate();
                    DateTime currentDate = DateTime.Now.Date;

                    if ((currentDate - lastInterestDate).TotalDays >= 30)
                    {
                        account.ApplyInterest();
                        Console.WriteLine($"Interest applied to account {account.GetAccountNumber()} - holder: {account.GetHolderName()}.");
                         
                    }
                    else
                    {
                        Console.WriteLine($"Interest for account {account.GetAccountNumber()} - holder: {account.GetHolderName()} has already been applied within the last 30 days.");
                    }
                }
            }
        }
    }
}
