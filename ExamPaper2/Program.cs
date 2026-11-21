using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccounting
{
    /// <summary>Модель книги.</summary>
    public class Book
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
    }

    /// <summary>
    /// Каталог библиотеки.
    /// Задание 2: реализовать поиск книги по автору.
    /// </summary>
    public class LibraryCatalog
    {
        private readonly List<Book> books = new();

        /// <summary>Добавление книги с проверкой дубликата и года.</summary>
        public bool AddBook(Book book)
        {
            if (book is null)
                throw new ArgumentNullException(nameof(book));
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Название обязательно.", nameof(book));
            if (book.Year < 0 || book.Year > DateTime.Now.Year)
                throw new ArgumentException("Некорректный год издания.", nameof(book));

            bool exists = books.Exists(b =>
                b.Title.Equals(book.Title, StringComparison.OrdinalIgnoreCase) &&
                b.Author.Equals(book.Author, StringComparison.OrdinalIgnoreCase));

            if (exists) return false;

            books.Add(book);
            return true;
        }

        /// <summary>Поиск книги по названию.</summary>
        public IEnumerable<Book> FindByTitle(string title)
        {
            return books.FindAll(b =>
                b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }

        // TODO (Задание 2): реализовать метод поиска книги по автору.
        // public IEnumerable<Book> FindByAuthor(string author) { ... }
    }

    internal static class Program
    {
        private static void Main()
        {
            var catalog = new LibraryCatalog();
            catalog.AddBook(new Book { Title = "Война и мир", Author = "Толстой", Year = 1869 });
            catalog.AddBook(new Book { Title = "Преступление и наказание", Author = "Достоевский", Year = 1866 });

            Console.Write("Введите название для поиска: ");
            string title = Console.ReadLine() ?? string.Empty;

            foreach (var book in catalog.FindByTitle(title))
                Console.WriteLine($"{book.Title} — {book.Author}, {book.Year}");
        }
    }
}