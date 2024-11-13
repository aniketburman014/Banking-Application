using System;

namespace BankingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BankingService bankingService = new BankingService();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Welcome to  Banking Application");

                // Display 
                if (bankingService.IsUserLoggedIn())
                {
                    Console.WriteLine("1. Logout");
                    Console.WriteLine("2. Open Account");
                    Console.WriteLine("3. Deposit");
                    Console.WriteLine("4. Withdraw");
                    Console.WriteLine("5. Check Balance");
                    Console.WriteLine("6. Display Statement");
                    Console.WriteLine("7. Apply Monthly Interest");
                    Console.WriteLine("8. Exit");
                }
                else
                {
                    Console.WriteLine("1. Register");
                    Console.WriteLine("2. Login");
                    Console.WriteLine("3. Exit");
                }

                Console.Write("Select an option: ");
                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (bankingService.IsUserLoggedIn())
                    {
                        //  if the user is logged in
                        switch (choice)
                        {
                            case 1:
                                bankingService.LogoutUser();
                                Console.WriteLine("Logged out successfully.");
                                break;

                            case 2:
                                OpenAccount(bankingService);
                                break;

                            case 3:
                                Deposit(bankingService);
                                break;

                            case 4:
                                Withdraw(bankingService);
                                break;

                            case 5:
                                CheckBalance(bankingService);
                                break;

                            case 6:
                                DisplayStatement(bankingService);
                                break;

                            case 7:
                                ApplyInterest(bankingService);
                                break;

                            case 8:
                                running = false;
                                break;

                            default:
                                Console.WriteLine("Invalid option. Try again.");
                                break;
                        }
                    }
                    else
                    {
                        //  if the user is not logged in
                        switch (choice)
                        {
                            case 1:
                                Register(bankingService);
                                break;

                            case 2:
                                Login(bankingService);
                                break;

                            case 3:
                                running = false;
                                break;

                            default:
                                Console.WriteLine("Invalid option. Try again.");
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void Register(BankingService bankingService)
        {
            Console.Write("Enter a username: ");
            string username = Console.ReadLine();
            Console.Write("Enter a password: ");
            string password = Console.ReadLine();
            bankingService.RegisterUser(username, password);
            
        }

        static void Login(BankingService bankingService)
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            bankingService.LoginUser(username, password);
        }

        static void OpenAccount(BankingService bankingService)
        {
            Console.Write("Enter account holder's name: ");
            string holderName = Console.ReadLine();

            Console.Write("Enter account type (1 for Savings, 2 for Checking): ");
            string accountTypeInput = Console.ReadLine();
            string accountType = accountTypeInput switch
            {
                "1" => "savings",
                "2" => "checking",
                _ => null
            };

            if (accountType == null)
            {
                Console.WriteLine("Invalid account type. Account not created.");
                return;
            }

            Console.Write("Enter initial deposit amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal initialDeposit))
            {
                bankingService.OpenAccount(holderName, accountType, initialDeposit);
                Console.WriteLine($"{accountType} account created successfully.");
            }
            else
            {
                Console.WriteLine("Invalid amount. Account not created.");
            }
        }


        static void Deposit(BankingService bankingService)
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Enter deposit amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                bankingService.DepositToAccount(accountNumber, amount);
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        static void Withdraw(BankingService bankingService)
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Enter withdrawal amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                bankingService.WithdrawFromAccount(accountNumber, amount);
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        static void CheckBalance(BankingService bankingService)
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();
            decimal balance = bankingService.CheckBalance(accountNumber);
            Console.WriteLine($"Current balance: {balance}");
        }

        static void DisplayStatement(BankingService bankingService)
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();
            bankingService.DisplayStatement(accountNumber);
        }

        static void ApplyInterest(BankingService bankingService)
        {
            bankingService.ApplyMonthlyInterest();
            
        }
    }
}
