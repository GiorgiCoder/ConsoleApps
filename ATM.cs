using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StepProjects
{
    public class ATM
    {
        private const string filePath = "Account.xml";
        public void Action()
        {
            List<Account> accounts;

            if (File.Exists(filePath))
            {
                accounts = DeserializeAccounts();
            }
            else
            {
                accounts = Account.GetDefaultAccounts();
                SerializeAccounts(accounts);
            }

            Account currentAccount = null;
            Console.WriteLine("Login (1) or Register (2)?");
            int option = 1;

            while (true) // validation for input
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

            if (option == 1) // login
            {
                Console.WriteLine("Enter username");
                string username = Console.ReadLine();
                var acc = Account.GetAccountByUsername(accounts, username);

                if (acc != null)
                {
                    Console.WriteLine("Enter password");
                    string password = Console.ReadLine();

                    if (acc.CheckPassword(password))
                    {
                        Console.WriteLine("Login successful");
                        currentAccount = acc;
                    }
                    else
                    {
                        Console.WriteLine("Password is incorrect");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("User doesn't exist");
                    return;
                }
            }
            else // register
            {
                Console.WriteLine("Enter your username");
                string username = Console.ReadLine();

                if (Account.UsernameExists(accounts, username))
                {
                    Console.WriteLine("User already exists. Try again");
                    return;
                }

                Console.WriteLine("Enter your password");
                string password = Console.ReadLine();
                var account = new Account(username, password, 0);
                accounts.Add(account);
                SerializeAccounts(accounts);
                Console.WriteLine("Register successful");
                currentAccount = account;
            }

            bool continueUsing = true;
            while (continueUsing)
            {
                Console.WriteLine("What service do you want to use? (enter number)\n1. Check balance\n2. Withdraw money\n3. Deposit money\n4. Exit");
                int optionOfService = 1;

                while (true) // validation for service
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

                if (optionOfService == 1) // check balance
                {
                    Console.WriteLine($"{currentAccount.Username}, your current balance is: {currentAccount.Balance}$");
                }
                else if (optionOfService == 2 || optionOfService == 3) // deposit and withdraw are combined, because they are similar
                {
                    if (optionOfService == 2) Console.WriteLine("Enter amount to withdraw");
                    else Console.WriteLine("Enter amount to deposit");

                    int amount = 0;
                    while (true) // amount validation
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

                    if (optionOfService == 2) // withdraw
                    {
                        var success = currentAccount.Withdraw(amount);

                        if (success)
                        {
                            Console.WriteLine($"{amount}$ was withdrawn successfully!");
                            Console.WriteLine($"Your current balance is {currentAccount.Balance}$");
                        }
                        else Console.WriteLine("Insufficient funds");
                    }
                    else // deposit
                    {
                        currentAccount.Deposit(amount);
                        Console.WriteLine($"{amount}$ was deposited successfully!");
                        Console.WriteLine($"Your current balance is {currentAccount.Balance}$");
                    }
                    SerializeAccounts(accounts);
                }
                else // exit
                {
                    currentAccount = null; // no need but still
                    return;
                }

                string input = "Y";
                while (true)
                {
                    Console.WriteLine("Do you want to use our services again? Y / N");
                    input = Console.ReadLine();
                    if (input == "Y" || input == "N")
                        break;
                }

                if (input == "N") continueUsing = false; // else, while loop terminates again
            }
        }

        private void SerializeAccounts(List<Account> accounts)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Account>));
            using (FileStream fileStream = File.Create(filePath))
            {
                serializer.Serialize(fileStream, accounts);
            }
        }

        private List<Account> DeserializeAccounts()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Account>));
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                return (List<Account>)serializer.Deserialize(fileStream);
            }
        }
    }

    [Serializable]
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Balance { get; set; }

        public Account() { }

        public Account(string username, string password, int balance)
        {
            Username = username;
            Password = password;
            Balance = balance;
        }

        public bool Withdraw(int amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        public void Deposit(int amount)
        {
            Balance += amount;
        }

        public bool CheckPassword(string password)
        {
            return Password == password;
        }

        public static Account GetAccountByUsername(List<Account> accounts, string username)
        {
            return accounts.FirstOrDefault(a => a.Username == username);
        }

        public static bool UsernameExists(List<Account> accounts, string username)
        {
            return accounts.Any(a => a.Username == username);
        }

        public static List<Account> GetDefaultAccounts()
        {
            return new List<Account>
            {
                new Account("Giorgi", "password123", 1500),
                new Account("John", "password123", 0),
                new Account("Luka", "password123", 100000),
                new Account("Jane", "doe", 0)
            };
        }
    }
}
