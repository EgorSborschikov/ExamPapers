using System;
using System.Collections.Generic;
using System.Linq;

namespace EquipmentRental
{
    /// <summary>Модель единицы оборудования.</summary>
    public class Equipment
    {
        public string Name { get; set; } = string.Empty;
        public decimal DailyRate { get; set; }
        public bool IsRented { get; set; }
    }

    /// <summary>Модель аренды.</summary>
    public class Rental
    {
        public string EquipmentName { get; set; } = string.Empty;
        public string Client { get; set; } = string.Empty;
        public int Days { get; set; }
    }

    /// <summary>
    /// Учёт аренды оборудования.
    /// Задание 2: добавить валидацию срока аренды (1..30 дней) и занятости оборудования.
    /// </summary>
    public class RentalService
    {
        private readonly List<Equipment> equipment = new();
        private readonly List<Rental> rentals = new();

        /// <summary>Добавление оборудования с валидацией.</summary>
        public void AddEquipment(Equipment item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Название обязательно.", nameof(item));
            if (item.DailyRate <= 0)
                throw new ArgumentException("Ставка должна быть положительной.", nameof(item));

            equipment.Add(item);
        }

        /// <summary>Расчёт стоимости аренды.</summary>
        public decimal CalculateCost(decimal dailyRate, int days)
        {
            if (dailyRate <= 0 || days <= 0)
                throw new ArgumentException("Некорректные параметры аренды.");

            return dailyRate * days;
        }

        // TODO (Задание 2): добавить валидацию срока (1..30 дней) и проверку, что оборудование не занято.
        // public void Rent(string equipmentName, string client, int days) { ... }
    }

    internal static class Program
    {
        private static void Main()
        {
            var service = new RentalService();
            service.AddEquipment(new Equipment { Name = "Перфоратор", DailyRate = 500m });
            service.AddEquipment(new Equipment { Name = "Генератор", DailyRate = 1200m });

            Console.Write("Оборудование: ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.Write("Клиент: ");
            string client = Console.ReadLine() ?? string.Empty;
            Console.Write("Срок (дней): ");
            int days = int.Parse(Console.ReadLine() ?? "0");

            var item = service.GetType()
                .GetField("equipment", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(service) as List<Equipment>;
            var found = item?.Find(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (found is null)
            {
                Console.WriteLine("Оборудование не найдено.");
                return;
            }

            Console.WriteLine($"Стоимость аренды: {service.CalculateCost(found.DailyRate, days):F2} руб.");
        }
    }
}