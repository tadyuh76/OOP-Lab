using System.Text.Json.Serialization;
using System.Text.Json;

namespace lab_10
{
    // Event arguments classes
    public class TransactionEventArgs : EventArgs
    {
        public decimal Amount { get; set; }
        public decimal NewBalance { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionTime { get; set; }

        public TransactionEventArgs(decimal amount, decimal newBalance, string transactionType)
        {
            Amount = amount;
            NewBalance = newBalance;
            TransactionType = transactionType;
            TransactionTime = DateTime.Now;
        }
    }

    public class TransferEventArgs : TransactionEventArgs
    {
        public string RecipientAccount { get; set; }

        public TransferEventArgs(decimal amount, decimal newBalance, string recipientAccount)
            : base(amount, newBalance, "Transfer")
        {
            RecipientAccount = recipientAccount;
        }
    }

    // Account class to represent a bank account
    public class Account
    {
        [JsonInclude]
        public string AccountNumber { get; private set; }
        [JsonInclude]
        public string OwnerName { get; set; }
        [JsonInclude]
        public string PhoneNumber { get; set; }
        [JsonInclude] 
        public decimal Balance { get; private set; }

        // Delegate definitions
        public delegate void TransactionHandler(object sender, TransactionEventArgs e);
        public delegate void TransferHandler(object sender, TransferEventArgs e);

        // Event declarations
        public event TransactionHandler WithdrawalCompleted;
        public event TransferHandler TransferCompleted;

        // Parameterless constructor for JSON deserialization
        public Account()
        {
            AccountNumber = "";
            OwnerName = "";
            PhoneNumber = "";
            Balance = 0;
        }


        public Account(string accountNumber, string ownerName, string phoneNumber, decimal initialBalance = 0)
        {
            AccountNumber = accountNumber;
            OwnerName = ownerName;
            PhoneNumber = phoneNumber;
            Balance = initialBalance;
        }

        // Set Balance for deserialization
        public void SetBalance(decimal balance)
        {
            Balance = balance;
        }


        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive");

            Balance += amount;
            Console.WriteLine($"{amount:C} deposited. New balance: {Balance:C}");
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive");

            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds");

            Balance -= amount;

            // Trigger withdrawal event
            OnWithdrawalCompleted(new TransactionEventArgs(amount, Balance, "Withdrawal"));

            Console.WriteLine($"{amount:C} withdrawn. New balance: {Balance:C}");
        }

        public void Transfer(decimal amount, Account recipient)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive");

            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds");

            Balance -= amount;
            recipient.Balance += amount;

            // Trigger transfer event
            OnTransferCompleted(new TransferEventArgs(amount, Balance, recipient.AccountNumber));

            Console.WriteLine($"{amount:C} transferred to {recipient.OwnerName}. New balance: {Balance:C}");
        }

        protected virtual void OnWithdrawalCompleted(TransactionEventArgs e)
        {
            WithdrawalCompleted?.Invoke(this, e);
        }

