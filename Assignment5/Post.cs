using System;
using System.Collections.Generic;

// Base class representing a generic mail
public abstract class Mail
{
    protected double weight; // Weight of the mail in grams
    protected bool express; // Indicates if the mail is sent in express mode
    protected string destinationAddress; // Destination address of the mail

    public Mail(double weight, bool express, string destinationAddress)
    {
        if (weight <= 0)
            throw new ArgumentException("Weight must be greater than zero.");

        this.weight = weight;
        this.express = express;
        this.destinationAddress = destinationAddress;
    }

    // Method to calculate and return the postage amount
    public abstract double Stamp();

    // Method to check if the mail is valid
    public abstract bool IsValid();

    // Method to display mail information
    public abstract void Display();
}

// Class representing a Letter
public class Letter : Mail
{
    private string format; // Format of the letter ("A3" or "A4")

    public Letter(double weight, bool express, string destinationAddress, string format)
        : base(weight, express, destinationAddress)
    {
        if (format != "A3" && format != "A4")
            throw new ArgumentException("Invalid letter format.");

        this.format = format;
    }

    public override double Stamp()
    {
        double baseFare = (format == "A4") ? 2.50 : 3.50;
        double amount = (express ? 2 : 1) * (baseFare + 0.001 * weight);
        return amount;
    }

    public override bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(destinationAddress) && (format == "A3" || format == "A4");
    }

    public override void Display()
    {
        Console.WriteLine("Letter");
        if (IsValid())
        {
            Console.WriteLine($"    Weight: {weight} grams");
            Console.WriteLine($"    Express: {(express ? "yes" : "no")}");
            Console.WriteLine($"    Destination: {destinationAddress}");
            Console.WriteLine($"    Price: ${Stamp():F1}");
            Console.WriteLine($"    Format: {format}");
        }
        else
        {
            Console.WriteLine("    (Invalid courier)");
            Console.WriteLine($"    Weight: {weight} grams");
            Console.WriteLine($"    Express: {(express ? "yes" : "no")}");
            Console.WriteLine($"    Destination:");
            Console.WriteLine($"    Price: 0.0 CHF");
            Console.WriteLine($"    Format: {format}");
        }
    }
}

// Class representing a Parcel
public class Parcel : Mail
{
    private double volume; // Volume of the parcel in liters

    public Parcel(double weight, bool express, string destinationAddress, double volume)
        : base(weight, express, destinationAddress)
    {
        if (volume <= 0)
            throw new ArgumentException("Volume must be greater than zero.");

        this.volume = volume;
    }

    public override double Stamp()
    {
        double amount = (express ? 2 : 1) * (0.25 * volume + weight * 1.0);
        return amount;
    }

    public override bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(destinationAddress) && volume <= 50;
    }

    public override void Display()
    {
        Console.WriteLine("Parcel");
        if (IsValid())
        {
            Console.WriteLine($"    Weight: {weight} grams");
            Console.WriteLine($"    Express: {(express ? "yes" : "no")}");
            Console.WriteLine($"    Destination: {destinationAddress}");
            Console.WriteLine($"    Price: ${Stamp():F1}");
            Console.WriteLine($"    Volume: {volume} liters");
        }
        else
        {
            Console.WriteLine("    (Invalid courier)");
            Console.WriteLine($"    Weight: {weight} grams");
            Console.WriteLine($"    Express: {(express ? "yes" : "no")}");
            Console.WriteLine($"    Destination: {destinationAddress}");
            Console.WriteLine($"    Price: 0.0");
            Console.WriteLine($"    Volume: {volume} liters");
        }
    }
}

// Class representing an Advertisement
public class Advertisement : Mail
{
    // Constructor
    public Advertisement(double weight, bool express, string destinationAddress)
        : base(weight, express, destinationAddress)
    {
    }

    public override double Stamp()
    {
        double amount = (express ? 2 : 1) * (5.0 * weight);
        return amount;
    }

    public override bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(destinationAddress);
    }

    public override void Display()
    {
        Console.WriteLine("Advertisement");
        if (IsValid())
        {
            Console.WriteLine($"    Weight: {weight} grams");
            Console.WriteLine($"    Express: {(express ? "yes" : "no")}");
            Console.WriteLine($"    Destination: {destinationAddress}");
            Console.WriteLine($"    Price: ${Stamp():F1}");
        }
        else
        {
            Console.WriteLine("    (Invalid courier)");
            Console.WriteLine($"    Weight: {weight} grams");
            Console.WriteLine($"    Express: {(express ? "yes" : "no")}");
            Console.WriteLine($"    Destination:");
            Console.WriteLine($"    Price: 0.0");
        }
    }
}

// Class representing a mailbox
public class Mailbox
{
    private List<Mail> mails;

    public Mailbox()
    {
        mails = new List<Mail>();
    }

    // Method to add mail to the mailbox
    public void AddMail(Mail mail)
    {
        mails.Add(mail);
    }

    // Method to stamp all mails and return total postage amount
    public double Stamp()
    {
        double totalPostage = 0.0;
        foreach (Mail mail in mails)
        {
            totalPostage += mail.Stamp();
        }
        return totalPostage;
    }

    // Method to count and return the number of invalid mails
    public int InvalidMails()
    {
        int invalidCount = 0;
        foreach (Mail mail in mails)
        {
            if (!mail.IsValid())
            {
                invalidCount++;
            }
        }
        return invalidCount;
    }

    // Method to display contents of the mailbox
    public void Display()
    {
        foreach (Mail mail in mails)
        {
            mail.Display();
        }
    }
}



class Post
{
    static void Main()
    {
        // Instantiate the mailbox
        Mailbox mailbox = new Mailbox();

        // Add some mails to the mailbox

        Letter letter1 = new Letter(200, true, "Chemin des Acacias 28, 1009 Pully", "A3");
        Letter letter2 = new Letter(800, false, "", "A4"); // invalid

        Advertisement ad1 = new Advertisement(1500, true, "Les Moilles  13A, 1913 Saillon");
        Advertisement ad2 = new Advertisement(3000, false, ""); // invalid

        Parcel parcel1 = new Parcel(5000, true, "Grand rue 18, 1950 Sion", 30);
        Parcel parcel2 = new Parcel(3000, true, "Chemin des fleurs 48, 2800 Delemont", 70); // invalid parcel

        
        // Add mails to the mailbox
        mailbox.AddMail(letter1);
        mailbox.AddMail(letter2);
        mailbox.AddMail(ad1);
        mailbox.AddMail(ad2);
        mailbox.AddMail(parcel1);
        mailbox.AddMail(parcel2);

        // Display contents of the mailbox
        mailbox.Display();

        // Stamp the mails and display the total postage
        double totalPostage = mailbox.Stamp();
        Console.WriteLine($"The total amount of postage is {totalPostage:F1}");

        // Display the number of invalid mails
        int invalidCount = mailbox.InvalidMails();
        Console.WriteLine($"The box contains {invalidCount} invalid mails");
    }
}
