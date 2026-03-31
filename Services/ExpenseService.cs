using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseTrackerCLI.Models;
using System.Text.Json;
using System.IO;


namespace ExpenseTrackerCLI.Services
{
    public class ExpenseService
    {
        private List<Expense> expenses = new List<Expense>();
        private int idCounter = 1;
        private string FilePath = "expenses.json";

        public ExpenseService()
        {
            LoadExpenses();
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

        public void AddExpense(string description, decimal amount)
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
                Date = DateTime.Now
            });

            SaveExpenses();
        }

        public List<Expense> GetExpenses()
        {
            return expenses;
        }

        public Expense GetExpenseById(int id)
        {
            return expenses.FirstOrDefault(e => e.Id == id);
        }

        public bool UpdateExpense(int id, string description, decimal amount)
        {
            var expense = GetExpenseById(id);
            if (expense != null)
            {
                expense.Description = description;
                expense.Amount = amount;
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
    }
}