using System;
using System.Collections.Generic;

namespace WarehouseAccounting
{
    /// <summary>Модель товара.</summary>
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    /// <summary>
    /// Складской учёт.
    /// Задание 2: добавить метод списания товара при продаже.
    /// </summary>
    public class Warehouse
    {
        private readonly List<Product> products = new();

        /// <summary>Добавление товара с валидацией.</summary>
        public void AddProduct(Product product)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product));
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Название обязательно.", nameof(product));
            if (product.Quantity < 0)
                throw new ArgumentException("Количество не может быть отрицательным.", nameof(product));
            if (product.Price < 0)
                throw new ArgumentException("Цена не может быть отрицательной.", nameof(product));

            products.Add(product);
        }

        /// <summary>Поиск товара по названию.</summary>
        public Product? FindByName(string name)
        {
            return products.Find(p =>
                p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // TODO (Задание 2): реализовать метод списания товара при продаже.
        // Уменьшать Quantity, проверять достаточность остатка.
        // public bool WriteOff(string name, int count) { ... }
    }

    internal static class Program
    {
        private static void Main()
        {
            var warehouse = new Warehouse();
            warehouse.AddProduct(new Product { Name = "Монитор", Quantity = 10, Price = 15000m });

            Product? product = warehouse.FindByName("Монитор");
            Console.WriteLine(product is null
                ? "Товар не найден."
                : $"{product.Name}: {product.Quantity} шт. по {product.Price:F2} руб.");
        }
    }
}