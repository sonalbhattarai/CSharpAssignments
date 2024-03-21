using System;

public class BankAccount
{
    public string AccountNumber { get; private set; }
    public decimal Balance { get; private set; }
    public string Type { get; private set; }

    //Creating a new bank acoount with Acoount number, balance and type
    public BankAccount(string accountNumber, decimal initialBalance, string type = "Checking")
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
        Type = type;
    }

    //Method to deposit amount in account 
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Deposit amount must be positive.");
            return;
        }
        Balance += amount;
        Console.WriteLine($"{amount} has been deposited.\nNew balance: ${Balance}");
    }

    //Method to withdraw amount from the account.
    public bool Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Withdrawal amount must be positive.");
            return false;
        }

        if (Balance < amount)
        {
            Console.WriteLine("Insufficient funds.");
            return false;
        }

        Balance -= amount;
        Console.WriteLine($"{amount} has been withdrawn.\nNew balance: ${Balance}");
        return true;
    }

}

public class Program
{
    static void Main(string[] args)
    {
        // Create a checking account with an initial balance
        var myAccount = new BankAccount("C0895168", 1000);

        // Prompt the user to enter a  deposit and withdrawal amount for the checking account.
        Console.Write("Enter deposit amount for the Checking account: ");
        decimal depositAmount = ReadDecimalFromUser();
        myAccount.Deposit(depositAmount);

        Console.Write("Enter withdrawal amount for the Checking account: ");
        decimal withdrawalAmount = ReadDecimalFromUser();
        myAccount.Withdraw(withdrawalAmount);

        // Create a savings account with an initial balance
        var mySavings = new BankAccount("SC0895168", 1500, "Saving");

        // Prompt the user to enter a  deposit and withdrawal amount for the saving account.
        Console.Write("Enter deposit amount for the savings account: ");
        depositAmount = ReadDecimalFromUser();
        mySavings.Deposit(depositAmount);

        Console.Write("Enter withdrawal amount for the savings account: ");
        withdrawalAmount = ReadDecimalFromUser();
        mySavings.Withdraw(withdrawalAmount);
    }

    // Method to read a decimal value and ensuring that the input is valid.
    private static decimal ReadDecimalFromUser()
    {
        decimal result;
        while (true)
        {
            string input = Console.ReadLine();
            if (decimal.TryParse(input, out result))
            {
                return result;
            }
            else
            {
                // Prompt the user again for a valid decimal number.
                Console.Write("Invalid input. Please enter a valid amount: ");
            }
        }
    }
}
