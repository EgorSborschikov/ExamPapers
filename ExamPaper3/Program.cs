using System;
using System.Collections.Generic;
using System.Linq;

namespace BankTransfers
{
    /// <summary>Модель счёта.</summary>
    public class Account
    {
        public string Number { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }

    /// <summary>Сервис банковских переводов.</summary>
    public class TransferService
    {
        private readonly List<Account> accounts = new();

        /// <summary>Открытие счёта с валидацией.</summary>
        public void OpenAccount(Account account)
        {
            if (account is null)
                throw new ArgumentNullException(nameof(account));
            if (string.IsNullOrWhiteSpace(account.Number))
                throw new ArgumentException("Номер счёта обязателен.", nameof(account));
            if (accounts.Any(a => a.Number == account.Number))
                throw new InvalidOperationException("Счёт с таким номером уже существует.");
            if (account.Balance < 0)
                throw new ArgumentException("Баланс не может быть отрицательным.", nameof(account));

            accounts.Add(account);
        }

        /// <summary>Поиск счёта по номеру.</summary>
        public Account? FindAccount(string number)
        {
            return accounts.Find(a => a.Number == number);
        }

        /// <summary>Перевод между счетами.</summary>
        public void Transfer(string fromNumber, string toNumber, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма перевода должна быть положительной.", nameof(amount));

            Account? from = FindAccount(fromNumber);
            Account? to = FindAccount(toNumber);

            if (from is null || to is null)
                throw new InvalidOperationException("Один из счетов не найден.");
            if (from.Balance < amount)
                throw new InvalidOperationException("Недостаточно средств на счёте.");

            from.Balance -= amount;
            to.Balance += amount;
        }

        // TODO (Задание 2): реализовать метод расчёта комиссии за перевод (например, 1% от суммы, минимум 50 руб.).
        // public decimal CalculateCommission(decimal amount) { ... }
    }

    internal static class Program
    {
        private static void Main()
        {
            var service = new TransferService();
            service.OpenAccount(new Account { Number = "001", Owner = "Иван", Balance = 10000m });
            service.OpenAccount(new Account { Number = "002", Owner = "Мария", Balance = 5000m });

            Console.Write("Счёт списания: ");
            string from = Console.ReadLine() ?? string.Empty;
            Console.Write("Счёт зачисления: ");
            string to = Console.ReadLine() ?? string.Empty;
            Console.Write("Сумма: ");
            decimal amount = decimal.Parse(Console.ReadLine() ?? "0");

            service.Transfer(from, to, amount);
            Console.WriteLine($"Баланс {from}: {service.FindAccount(from)?.Balance:F2}");
            Console.WriteLine($"Баланс {to}: {service.FindAccount(to)?.Balance:F2}");
        }
    }
}