using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseTrackerCLI.Models;
using System.Text.Json;
using System.IO;
using System.ComponentModel;


namespace ExpenseTrackerCLI.Services
{
    public class ExpenseService
    {
        private List<Budget> budgets = new List<Budget>();
        private string BudgetFilePath = "budgets.json";
        private List<Expense> expenses = new List<Expense>();
        private int idCounter = 1;
        private string FilePath = "expenses.json";

        public ExpenseService()
        {
            LoadBudgets();
            LoadExpenses();
        }
        private void LoadBudgets()
        {
            if (File.Exists(BudgetFilePath))
            {
                var json = File.ReadAllText(BudgetFilePath);
                budgets = JsonSerializer.Deserialize<List<Budget>>(json) ?? new List<Budget>();
            }
        }

        private void SaveBudgets()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(budgets);
            File.WriteAllText(BudgetFilePath, json);
        }

        public void SetBudget(int month, decimal limit)
        {
            var budget = budgets.FirstOrDefault(b => b.Month == month);
            if (budget != null)
            {
                budget.Limit = limit;
            }
            else
            {
                budgets.Add(new Budget { Month = month, Limit = limit });
            }
            SaveBudgets();
        }
        private void CheckBudget()
        {
            var budget = budgets.FirstOrDefault(b => b.Month == DateTime.Now.Month);
            if (budget != null)
            {
                var totalExpenses = expenses
                    .Where(e => e.Date.Month == DateTime.Now.Month && e.Date.Year == DateTime.Now.Year)
                    .Sum(e => e.Amount);

                if (totalExpenses > budget.Limit)
                {
                    Console.WriteLine($"Budget exceeded! Limit: R${budget.Limit}, Expenses: R${totalExpenses}");
                }
            }
        }

        private void SaveExpenses()
        {
            var json = JsonSerializer.Serialize(expenses, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(FilePath, json);
        }

        private void LoadExpenses()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                expenses = JsonSerializer.Deserialize<List<Expense>>(json) ?? new List<Expense>();
                if (expenses.Any())
                {
                    idCounter = expenses.Max(e => e.Id) + 1;
                }
            }
        }

        public void ExportToCSV(string fileName, int? month = null)
        {
            var filtred = expenses;

            if (month.HasValue)
            {
                filtred = expenses.Where(e => e.Date.Month == month.Value).ToList();
            }

            var lines = new List<string>
            {
                "Id,Date,Description,Category,Amount"
            };

            foreach (var e in filtred)
            {
                lines.Add($"{e.Id},{e.Date:yyyy-MM-dd},{e.Description},{e.Category},{e.Amount}");
            }

            File.WriteAllLines(fileName, lines);

            Console.WriteLine($"Expenses exported to {fileName}");
        }

        public void AddExpense(string description, decimal amount, string category)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Amount must be greater than zero.");
                return;
            }

            expenses.Add(new Expense
            {
                Id = idCounter++,
                Description = description,
                Amount = amount,
                Date = DateTime.Now,
                Category = category
            });

            SaveExpenses();
            CheckBudget();
        }

        public List<Expense> GetExpenses()
        {
            return expenses;
        }

        public Expense GetExpenseById(int id)
        {
            return expenses.FirstOrDefault(e => e.Id == id);
        }

        public bool UpdateExpense(int id, string description, decimal amount, string category)
        {
            var expense = GetExpenseById(id);
            if (expense != null)
            {
                expense.Description = description;
                expense.Amount = amount;
                expense.Category = category;
                SaveExpenses();
                return true;
            }
            return false;
        }

        public decimal GetTotalExpenses()
        {
            return expenses.Sum(e => e.Amount);
        }

        public bool RemoveExpense(int id)
        {
            var expense = GetExpenseById(id);
            if (expense != null)
            {
                expenses.Remove(expense);
                SaveExpenses();
                return true;    
            }
            return false;
        }

        public decimal GetTotalExpensesByDate(DateTime date)
        {
            return expenses
                .Where(e => e.Date.Month == date.Month && e.Date.Year == date.Year)
                .Sum(e => e.Amount);
        }

        public List<Expense> GetExpensesByDate(DateTime date)
        {
            return expenses
                .Where(e => e.Date.Month == date.Month && e.Date.Year == date.Year)
                .ToList();
        }

        public List<Expense> GetExpensesByCategory(string category)
        {
            return expenses
                .Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}