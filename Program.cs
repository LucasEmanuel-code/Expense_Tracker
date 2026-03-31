using System;
using System.Linq;
using System.Globalization;
using ExpenseTrackerCLI.Services;

class Program
{
    static void Main(string[] args)
    {
        var service = new ExpenseService();

        if (args.Length == 0)
        {
            Console.WriteLine("No command provided");
            return;
        }

        string command = args[0];

        switch (command)
        {
            case "add":
                AddExpense(service, args);
                break;

            case "list":
                ViewExpenses(service);
                break;

            case "summary":
                TotalExpenses(service, args);
                break;

            case "delete":
                RemoveExpense(service, args);
                break;

            case "update":
                UpdateExpense(service, args);
                break;

            default:
                Console.WriteLine("Unknown command.");
                break;
        }
    }

    static void AddExpense(ExpenseService service, string[] args)
    {
        string description = "";
        decimal amount = 0;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--description" && i + 1 < args.Length)
            {
                description = args[i + 1];
            }
            else if (args[i] == "--amount" && i + 1 < args.Length)
            {
                if (!decimal.TryParse(args[i + 1],
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out amount))
                {
                    Console.WriteLine("Invalid amount format.");
                    return;
                }
            }
        }

        service.AddExpense(description, amount);
        Console.WriteLine("Expense added successfully!");
    }

    static void ViewExpenses(ExpenseService service)
    {
        var list = service.GetExpenses();

        if (!list.Any())
        {
            Console.WriteLine("No expenses found.");
            return;
        }

        Console.WriteLine("ID   Date       Description        Amount");

        foreach (var e in list)
        {
            Console.WriteLine($"{e.Id,-5} {e.Date:yyyy-MM-dd} {e.Description,-15} R${e.Amount}");
        }
    }

    static void TotalExpenses(ExpenseService service, string[] args)
    {
        if (args.Contains("--month"))
        {
            int month = int.Parse(args[Array.IndexOf(args, "--month") + 1]);

            var filtered = service.GetExpenses()
                .Where(e => e.Date.Month == month);

            Console.WriteLine($"Total expenses for month {month}: R${filtered.Sum(e => e.Amount)}");
        }
        else
        {
            Console.WriteLine($"Total Expenses: R${service.GetTotalExpenses()}");
        }
    }

    static void RemoveExpense(ExpenseService service, string[] args)
    {
        int id = int.Parse(args[Array.IndexOf(args, "--id") + 1]);

        if (service.RemoveExpense(id))
            Console.WriteLine("Expense deleted successfully.");
        else
            Console.WriteLine("Expense not found.");
    }

    static void UpdateExpense(ExpenseService service, string[] args)
    {
        int id = 0;
        string description = "";
        decimal amount = 0;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--id" && i + 1 < args.Length)
                id = int.Parse(args[i + 1]);

            else if (args[i] == "--description" && i + 1 < args.Length)
                description = args[i + 1];

            else if (args[i] == "--amount" && i + 1 < args.Length)
            {
                decimal.TryParse(args[i + 1],
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out amount);
            }
        }

        if (service.UpdateExpense(id, description, amount))
            Console.WriteLine("Expense updated successfully.");
        else
            Console.WriteLine("Expense not found.");
    }
}