        protected virtual void OnTransferCompleted(TransferEventArgs e)
        {
            TransferCompleted?.Invoke(this, e);
        }
    }

    // Account data management class
    public static class AccountDataManager
    {
        private const string FILE_PATH = "accounts.json";

        // Save accounts to JSON file
        public static void SaveAccounts(List<Account> accounts)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                string jsonString = JsonSerializer.Serialize(accounts, options);
                File.WriteAllText(FILE_PATH, jsonString);
                Console.WriteLine($"Accounts saved successfully to {FILE_PATH}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving accounts: {ex.Message}");
            }
        }

        // Load accounts from JSON file
        public static List<Account> LoadAccounts()
        {
            try
            {
                if (File.Exists(FILE_PATH))
                {
                    string jsonString = File.ReadAllText(FILE_PATH);
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    List<Account>? accounts = JsonSerializer.Deserialize<List<Account>>(jsonString, options);
                    Console.WriteLine($"Accounts loaded successfully from {FILE_PATH}");
                    return accounts ?? new List<Account>();
                }
                else
                {
                    Console.WriteLine("No saved accounts found. Starting with empty list.");
                    return new List<Account>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading accounts: {ex.Message}");
                return new List<Account>();
            }
        }
    }


    // Notification service to send SMS notifications
    public class NotificationService
    {
        public void Subscribe(Account account)
        {
            account.WithdrawalCompleted += SendWithdrawalNotification;
            account.TransferCompleted += SendTransferNotification;
        }

        private void SendWithdrawalNotification(object sender, TransactionEventArgs e)
        {
            if (sender is Account account)
            {
                // Simulate sending SMS
                Console.WriteLine($"\nSMS sent to {account.PhoneNumber}:");
                Console.WriteLine($"Dear {account.OwnerName}, {e.Amount:C} has been withdrawn from your account.");
                Console.WriteLine($"New balance: {e.NewBalance:C}");
                Console.WriteLine($"Transaction time: {e.TransactionTime}");
                Console.WriteLine("Thank you for using our ATM service.\n");
            }
        }

        private void SendTransferNotification(object sender, TransferEventArgs e)
        {
            if (sender is Account account)
            {
                // Simulate sending SMS
                Console.WriteLine($"\nSMS sent to {account.PhoneNumber}:");
                Console.WriteLine($"Dear {account.OwnerName}, {e.Amount:C} has been transferred to account {e.RecipientAccount}.");
                Console.WriteLine($"New balance: {e.NewBalance:C}");
                Console.WriteLine($"Transaction time: {e.TransactionTime}");
                Console.WriteLine("Thank you for using our ATM service.\n");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ATM Account Management System");
            Console.WriteLine("=============================");

            // Create notification service
            NotificationService notificationService = new NotificationService();

            // Try to load accounts from file first
            List<Account> accounts = AccountDataManager.LoadAccounts();

            // If no accounts were loaded, create new ones
            if (accounts.Count == 0)
            {
                accounts = CreateAccounts();
            }

            // Subscribe all accounts to notification service
            foreach (Account account in accounts)
            {
                notificationService.Subscribe(account);
            }

            // Run the main menu
            RunMainMenu(accounts);

            // Save accounts before exiting
            AccountDataManager.SaveAccounts(accounts);
        }


        // Add "Save Accounts" option to the main menu
        static void RunMainMenu(List<Account> accounts)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\nATM Account Management System");
                Console.WriteLine("=============================");
                Console.WriteLine("1. Select an account");
                Console.WriteLine("2. Create a new account");
                Console.WriteLine("3. Save accounts");
                Console.WriteLine("4. Exit");
                Console.Write("\nEnter your choice (1-4): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Account selectedAccount = SelectAccount(accounts);
                            if (selectedAccount != null)
                            {
                                AccountMenu(selectedAccount, accounts);
                            }
                            break;
                        case 2:
                            Account newAccount = CreateSingleAccount();
                            if (newAccount != null)
                            {
                                accounts.Add(newAccount);
                                Console.WriteLine($"\nAccount created successfully for {newAccount.OwnerName}");
                                WaitForKeyPress();
                            }
                            break;
                        case 3:
                            AccountDataManager.SaveAccounts(accounts);
                            WaitForKeyPress();
                            break;
                        case 4:
                            exit = true;
                            AccountDataManager.SaveAccounts(accounts); // Save on exit
                            Console.WriteLine("Thank you for using our ATM service!");
                            break;
                        default:
                            Console.WriteLine("Invalid option. Press any key to continue...");
                            Console.ReadKey(true);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Press any key to continue...");
                    Console.ReadKey(true);
                }
            }
        }

        // Method to create a single account
        static Account CreateSingleAccount()
        {
            Console.WriteLine("\nCreate a new account");
            Console.WriteLine("--------------------");

            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter account owner name: ");
            string ownerName = Console.ReadLine();

            Console.Write("Enter phone number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter initial balance amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal initialBalance))
            {
                initialBalance = 0;
                Console.WriteLine("Invalid amount, setting initial balance to 0.");
            }

            return new Account(accountNumber, ownerName, phoneNumber, initialBalance);
        }

        // Modified CreateAccounts to use the new CreateSingleAccount method
        static List<Account> CreateAccounts()
        {
            List<Account> accounts = new List<Account>();
            bool addingAccounts = true;

            while (addingAccounts)
            {
                Account account = CreateSingleAccount();
                accounts.Add(account);
                Console.WriteLine($"\nAccount created successfully for {account.OwnerName}");

                Console.Write("\nDo you want to add another account? (yes/no): ");
                string response = Console.ReadLine().Trim().ToLower();
                addingAccounts = (response == "yes" || response == "y");
            }

            return accounts;
        }



        static Account SelectAccount(List<Account> accounts)
        {
            Console.Clear();
            Console.WriteLine("\nSelect an account");
            Console.WriteLine("-----------------");

            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {accounts[i].OwnerName} (Acc#: {accounts[i].AccountNumber})");
            }

            Console.WriteLine($"{accounts.Count + 1}. Return to main menu");
            Console.Write($"\nEnter your choice (1-{accounts.Count + 1}): ");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= accounts.Count + 1)
            {
                if (choice <= accounts.Count)
                {
                    return accounts[choice - 1];
                }
                return null; // Return to main menu
            }
            else
            {
                Console.WriteLine("Invalid selection. Press any key to continue...");
                Console.ReadKey(true);
                return null;
            }
        }

        static void AccountMenu(Account account, List<Account> allAccounts)
        {
            bool returnToMainMenu = false;

            while (!returnToMainMenu)
            {
                Console.Clear();
                Console.WriteLine($"\nAccount: {account.OwnerName} (Acc#: {account.AccountNumber})");
                Console.WriteLine($"Current Balance: {account.Balance:C}");
                Console.WriteLine("--------------------");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Return to Main Menu");
                Console.Write("\nEnter your choice (1-5): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine($"\nCurrent Balance: {account.Balance:C}");
                            WaitForKeyPress();
                            break;
                        case 2:
                            PerformDeposit(account);
                            WaitForKeyPress();
                            break;
                        case 3:
                            PerformWithdrawal(account);
                            WaitForKeyPress();
                            break;
                        case 4:
                            PerformTransfer(account, allAccounts);
                            WaitForKeyPress();
                            break;
                        case 5:
                            returnToMainMenu = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            WaitForKeyPress();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                    WaitForKeyPress();
                }
            }
        }

        static void PerformDeposit(Account account)
        {
            Console.Write("\nEnter amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                try
                {
                    account.Deposit(amount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        static void PerformWithdrawal(Account account)
        {
            Console.Write("\nEnter amount to withdraw: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                try
                {
                    account.Withdraw(amount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        static void PerformTransfer(Account sourceAccount, List<Account> allAccounts)
        {
            // First, list all possible recipient accounts except the source account
            Console.WriteLine("\nSelect recipient account:");
            List<Account>? recipientOptions = allAccounts.Where(acc => acc.AccountNumber != sourceAccount.AccountNumber).ToList();

            if (recipientOptions.Count == 0)
            {
                Console.WriteLine("No other accounts available for transfer.");
                return;
            }

            for (int i = 0; i < recipientOptions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipientOptions[i].OwnerName} (Acc#: {recipientOptions[i].AccountNumber})");
            }

            Console.Write($"\nEnter recipient account number (1-{recipientOptions.Count}): ");
            if (int.TryParse(Console.ReadLine(), out int recipientIndex) &&
                recipientIndex >= 1 && recipientIndex <= recipientOptions.Count)
            {
                Account? recipientAccount = recipientOptions[recipientIndex - 1];

                Console.Write("\nEnter amount to transfer: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    try
                    {
                        sourceAccount.Transfer(amount, recipientAccount);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid amount.");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }

        static void WaitForKeyPress()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }
}
