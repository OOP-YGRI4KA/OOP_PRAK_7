using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

class Program
{
    static List<Book> books = new List<Book>();

    static string filePath = "books.json";

    static void Main()
    {
        LoadBooks();

        while (true)
        {
            Console.WriteLine("\nКАТАЛОГ КНИГ");
            Console.WriteLine("1. Додати книгу");
            Console.WriteLine("2. Знайти книгу");
            Console.WriteLine("3. Видалити книгу");
            Console.WriteLine("4. Показати всі книги");
            Console.WriteLine("5. Зберегти каталог у JSON");
            Console.WriteLine("0. Вийти");

            Console.Write("\nВаш вибір: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBook();
                    break;

                case "2":
                    SearchBook();
                    break;

                case "3":
                    DeleteBook();
                    break;

                case "4":
                    ShowBooks();
                    break;

                case "5":
                    SaveBooks();
                    break;

                case "0":
                    Console.WriteLine("Завершення програми.");
                    return;

                default:
                    Console.WriteLine("Неправильний вибір!");
                    break;
            }
        }
    }

    static void AddBook()
    {
        Console.Write("Введіть назву книги: ");
        string title = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Введіть автора: ");
        string author = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(title) ||
            string.IsNullOrWhiteSpace(author))
        {
            Console.WriteLine("Назва та автор не можуть бути порожніми!");
            return;
        }

        Console.Write("Введіть рік видання: ");

        if (!int.TryParse(Console.ReadLine(), out int year) || year <= 0)
        {
            Console.WriteLine("Неправильний рік видання!");
            return;
        }

        int id = books.Count == 0
            ? 1
            : books.Max(b => b.Id) + 1;

        Book book = new Book
        {
            Id = id,
            Title = title,
            Author = author,
            Year = year
        };

        books.Add(book);

        Console.WriteLine("Книгу успішно додано!");
    }

    static void SearchBook()
    {
        Console.Write("Введіть назву книги для пошуку: ");
        string title = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Введіть назву книги!");
            return;
        }

        List<Book> foundBooks = books
            .Where(b => b.Title.Contains(
                title, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (foundBooks.Count == 0)
        {
            Console.WriteLine("Книгу не знайдено!");
        }
        else
        {
            foreach (Book book in foundBooks)
            {
                Console.WriteLine(
                    $"ID: {book.Id}, Назва: {book.Title}, Автор: {book.Author}, Рік: {book.Year}");
            }
        }
    }

    static void DeleteBook()
    {
        Console.Write("Введіть ID книги для видалення: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Неправильний ID!");
            return;
        }

        Book? book = books.FirstOrDefault(b => b.Id == id);

        if (book is not null)
        {
            books.Remove(book);
            Console.WriteLine("Книгу успішно видалено!");
        }
        else
        {
            Console.WriteLine("Книгу не знайдено!");
        }
    }

    static void ShowBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Каталог порожній!");
            return;
        }

        Console.WriteLine("\nСписок книг:");

        foreach (Book book in books)
        {
            Console.WriteLine(
                $"ID: {book.Id}, Назва: {book.Title}, Автор: {book.Author}, Рік: {book.Year}");
        }
    }

    static void SaveBooks()
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(
                    UnicodeRanges.BasicLatin,
                    UnicodeRanges.Cyrillic)
            };

            string json = JsonSerializer.Serialize(books, options);

            File.WriteAllText(filePath, json);

            Console.WriteLine("Каталог успішно збережено!");
            Console.WriteLine(Path.GetFullPath(filePath));
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Помилка збереження: {ex.Message}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Немає доступу для запису файлу!");
        }
    }

    static void LoadBooks()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл books.json ще не створено.");
            return;
        }

        try
        {
            string json = File.ReadAllText(filePath);

            books = JsonSerializer.Deserialize<List<Book>>(json)
                    ?? new List<Book>();

            Console.WriteLine("Каталог успішно завантажено!");
        }
        catch (JsonException)
        {
            Console.WriteLine("Помилка формату JSON!");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Помилка читання: {ex.Message}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Немає доступу до файлу!");
        }
    }
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public int Year { get; set; }
}