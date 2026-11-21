using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentPerformance
{
    /// <summary>Модель студента.</summary>
    public class Student
    {
        public string Name { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public List<int> Grades { get; } = new();
    }

    /// <summary>
    /// Учёт успеваемости.
    /// Задание 2: добавить метод вычисления среднего балла по группе.
    /// </summary>
    public class PerformanceJournal
    {
        private readonly List<Student> students = new();

        /// <summary>Добавление студента с валидацией.</summary>
        public void AddStudent(Student student)
        {
            if (student is null)
                throw new ArgumentNullException(nameof(student));
            if (string.IsNullOrWhiteSpace(student.Name))
                throw new ArgumentException("Имя обязательно.", nameof(student));
            if (string.IsNullOrWhiteSpace(student.Group))
                throw new ArgumentException("Группа обязательна.", nameof(student));
            if (students.Exists(s => s.Name.Equals(student.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Студент с таким именем уже существует.");

            students.Add(student);
        }

        /// <summary>Добавление оценки студенту.</summary>
        public void AddGrade(string name, int grade)
        {
            if (grade < 2 || grade > 5)
                throw new ArgumentOutOfRangeException(nameof(grade), "Оценка должна быть от 2 до 5.");

            Student? student = students.Find(s =>
                s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (student is null)
                throw new InvalidOperationException("Студент не найден.");

            student.Grades.Add(grade);
        }

        // TODO (Задание 2): реализовать метод вычисления среднего балла по группе.
        // public double GetGroupAverage(string group) { ... }
    }

    internal static class Program
    {
        private static void Main()
        {
            var journal = new PerformanceJournal();
            journal.AddStudent(new Student { Name = "Анна", Group = "ИС-21" });
            journal.AddStudent(new Student { Name = "Борис", Group = "ИС-21" });

            journal.AddGrade("Анна", 5);
            journal.AddGrade("Борис", 4);

            Console.WriteLine("Оценки добавлены. Средний балл по группе — TODO.");
        }
    }
}