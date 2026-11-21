using System;
using System.Collections.Generic;

namespace PhoneBook
{
    /// <summary>Модель контакта.</summary>
    public class Contact
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    /// <summary>
    /// Записная книжка.
    /// Задание 2: добавить метод редактирования телефона по имени.
    /// </summary>
    public class PhoneBook
    {
        private readonly List<Contact> contacts = new();

        /// <summary>Добавление контакта с валидацией.</summary>
        public void AddContact(Contact contact)
        {
            if (contact is null)
                throw new ArgumentNullException(nameof(contact));
            if (string.IsNullOrWhiteSpace(contact.Name))
                throw new ArgumentException("Имя обязательно.", nameof(contact));
            if (string.IsNullOrWhiteSpace(contact.Phone))
                throw new ArgumentException("Телефон обязателен.", nameof(contact));
            if (contacts.Exists(c => c.Name.Equals(contact.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Контакт с таким именем уже существует.");

            contacts.Add(contact);
        }

        /// <summary>Поиск контакта по имени.</summary>
        public Contact? FindByName(string name)
        {
            return contacts.Find(c =>
                c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // TODO (Задание 2): реализовать метод редактирования телефона по имени.
        // public bool UpdatePhone(string name, string newPhone) { ... }
    }

    internal static class Program
    {
        private static void Main()
        {
            var book = new PhoneBook();
            book.AddContact(new Contact { Name = "Иван", Phone = "+7-900-000-00-01" });

            Contact? found = book.FindByName("Иван");
            Console.WriteLine(found is null
                ? "Контакт не найден."
                : $"{found.Name}: {found.Phone}");
        }
    }
}