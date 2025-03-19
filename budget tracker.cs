using System;
using System.Collections.Generic;
using System.IO;

class BudgetTracker
{
    static string filePath = "expenses.txt";
    static List<Expense> expenses = new List<Expense>();

    static void Main()
    {
        LoadExpenses();

        while (true)
        {
            Console.WriteLine("\n===== Budget Tracker =====");
            Console.WriteLine("1. Add Expense");
            Console.WriteLine("2. View Expenses");
            Console.WriteLine("3. Show Total Expenses");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddExpense();
                    break;
                case "2":
                    ViewExpenses();
                    break;
                case "3":
                    ShowTotalExpenses();
                    break;
                case "4":
                    SaveExpenses();
                    Console.WriteLine("Exiting... Data saved.");
                    return;
                default:
                    Console.WriteLine("Invalid option! Try again.");
                    break;
            }
        }
    }

    static void AddExpense()
    {
        Console.Write("Enter category (e.g., Food, Rent, Shopping): ");
        string category = Console.ReadLine();
        
        Console.Write("Enter amount: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            Console.WriteLine("Invalid amount! Try again.");
            return;
        }

        string date = DateTime.Now.ToString("yyyy-MM-dd");
        expenses.Add(new Expense(category, amount, date));
        
        Console.WriteLine("Expense added successfully!");
    }

    static void ViewExpenses()
    {
        Console.WriteLine("\n==== Your Expenses ====");
        if (expenses.Count == 0)
        {
            Console.WriteLine("No expenses recorded.");
            return;
        }

        foreach (var exp in expenses)
        {
            Console.WriteLine($"Category: {exp.Category}, Amount: {exp.Amount:C}, Date: {exp.Date}");
        }
    }

    static void ShowTotalExpenses()
    {
        decimal total = 0;
        foreach (var exp in expenses)
        {
            total += exp.Amount;
        }
        Console.WriteLine($"\nTotal Expenses: {total:C}");
    }

    static void SaveExpenses()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var exp in expenses)
            {
                writer.WriteLine($"{exp.Category},{exp.Amount},{exp.Date}");
            }
        }
    }

    static void LoadExpenses()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                string[] data = line.Split(',');
                if (data.Length == 3 && decimal.TryParse(data[1], out decimal amount))
                {
                    expenses.Add(new Expense(data[0], amount, data[2]));
                }
            }
        }
    }
}

class Expense
{
    public string Category { get; }
    public decimal Amount { get; }
    public string Date { get; }

    public Expense(string category, decimal amount, string date)
    {
        Category = category;
        Amount = amount;
        Date = date;
    }
}
