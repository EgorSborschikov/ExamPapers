using System;
using System.Collections.Generic;

namespace DepositCalculator
{
    /// <summary>Модель вклада.</summary>
    public class Deposit
    {
        public string Owner { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public int Months { get; set; }
    }

    /// <summary>
    /// Калькулятор вкладов: простой процент.
    /// Задание 2: добавить расчёт сложного процента (капитализации).
    /// </summary>
    public class DepositCalculator
    {
        private readonly List<Deposit> deposits = new();

        /// <summary>Добавление вклада с валидацией.</summary>
        public void AddDeposit(Deposit deposit)
        {
            if (deposit is null)
                throw new ArgumentNullException(nameof(deposit));
            if (deposit.Amount <= 0)
                throw new ArgumentException("Сумма должна быть положительной.", nameof(deposit));
            if (deposit.Rate < 0)
                throw new ArgumentException("Ставка не может быть отрицательной.", nameof(deposit));
            if (deposit.Months <= 0)
                throw new ArgumentException("Срок должен быть положительным.", nameof(deposit));

            deposits.Add(deposit);
        }

        /// <summary>Расчёт дохода по простому проценту.</summary>
        public decimal CalculateSimpleInterest(decimal amount, decimal rate, int months)
        {
            if (amount <= 0 || rate < 0 || months <= 0)
                throw new ArgumentException("Некорректные параметры расчёта.");

            decimal interest = amount * (rate / 100m) * (months / 12m);
            return amount + interest;
        }

        // TODO (Задание 2): реализовать метод расчёта сложного процента (капитализации).
        // public decimal CalculateCompoundInterest(decimal amount, decimal rate, int months) { ... }
    }

    internal static class Program
    {
        private static void Main()
        {
            var calculator = new DepositCalculator();
            calculator.AddDeposit(new Deposit { Owner = "Иван", Amount = 100000m, Rate = 8m, Months = 12 });

            Console.Write("Сумма вклада: ");
            decimal amount = decimal.Parse(Console.ReadLine() ?? "0");
            Console.Write("Годовая ставка (%): ");
            decimal rate = decimal.Parse(Console.ReadLine() ?? "0");
            Console.Write("Срок (месяцев): ");
            int months = int.Parse(Console.ReadLine() ?? "0");

            decimal result = calculator.CalculateSimpleInterest(amount, rate, months);
            Console.WriteLine($"Итоговая сумма (простой процент): {result:F2}");
        }
    }
}