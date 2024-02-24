using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProjects
{
    public class ATM
    {
        public void Action()
        {
            Account currentAccount = null;
            Dictionary<string, int> UsernameAndBalance = new Dictionary<string, int>();
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\StepProjects\\StepProjects\\files\\bankinfo.txt";
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine("Name,Password");
                foreach (var acc in Account.accounts)
                {
                    UsernameAndBalance.Add(acc.Username, acc.Balance);
                    writer.WriteLine($"{acc.Username},{acc.Password}");
                }
            }

        TryAgain:
            Console.WriteLine("Login (1) or Register (2)?");
            int option = 1;

            while (true)
            {
                try
                {
                    option = int.Parse(Console.ReadLine());
                    if (option == 1 || option == 2)
                    {
                        break;
                    }
                    else int.Parse("GoToError");
                }
                catch (Exception) { }
                {
                    Console.WriteLine("Enter a valid option");
                }
            }

            if (option == 1)
            {
                Console.WriteLine("Enter username");
                string username = Console.ReadLine();
                if (UsernameAndBalance.ContainsKey(username))
                {
                    var acc = Account.GetAccountByUsername(username);
                    Console.WriteLine("Enter password");
                    string password = Console.ReadLine();
                    if (acc.Password == password)
                    {
                        Console.WriteLine("Login successful");
                        currentAccount = acc;
                    }
                    else
                    {
                        Console.WriteLine("Password is incorrect");
                        goto TryAgain;
                    }
                }
                else
                {
                    Console.WriteLine("User doesn't exist");
                    goto TryAgain;
                }
            }
            else
            {
                Console.WriteLine("Enter your username");
                string username = Console.ReadLine();
                if (UsernameAndBalance.ContainsKey(username))
                {
                    Console.WriteLine("User already exists. Try again");
                    goto TryAgain;
                }
                Console.WriteLine("Enter your password");
                string password = Console.ReadLine();
                var account = new Account(username, password, 0);
                var success = Account.AddAccount(account);
                if (success)
                {
                    Console.WriteLine("Register successfull");
                    currentAccount = account;
                }
                else
                {
                    Console.WriteLine("Something went wrong");
                    goto TryAgain;
                }
            }

            Services:
            Console.WriteLine("What service do you want to use? (enter number)\n1. Check balance\n2. Withdraw money\n3. Deposit money\n4. Exit");
            int optionOfService = 1;

            while (true)
            {
                try
                {
                    optionOfService = int.Parse(Console.ReadLine());
                    if (optionOfService == 1 || optionOfService == 2 || optionOfService == 3 || optionOfService == 4)
                    {
                        break;
                    }
                    else int.Parse("GoToError");
                }
                catch (Exception) { }
                {
                    Console.WriteLine("Enter a valid option");
                }
            }

            if (optionOfService == 1)
            {
                Console.WriteLine($"{currentAccount.Username}, your current balance is: {currentAccount.Balance}$");
                goto Services;
            }
            else if (optionOfService == 2 || optionOfService == 3)
            {
                if (optionOfService == 2) Console.WriteLine("Enter amount to withdraw");
                else Console.WriteLine("Enter amount to deposit");

                int amount = 0;
                while (true)
                {
                    try
                    {
                        amount = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Enter valid amount");
                    }
                }

                if (optionOfService == 2)
                {
                    var success = currentAccount.Withdraw(amount);

                    if (success)
                    {
                        Console.WriteLine($"{amount}$ was withdrawn successfully!");
                        Console.WriteLine($"Your current balance is {currentAccount.Balance}$");
                    }
                    else Console.WriteLine("Insufficient funds");
                }
                else
                {
                    currentAccount.Deposit(amount);
                    Console.WriteLine($"{amount}$ was deposited successfully!");
                    Console.WriteLine($"Your current balance is {currentAccount.Balance}$");
                }

                goto Services;
            }
            else
            {
                currentAccount = null;
                return;
            }
        }
    }

    public class Account
    {
        public static List<Account> accounts = new List<Account>()
        {
            new Account("Giorgi", "password123", 1500),
            new Account("John", "password123", 0),
            new Account("Luka", "password123", 100000),
        };

        public string Username { get; set; }
        public string Password { get; set; }
        public int Balance { get; set; }

        public Account(string username, string password, int balance)
        {
            Username = username;
            Password = password;
            Balance = balance;
        }

        public bool Withdraw(int amount)
        {
            if (this.Balance < amount) { return false; }

            this.Balance -= amount;
            return true;
        }

        public bool Deposit(int amount)
        {
            this.Balance += amount;
            return true;
        }

        public bool CheckPassword(string password)
        {
            if (this.Password == password) { return true; }
            return false;
        }

        public static Account GetAccountByUsername(string username)
        {
            return accounts.FirstOrDefault(x => x.Username == username);
        }

        public static bool AddAccount(Account account)
        {
            var acc = accounts.FirstOrDefault(a => a.Username == account.Username);
            if (acc != null) { return false; }
            accounts.Add(account);
            return true;
        }
    }
}
